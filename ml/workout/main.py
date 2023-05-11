import numpy as np
import pandas
import pandas as pd
import random

df = pandas.read_csv("training.csv", header=0)
dataset = [[], []]

idx = 0
workout = []
i = 0
for row in df.iterrows():
    exercise = row[1].Exercise
    progression = row[1].Progression
    sets = row[1].Sets
    reps = row[1].Reps

    entry = [exercise, progression, sets, reps]
    workout.append(entry)
    i += 1
    if i == 7:
        if idx == 1:
            workout.append(["", "_end", 0, 0])
        dataset[idx].append(workout)

        idx = 1 - idx
        i = 0
        workout = []
        if idx == 1:
            workout.append(["", "start_", 0, 0])

x = dataset[0]
y = dataset[1]
input_vocab = set()
for workout in x:
    for ex in workout:
        name = ex[1]
        if name not in input_vocab:
            input_vocab.add(name)

target_vocab = set()
for workout in y:
    for ex in workout:
        name = ex[1]
        if name not in target_vocab:
            target_vocab.add(name)

input_length = 7
target_length = input_length + 2
samples = 500

print(input_vocab)
print(target_vocab)

input_names = sorted(list(input_vocab))
target_names = sorted(list(target_vocab))
num_encoder_tokens = len(input_vocab)
num_decoder_tokens = len(target_vocab)

input_token_index = dict(
    [(word, i) for i, word in enumerate(input_names)])
target_token_index = dict(
    [(word, i) for i, word in enumerate(target_names)])

encoder_input_data = np.zeros(
    (samples, input_length),
    dtype='float32')
decoder_input_data = np.zeros(
    (samples, target_length),
    dtype='float32')
decoder_target_data = np.zeros(
    (samples, target_length, num_decoder_tokens),
    dtype='float32')

for i, (input, target) in enumerate(zip(x, y)):
    for t, workout in enumerate(input):
        encoder_input_data[i, t] = input_token_index[workout[1]]
    for t, workout in enumerate(target):
        # decoder_target_data is ahead of decoder_input_data by one timestep
        decoder_input_data[i, t] = target_token_index[workout[1]]
        if t > 0:
            # decoder_target_data will be ahead by one timestep
            # and will not include the start character.
            decoder_target_data[i, t - 1, target_token_index[workout[1]]] = 1.

from keras.layers import Input, LSTM, Embedding, Dense, Bidirectional, Concatenate, Dropout
from keras.models import Model
from keras.utils import plot_model

embedding_size = 30
latent_dim = 128
batch_size = 8
epochs = 70

encoder_inputs = Input(shape=(None,))
en_x = Embedding(num_encoder_tokens, embedding_size)(encoder_inputs)

encoder1 = Bidirectional(LSTM(latent_dim, return_sequences=True, return_state=True))
encoder_outputs1, forward_h1, forward_c1, backward_h1, backward_c1 = encoder1(en_x)
encoder_outputs1 = Dropout(0.4)(encoder_outputs1)

encoder2 = Bidirectional(LSTM(latent_dim, return_state=True))
encoder_outputs2, forward_h2, forward_c2, backward_h2, backward_c2 = encoder2(encoder_outputs1)
encoder_outputs2 = Dropout(0.4)(encoder_outputs2)

state_h = Concatenate()([forward_h1, backward_h1, forward_h2, backward_h2])
state_c = Concatenate()([forward_c1, backward_c1, forward_c2, backward_c2])
encoder_states = [state_h, state_c]


# Set up the decoder, using `encoder_states` as initial state.
decoder_inputs = Input(shape=(None,))

dex = Embedding(num_decoder_tokens, embedding_size)

final_dex = dex(decoder_inputs)

decoder_lstm = LSTM(latent_dim * 4, return_sequences=True, return_state=True)

decoder_outputs, _, _ = decoder_lstm(final_dex,
                                     initial_state=encoder_states)

decoder_outputs = Dropout(0.2)(decoder_outputs)
decoder_dense = Dense(num_decoder_tokens, activation='softmax')

decoder_outputs = decoder_dense(decoder_outputs)

model = Model([encoder_inputs, decoder_inputs], decoder_outputs)

model.compile(optimizer='rmsprop', loss='categorical_crossentropy', metrics=['acc'])

model.fit([encoder_input_data, decoder_input_data], decoder_target_data,
          batch_size=batch_size,
          epochs=epochs,
          validation_split=0.05)

encoder_model = Model(encoder_inputs, encoder_states)

decoder_state_input_h = Input(shape=(latent_dim * 4,))
decoder_state_input_c = Input(shape=(latent_dim * 4,))
decoder_states_inputs = [decoder_state_input_h, decoder_state_input_c]

final_dex2 = dex(decoder_inputs)

decoder_outputs2, state_h2, state_c2 = decoder_lstm(final_dex2, initial_state=decoder_states_inputs)
decoder_states2 = [state_h2, state_c2]

decoder_outputs2 = Dropout(0.4)(decoder_outputs2)
decoder_outputs2 = decoder_dense(decoder_outputs2)
decoder_model = Model(
    [decoder_inputs] + decoder_states_inputs,
    [decoder_outputs2] + decoder_states2)

# Reverse-lookup token index to decode sequences back to
# something readable.
reverse_input_char_index = dict(
    (i, char) for char, i in input_token_index.items())
reverse_target_char_index = dict(
    (i, char) for char, i in target_token_index.items())


def decode_sequence(input_seq):
    # Encode the input as state vectors.
    states_value = encoder_model.predict(input_seq)
    # Generate empty target sequence of length 1.
    target_seq = np.zeros((1, 1))
    # Populate the first character of target sequence with the start character.
    target_seq[0, 0] = target_token_index['start_']

    # Sampling loop for a batch of sequences
    # (to simplify, here we assume a batch of size 1).
    stop_condition = False
    decoded_sentence = ''
    i = 0
    while not stop_condition:
        output_tokens, h, c = decoder_model.predict(
            [target_seq] + states_value)

        # Sample a token
        sampled_token_index = np.argmax(output_tokens[0, -1, :])
        sampled_char = reverse_target_char_index[sampled_token_index]
        decoded_sentence += ' ' + sampled_char
        i += 1
        # Exit condition: either hit max length
        # or find stop character.
        if (sampled_char == '_end' or i > target_length):
            stop_condition = True
            i = 0

        # Update the target sequence (of length 1).
        target_seq = np.zeros((1, 1))
        target_seq[0, 0] = sampled_token_index

        # Update states
        states_value = [h, c]

    return decoded_sentence


for seq_index in range(20):
    input_seq = encoder_input_data[seq_index: seq_index + 1]
    decoded_sentence = decode_sequence(input_seq)
    print('-')
    print('Input sentence:', x[seq_index: seq_index + 1])
    print('Decoded sentence:', decoded_sentence)
