import "../../bootstrap.min.css"
import Back from "./img/back.png"
import Chest from "./img/chest.png"

export const Muscle = () => {

    return (

        <section className="py-5">
            <div className="container py-5">
                <div className="row mb-5">
                    <div className="col-md-8 col-xl-6 text-center mx-auto">
                        <h2 className="fw-bold">Muscle groups</h2>
                        <p className="text-muted">
                            Select muscle group to learn more about the biology, function and
                            target exercises.
                        </p>
                    </div>
                </div>
                <div
                    className="row row-cols-1 row-cols-md-2 mx-auto"
                    style={{ maxWidth: 900 }}
                >
                    <div className="col mb-4">
                        <div>
                            <a href="#">
                                <img
                                    className="rounded img-fluid shadow w-100 fit-cover"
                                    src={Chest}
                                    style={{ height: 250 }}
                                />
                            </a>
                            <div className="py-4">
                                <span className="badge bg-primary mb-2">Upper body</span>
                                <h4 className="fw-bold">Chest</h4>
                                <p className="text-muted">
                                    The main function of this chest muscle as a whole is the adduction
                                    and internal rotation of the arm on the shoulder joint.&nbsp;
                                    <br />
                                    <br />
                                    <br />
                                    <br />
                                </p>
                            </div>
                        </div>
                    </div>
                    <div className="col mb-4">
                        <div>
                            <a href="#">
                                <img
                                    className="rounded img-fluid shadow w-100 fit-cover"
                                    src={Back}
                                    style={{ height: 250 }}
                                />
                            </a>
                            <div className="py-4">
                                <span className="badge bg-primary mb-2">Upper body</span>
                                <h4 className="fw-bold">Back</h4>
                                <p className="text-muted">
                                    Your back muscles are the main structural support for your trunk
                                    (torso). These muscles help you move your body, including your
                                    head, neck, shoulders, arms and legs.&nbsp;
                                </p>
                            </div>
                        </div>
                    </div>
                </div>
                <div
                    className="row row-cols-1 row-cols-md-2 mx-auto"
                    style={{ maxWidth: 900 }}
                >
                    <div className="col mb-4">
                        <div>
                            <a href="#">
                                <img
                                    className="rounded img-fluid shadow w-100 fit-cover"
                                    src={Chest}
                                    style={{ height: 250 }}
                                />
                            </a>
                            <div className="py-4">
                                <span className="badge bg-primary mb-2">Lower body</span>
                                <h4 className="fw-bold">Legs</h4>
                                <p className="text-muted">
                                    What is the purpose of the leg muscles? Your leg muscles help you
                                    move, carry the weight of your body and support you when you
                                    stand.
                                </p>
                            </div>
                        </div>
                    </div>
                    <div className="col mb-4">
                        <div>
                            <a href="#">
                                <img
                                    className="rounded img-fluid shadow w-100 fit-cover"
                                    src={Chest}
                                    style={{ height: 250 }}
                                />
                            </a>
                            <div className="py-4">
                                <span className="badge bg-primary mb-2">Upper body</span>
                                <h4 className="fw-bold">Abdomen</h4>
                                <p className="text-muted">
                                    The abdominal muscles support the trunk, allow movement and hold
                                    organs in place by regulating internal abdominal pressure.
                                </p>
                            </div>
                        </div>
                    </div>
                    <div className="col mb-4">
                        <div>
                            <a href="#">
                                <img
                                    className="rounded img-fluid shadow w-100 fit-cover"
                                    src="assets/img/products/2.jpg"
                                    style={{ height: 250 }}
                                />
                            </a>
                            <div className="py-4">
                                <span className="badge bg-primary mb-2">Upper limbs</span>
                                <h4 className="fw-bold">Arms</h4>
                                <p className="text-muted">
                                    Your arm muscles help you move your arms, hands, fingers and
                                    thumbs. You have many muscles in your upper arm and forearm.
                                </p>
                            </div>
                        </div>
                    </div>
                    <div className="col mb-4">
                        <div>
                            <a href="#">
                                <img
                                    className="rounded img-fluid shadow w-100 fit-cover"
                                    src="assets/img/products/3.jpg"
                                    style={{ height: 250 }}
                                />
                            </a>
                            <div className="py-4">
                                <span className="badge bg-primary mb-2">Upper limbs</span>
                                <h4 className="fw-bold">Shoulders</h4>
                                <p className="text-muted">
                                    Your shoulder muscles support and stabilize the most flexible
                                    joint in your body. They help you perform a wide range of
                                    movements.
                                    <br />
                                    <br />
                                    <br />
                                </p>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>

    );
}