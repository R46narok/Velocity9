import csv
import json
import re

import keras.models
import matplotlib.pyplot as plt
import numpy
from keras import Input, Model, Sequential, activations
from keras.callbacks import EarlyStopping
from keras.layers import CuDNNLSTM, Dropout, RepeatVector, Dense, Bidirectional, TimeDistributed
from keras.utils import pad_sequences

from signal_rep_count.utils.metrics import Metrics
from signal_rep_count.utils.compute import ComputeMetric, ZeroCrossing, SmoothFilter
from motion_detection.inference.movenet_infer import *

import pandas as pd
import os
import numpy as np


def load_videos(directory, n):
    videos = []
    keypoints = []
    for root, dirs, files in os.walk(directory):

        for file in files:
            if file.endswith(".mp4"):
                video = cv2.VideoCapture(os.path.join(root, file))
                frames = []
                kps = []
                count = int(video.get(cv2.CAP_PROP_FRAME_COUNT))
                step = count // n
                for i in range(0, count, step):
                    video.set(cv2.CAP_PROP_POS_FRAMES, i)
                    ret, frame = video.read()
                    if ret:
                        frames.append(frame)
                        kp, image = get_inference(frame)
                        kp = kp.reshape(-1)
                        kps.append(kp)
                if len(frames) == n:
                    videos.append(frames)
                    keypoints.append(kps)
    return (videos, keypoints)


frames = 10
# videos, keypoints = load_videos("resized_videos/push_up", frames)
# Open a file: file
training_data = numpy.loadtxt("foo.csv", delimiter=',')
training_data = training_data.reshape((487, frames, 51))

features = 51
latent_dim = 512
batch_size = 8
epochs = 100
samples = 487

input_data = np.zeros(
    (samples, frames, features),
    dtype='float32')

target_data = np.zeros(
    (samples, frames, features),
    dtype='float32')


for i in range(samples):
    for t in range(frames):
        input_data[i, t] = training_data[i, t]
        if t > 0:
            target_data[i, t - 1] = training_data[i, t]


model = Sequential()
model.add(Bidirectional(CuDNNLSTM(512, input_shape=(None, features), return_sequences=True)))
model.add(Dropout(0.5))
model.add(Bidirectional(CuDNNLSTM(256, return_sequences=True)))
model.add(Dropout(0.5))
model.add(Bidirectional(CuDNNLSTM(128, return_sequences=True)))
model.add(Dropout(0.5))
model.add(TimeDistributed(Dense(features, activation="linear")))
model.compile(loss="mean_squared_error", optimizer='adam')

callbacks = [
   EarlyStopping(monitor='loss', patience=5)

]
# model.fit(input_data, target_data, batch_size=batch_size, validation_split=0.1, epochs=epochs, callbacks=callbacks)
# model.save("trained.h5")
# model.evaluate(input_data, target_data)
model = keras.models.load_model("trained.h5")
config_path = "CONFIG.json"
windowSize = 10


