import Planche from './img/planche.jpg'
import Handstand from './img/handstand.jpg'
import AI from './img/ai.jpg'
import "./Slider.js";

export const Home = () => {
    return (
        <>
            <header className="bg-dark">
                <div className="container pt-4 pt-xl-5">
                    <div className="row pt-5">
                        <div className="col-md-8 col-xl-6 text-center text-md-start mx-auto">
                            <div className="text-center">
                                <p className="fw-bold text-success mb-2">The first ever</p>
                                <h1 className="fw-bold">AI - powered calisthenics coach</h1>
                            </div>
                        </div>
                        <div className="col-12 col-lg-10 mx-auto">
                            <div
                                className="position-relative"
                                style={{
                                    display: "flex",
                                    flexWrap: "wrap",
                                    justifyContent: "flex-end"
                                }}
                            >
                                <div
                                    style={{
                                        position: "relative",
                                        flex: "0 0 45%",
                                        transform: "translate3d(-15%, 35%, 0)"
                                    }}
                                >
                                    <img
                                        className="img-fluid"
                                        data-bss-parallax=""
                                        data-bss-parallax-speed="0.8"
                                        src={Handstand}
                                    />
                                </div>
                                <div
                                    style={{
                                        position: "relative",
                                        flex: "0 0 45%",
                                        transform: "translate3d(-5%, 20%, 0)"
                                    }}
                                >
                                    <img
                                        className="img-fluid"
                                        data-bss-parallax=""
                                        data-bss-parallax-speed="0.4"
                                        src={AI}
                                        width={313}
                                        height={209}
                                    />
                                </div>
                                <div
                                    style={{
                                        position: "relative",
                                        flex: "0 0 60%",
                                        transform: "translate3d(0, 0%, 0)"
                                    }}
                                >
                                    <img
                                        className="img-fluid"
                                        data-bss-parallax=""
                                        data-bss-parallax-speed="0.25"
                                        src={Planche}
                                    />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </header>
            <section className="py-5">
                <div className="container text-center py-5">
                    <p className="mb-4" style={{ fontSize: "1.6rem" }}>
                        Used by&nbsp;
                        <span className="text-success">
          <strong>top athletes</strong>
        </span>
                        &nbsp;from all over Bulgaria.
                    </p>
                    <a href="#">
                        {" "}
                        <img className="m-3" src="assets/img/brands/google.png" />
                    </a>
                    <a href="#">
                        {" "}
                        <img className="m-3" src="assets/img/brands/microsoft.png" />
                    </a>
                    <a href="#">
                        {" "}
                        <img className="m-3" src="assets/img/brands/apple.png" />
                    </a>
                    <a href="#">
                        {" "}
                        <img className="m-3" src="assets/img/brands/facebook.png" />
                    </a>
                    <a href="#">
                        {" "}
                        <img className="m-3" src="assets/img/brands/twitter.png" />
                    </a>
                </div>
            </section>
            <section>
                <div className="container bg-dark py-5">
                    <div className="row">
                        <div className="col-md-8 col-xl-6 text-center mx-auto">
                            <p className="fw-bold text-success mb-2">Our Services</p>
                            <h3 className="fw-bold">What we can do for you</h3>
                        </div>
                    </div>
                    <div className="py-5 p-lg-5">
                        <div
                            className="row row-cols-1 row-cols-md-2 mx-auto"
                            style={{ maxWidth: 900 }}
                        >
                            <div className="col mb-5">
                                <div className="card shadow-sm">
                                    <div className="card-body px-4 py-5 px-md-5">
                                        <div
                                            className="bs-icon-lg d-flex justify-content-center align-items-center mb-3 bs-icon"
                                            style={{ top: "1rem", right: "1rem", position: "absolute" }}
                                        >
                                            <svg
                                                xmlns="http://www.w3.org/2000/svg"
                                                width="1em"
                                                height="1em"
                                                fill="currentColor"
                                                viewBox="0 0 16 16"
                                                className="bi bi-bezier text-success"
                                            >
                                                <path
                                                    fillRule="evenodd"
                                                    d="M0 10.5A1.5 1.5 0 0 1 1.5 9h1A1.5 1.5 0 0 1 4 10.5v1A1.5 1.5 0 0 1 2.5 13h-1A1.5 1.5 0 0 1 0 11.5v-1zm1.5-.5a.5.5 0 0 0-.5.5v1a.5.5 0 0 0 .5.5h1a.5.5 0 0 0 .5-.5v-1a.5.5 0 0 0-.5-.5h-1zm10.5.5A1.5 1.5 0 0 1 13.5 9h1a1.5 1.5 0 0 1 1.5 1.5v1a1.5 1.5 0 0 1-1.5 1.5h-1a1.5 1.5 0 0 1-1.5-1.5v-1zm1.5-.5a.5.5 0 0 0-.5.5v1a.5.5 0 0 0 .5.5h1a.5.5 0 0 0 .5-.5v-1a.5.5 0 0 0-.5-.5h-1zM6 4.5A1.5 1.5 0 0 1 7.5 3h1A1.5 1.5 0 0 1 10 4.5v1A1.5 1.5 0 0 1 8.5 7h-1A1.5 1.5 0 0 1 6 5.5v-1zM7.5 4a.5.5 0 0 0-.5.5v1a.5.5 0 0 0 .5.5h1a.5.5 0 0 0 .5-.5v-1a.5.5 0 0 0-.5-.5h-1z"
                                                />
                                                <path d="M6 4.5H1.866a1 1 0 1 0 0 1h2.668A6.517 6.517 0 0 0 1.814 9H2.5c.123 0 .244.015.358.043a5.517 5.517 0 0 1 3.185-3.185A1.503 1.503 0 0 1 6 5.5v-1zm3.957 1.358A1.5 1.5 0 0 0 10 5.5v-1h4.134a1 1 0 1 1 0 1h-2.668a6.517 6.517 0 0 1 2.72 3.5H13.5c-.123 0-.243.015-.358.043a5.517 5.517 0 0 0-3.185-3.185z" />
                                            </svg>
                                        </div>
                                        <h5 className="fw-bold card-title">
                                            Connect with other athletes
                                        </h5>
                                        <p className="text-muted card-text mb-4">
                                            Erat netus est hendrerit, nullam et quis ad cras porttitor
                                            iaculis. Bibendum vulputate cras aenean.
                                        </p>
                                        <button className="btn btn-primary shadow" type="button">
                                            Learn more
                                        </button>
                                    </div>
                                </div>
                            </div>
                            <div className="col mb-5">
                                <div className="card shadow-sm">
                                    <div className="card-body px-4 py-5 px-md-5">
                                        <div
                                            className="bs-icon-lg d-flex justify-content-center align-items-center mb-3 bs-icon"
                                            style={{ top: "1rem", right: "1rem", position: "absolute" }}
                                        >
                                            <svg
                                                xmlns="http://www.w3.org/2000/svg"
                                                width="1em"
                                                height="1em"
                                                fill="currentColor"
                                                viewBox="0 0 16 16"
                                                className="bi bi-lightning-charge text-success"
                                            >
                                                <path d="M11.251.068a.5.5 0 0 1 .227.58L9.677 6.5H13a.5.5 0 0 1 .364.843l-8 8.5a.5.5 0 0 1-.842-.49L6.323 9.5H3a.5.5 0 0 1-.364-.843l8-8.5a.5.5 0 0 1 .615-.09zM4.157 8.5H7a.5.5 0 0 1 .478.647L6.11 13.59l5.732-6.09H9a.5.5 0 0 1-.478-.647L9.89 2.41 4.157 8.5z" />
                                            </svg>
                                        </div>
                                        <h5 className="fw-bold card-title">Elevate your workouts</h5>
                                        <p className="text-muted card-text mb-4">
                                            Erat netus est hendrerit, nullam et quis ad cras porttitor
                                            iaculis. Bibendum vulputate cras aenean.
                                        </p>
                                        <button className="btn btn-primary shadow" type="button">
                                            Learn more
                                        </button>
                                    </div>
                                </div>
                            </div>
                            <div className="col mb-4">
                                <div className="card shadow-sm">
                                    <div className="card-body px-4 py-5 px-md-5">
                                        <div
                                            className="bs-icon-lg d-flex justify-content-center align-items-center mb-3 bs-icon"
                                            style={{ top: "1rem", right: "1rem", position: "absolute" }}
                                        >
                                            <svg
                                                xmlns="http://www.w3.org/2000/svg"
                                                width="1em"
                                                height="1em"
                                                fill="currentColor"
                                                viewBox="0 0 16 16"
                                                className="bi bi-eye text-success"
                                                style={{ fontSize: 32 }}
                                            >
                                                <path d="M16 8s-3-5.5-8-5.5S0 8 0 8s3 5.5 8 5.5S16 8 16 8zM1.173 8a13.133 13.133 0 0 1 1.66-2.043C4.12 4.668 5.88 3.5 8 3.5c2.12 0 3.879 1.168 5.168 2.457A13.133 13.133 0 0 1 14.828 8c-.058.087-.122.183-.195.288-.335.48-.83 1.12-1.465 1.755C11.879 11.332 10.119 12.5 8 12.5c-2.12 0-3.879-1.168-5.168-2.457A13.134 13.134 0 0 1 1.172 8z" />
                                                <path d="M8 5.5a2.5 2.5 0 1 0 0 5 2.5 2.5 0 0 0 0-5zM4.5 8a3.5 3.5 0 1 1 7 0 3.5 3.5 0 0 1-7 0z" />
                                            </svg>
                                        </div>
                                        <h5 className="fw-bold card-title">Get real-time evaluation</h5>
                                        <p className="text-muted card-text mb-4">
                                            Erat netus est hendrerit, nullam et quis ad cras porttitor
                                            iaculis. Bibendum vulputate cras aenean.
                                        </p>
                                        <button className="btn btn-primary shadow" type="button">
                                            Learn more
                                        </button>
                                    </div>
                                </div>
                            </div>
                            <div className="col mb-4">
                                <div className="card shadow-sm">
                                    <div className="card-body px-4 py-5 px-md-5">
                                        <div
                                            className="bs-icon-lg d-flex justify-content-center align-items-center mb-3 bs-icon"
                                            style={{ top: "1rem", right: "1rem", position: "absolute" }}
                                        >
                                            <svg
                                                xmlns="http://www.w3.org/2000/svg"
                                                width="1em"
                                                height="1em"
                                                fill="currentColor"
                                                viewBox="0 0 16 16"
                                                className="bi bi-book text-success"
                                            >
                                                <path d="M1 2.828c.885-.37 2.154-.769 3.388-.893 1.33-.134 2.458.063 3.112.752v9.746c-.935-.53-2.12-.603-3.213-.493-1.18.12-2.37.461-3.287.811V2.828zm7.5-.141c.654-.689 1.782-.886 3.112-.752 1.234.124 2.503.523 3.388.893v9.923c-.918-.35-2.107-.692-3.287-.81-1.094-.111-2.278-.039-3.213.492V2.687zM8 1.783C7.015.936 5.587.81 4.287.94c-1.514.153-3.042.672-3.994 1.105A.5.5 0 0 0 0 2.5v11a.5.5 0 0 0 .707.455c.882-.4 2.303-.881 3.68-1.02 1.409-.142 2.59.087 3.223.877a.5.5 0 0 0 .78 0c.633-.79 1.814-1.019 3.222-.877 1.378.139 2.8.62 3.681 1.02A.5.5 0 0 0 16 13.5v-11a.5.5 0 0 0-.293-.455c-.952-.433-2.48-.952-3.994-1.105C10.413.809 8.985.936 8 1.783z" />
                                            </svg>
                                        </div>
                                        <h5 className="fw-bold card-title">Gain more knowledge</h5>
                                        <p className="text-muted card-text mb-4">
                                            Erat netus est hendrerit, nullam et quis ad cras porttitor
                                            iaculis. Bibendum vulputate cras aenean.
                                        </p>
                                        <button className="btn btn-primary shadow" type="button">
                                            Learn more
                                        </button>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </section>
            <section>
                <div className="container py-5">
                    <div className="mx-auto" style={{ maxWidth: 900 }}>
                        <div className="row row-cols-1 row-cols-md-2 d-flex justify-content-center">
                            <div className="col mb-4">
                                <div className="card bg-primary-light">
                                    <div className="card-body text-center px-4 py-5 px-md-5">
                                        <p className="fw-bold text-primary card-text mb-2">
                                            Innovative
                                        </p>
                                        <h5 className="fw-bold card-title mb-3">
                                            Powered by the Axon framework
                                        </h5>
                                        <button className="btn btn-primary btn-sm" type="button">
                                            Learn more
                                        </button>
                                    </div>
                                </div>
                            </div>
                            <div className="col mb-4">
                                <div className="card bg-secondary-light">
                                    <div className="card-body text-center px-4 py-5 px-md-5">
                                        <p className="fw-bold text-secondary card-text mb-2">
                                            Performant
                                        </p>
                                        <h5 className="fw-bold card-title mb-3">
                                            Uses cloud acceleration
                                        </h5>
                                        <button className="btn btn-secondary btn-sm" type="button">
                                            Learn more
                                        </button>
                                    </div>
                                </div>
                            </div>
                            <div className="col mb-4">
                                <div className="card bg-info-light">
                                    <div className="card-body text-center px-4 py-5 px-md-5">
                                        <p className="fw-bold text-info card-text mb-2">Champion</p>
                                        <h5 className="fw-bold card-title mb-3">Multiple awards</h5>
                                        <button className="btn btn-info btn-sm" type="button">
                                            Learn more
                                        </button>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </section>
            <section className="py-5 mt-5">
                <div className="container py-5">
                    <div className="row mb-5">
                        <div className="col-md-8 col-xl-6 text-center mx-auto">
                            <p className="fw-bold text-success mb-2">Testimonials</p>
                            <h2 className="fw-bold">
                                <strong>What People Say About us</strong>
                            </h2>
                            <p className="text-muted">
                                No matter the project, our team can handle it.&nbsp;
                            </p>
                        </div>
                    </div>
                    <div className="row row-cols-1 row-cols-sm-2 row-cols-lg-3 d-sm-flex justify-content-sm-center">
                        <div className="col mb-4">
                            <div className="d-flex flex-column align-items-center align-items-sm-start">
                                <p className="bg-dark border rounded border-dark p-4">
                                    Nisi sit justo faucibus nec ornare amet, tortor torquent. Blandit
                                    class dapibus, aliquet morbi.
                                </p>
                                <div className="d-flex">
                                    <img
                                        className="rounded-circle flex-shrink-0 me-3 fit-cover"
                                        width={50}
                                        height={50}
                                        src="assets/img/team/avatar2.jpg"
                                    />
                                    <div>
                                        <p className="fw-bold text-primary mb-0">John Smith</p>
                                        <p className="text-muted mb-0">Erat netus</p>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div className="col mb-4">
                            <div className="d-flex flex-column align-items-center align-items-sm-start">
                                <p className="bg-dark border rounded border-dark p-4">
                                    Nisi sit justo faucibus nec ornare amet, tortor torquent. Blandit
                                    class dapibus, aliquet morbi.
                                </p>
                                <div className="d-flex">
                                    <img
                                        className="rounded-circle flex-shrink-0 me-3 fit-cover"
                                        width={50}
                                        height={50}
                                        src="assets/img/team/avatar4.jpg"
                                    />
                                    <div>
                                        <p className="fw-bold text-primary mb-0">John Smith</p>
                                        <p className="text-muted mb-0">Erat netus</p>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div className="col mb-4">
                            <div className="d-flex flex-column align-items-center align-items-sm-start">
                                <p className="bg-dark border rounded border-dark p-4">
                                    Nisi sit justo faucibus nec ornare amet, tortor torquent. Blandit
                                    class dapibus, aliquet morbi.
                                </p>
                                <div className="d-flex">
                                    <img
                                        className="rounded-circle flex-shrink-0 me-3 fit-cover"
                                        width={50}
                                        height={50}
                                        src="assets/img/team/avatar5.jpg"
                                    />
                                    <div>
                                        <p className="fw-bold text-primary mb-0">John Smith</p>
                                        <p className="text-muted mb-0">Erat netus</p>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </section>
            <section className="py-5">
                <div className="container">
                    <div className="row mb-5">
                        <div className="col-md-8 col-xl-6 text-center mx-auto">
                            <p className="fw-bold text-success mb-2">Contacts</p>
                            <h2 className="fw-bold">How you can reach us</h2>
                        </div>
                    </div>
                    <div className="row d-flex justify-content-center">
                        <div className="col-md-6 col-xl-4">
                            <div>
                                <form className="p-3 p-xl-4" method="post">
                                    <div className="mb-3">
                                        <input
                                            className="form-control"
                                            type="text"
                                            id="name-1"
                                            name="name"
                                            placeholder="Name"
                                        />
                                    </div>
                                    <div className="mb-3">
                                        <input
                                            className="form-control"
                                            type="email"
                                            id="email-1"
                                            name="email"
                                            placeholder="Email"
                                        />
                                    </div>
                                    <div className="mb-3">
                <textarea
                    className="form-control"
                    id="message-1"
                    name="message"
                    rows={6}
                    placeholder="Message"
                    defaultValue={""}
                />
                                    </div>
                                    <div>
                                        <button
                                            className="btn btn-primary shadow d-block w-100"
                                            type="submit"
                                        >
                                            Send{" "}
                                        </button>
                                    </div>
                                </form>
                            </div>
                        </div>
                        <div className="col-md-4 col-xl-4 d-flex justify-content-center justify-content-xl-start">
                            <div className="d-flex flex-wrap flex-md-column justify-content-md-start align-items-md-start h-100">
                                <div className="d-flex align-items-center p-3">
                                    <div className="bs-icon-md bs-icon-circle bs-icon-primary shadow d-flex flex-shrink-0 justify-content-center align-items-center d-inline-block bs-icon bs-icon-md">
                                        <svg
                                            xmlns="http://www.w3.org/2000/svg"
                                            width="1em"
                                            height="1em"
                                            fill="currentColor"
                                            viewBox="0 0 16 16"
                                            className="bi bi-envelope"
                                        >
                                            <path
                                                fillRule="evenodd"
                                                d="M0 4a2 2 0 0 1 2-2h12a2 2 0 0 1 2 2v8a2 2 0 0 1-2 2H2a2 2 0 0 1-2-2V4Zm2-1a1 1 0 0 0-1 1v.217l7 4.2 7-4.2V4a1 1 0 0 0-1-1H2Zm13 2.383-4.708 2.825L15 11.105V5.383Zm-.034 6.876-5.64-3.471L8 9.583l-1.326-.795-5.64 3.47A1 1 0 0 0 2 13h12a1 1 0 0 0 .966-.741ZM1 11.105l4.708-2.897L1 5.383v5.722Z"
                                            />
                                        </svg>
                                    </div>
                                    <div className="px-2">
                                        <h6 className="fw-bold mb-0">Email</h6>
                                        <p className="text-muted mb-0">info@example.com</p>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </section>
            <section className="py-5" />
        </>

    )
}