def main(video, reference, normalize):
    with open(config_path, "r") as f:
        data = json.load(f)

    normalize = False
    if True:
        cap = cv2.VideoCapture('1.avi')
        # cap = cv2.VideoCapture('resized_videos/push_up/40LMvyHkUKw_000011_000021_resized.mp4')
        fname = 'op_pu'
    else:
        cap = cv2.VideoCapture(0)
        fname = 'op_video.mp4'

    metricsThresh = data.get(reference, None)
    nonstat = metricsThresh[0]
    ref = metricsThresh[1]["mean"]
    ref_w = metricsThresh[1]["width"]
    ref_h = metricsThresh[1]["height"]

    if False:
        vcap = cv2.VideoCapture(0)
        f_width = vcap.get(cv2.CAP_PROP_FRAME_WIDTH)
        f_height = vcap.get(cv2.CAP_PROP_FRAME_HEIGHT)
        as_ratio = (f_width / ref_w, f_height / ref_h)
    else:
        as_ratio = (1, 1)

    # initialise all filter objects
    metrics = Metrics(as_ratio)
    track = [[] for _ in range(len(nonstat))]
    lpftrack = [SmoothFilter() for _ in range(len(nonstat))]
    zc = ZeroCrossing(windowSize, ref)

    overall_signal = []
    checkzc = []
    prev = reps = 0



    ## Writing the video with keypoints
    fps = cap.get(cv2.CAP_PROP_FPS)  # 25
    size = (input_size * 2, input_size * 2)
    fourcc = cv2.VideoWriter_fourcc(*'MP4V')
    video_writer = cv2.VideoWriter(fname, fourcc, fps, size)

    # plt.switch_backend('tkagg')
    # plt.ion()  # Set pyplot to interactive mode
    # fig, ax = plt.subplots()
    # plt.show()

    sequence = np.zeros((1, 1, features))

    collected = 0

    frames = []
    kps = []
    count = int(cap.get(cv2.CAP_PROP_FRAME_COUNT))
    step = count // 10
    for i in range(0, count, step):
        cap.set(cv2.CAP_PROP_POS_FRAMES, i)
        ret, frame = cap.read()
        if ret:
            frames.append(frame)
            kp, image = get_inference(frame)
            kp = kp.reshape(-1)
            kps.append(kp)

    seq = np.zeros((1, 10, features))
    seq[0, :, :] = np.asarray(kps)[:, :]
    predicted = model.predict(seq)

    reconstruction_error = np.mean(np.square(seq - predicted), axis=1)
    threshold = np.mean(reconstruction_error) + 3 * np.std(reconstruction_error)
    anomalies = np.where(reconstruction_error >= threshold)

    predicted = np.stack(predicted)
    predicted = predicted.reshape(10, 17, 3)
    i = 1
    while True:
        slice = predicted[i -1, :, :]
        image = frames[i]

        difference = np.abs(slice - kps[i].reshape(17, 3))
        max_difference = np.amax(difference)

        curr_kp = preprocess_kps(slice, image.shape[0], image.shape[1])
        output = draw_pose(image, curr_kp, preprocess=False)
        output = cv2.cvtColor(output, cv2.COLOR_BGR2RGB)
        outimage = np.asarray(output, dtype=np.uint8)
        outimage = cv2.resize(outimage, (640, 480))

        video_writer.write(outimage)
        cv2.imshow("frame", outimage)
        i+=1
        cv2.waitKey(0)
        if i >= 10:
            i = 1

    while True:
        ret, frame = cap.read()
        if not ret:
            break

        curr_kp, image = get_inference(frame)
        curr_kp = preprocess_kps(curr_kp, 640, 480)

        mean = np.mean(curr_kp, axis=0)
        kps_nor = curr_kp - mean  # normalize
        kps_nor = kps_nor.reshape(-1)  # flatten

        curr_sequence = np.zeros((1, 1, features))
        curr_sequence[0, 0, :] = kps_nor
        sequence = np.hstack((sequence, curr_sequence))

        predicted = model.predict(sequence)
        predicted = predicted[0, :].reshape(17, 3)
        predicted = predicted + mean

        kps = curr_kp.copy()
        if normalize:
            kps = ComputeMetric.normalize_kps(kps, image.shape)


        metrics.update(kps)

        sum_ = 0
        for i in range(len(nonstat)):
            x = (metrics.state[nonstat[i]] * as_ratio[1]) / metrics.state["shl_dist"]
            lpftrack[i].update([x], alpha=0.5)
            track[i].append(lpftrack[i]()[0])
            sum_ += lpftrack[i]()[0]

        overall_signal.append(sum_)
        zc.update(sum_)

        # ax.plot(overall_signal)
        # fig.canvas.draw()
        # fig.canvas.flush_events()

        current = zc.checkCross()
        checkzc.append(current)

        if prev == 0 and current == 1:
            reps += 1

        prev = current

        # output = draw_pose(image, curr_kp, preprocess=False)
        output = draw_pose(image, predicted, preprocess=False)
        output = cv2.cvtColor(output, cv2.COLOR_BGR2RGB)
        cv2.putText(output, f'reps: {reps}', (10, 20),
                    cv2.FONT_HERSHEY_SIMPLEX, 0.45,
                    (22, 160, 133), 1)

        outimage = np.asarray(output, dtype=np.uint8)
        outimage = cv2.resize(outimage, size)

        video_writer.write(outimage)
        cv2.imshow("frame", outimage)

        k = cv2.waitKey(1)
        if k == ord('q') or k == 27:
            break

    cap.release()
    cv2.destroyAllWindows()


if __name__ == "__main__":
    import argparse

    parser = argparse.ArgumentParser()
    parser.add_argument("--video", help="video or digit for camera, defaults to 0", required=False, default="0")
    parser.add_argument("--normalize", help="Normalize keypoints (y/n)", required=False, default='n')
    parser.add_argument("--reference", help="reference config key", required=False, default='reference_config_key_1')
    args = parser.parse_args()

    main(args.video, args.reference, args.normalize.lower() == 'y')
