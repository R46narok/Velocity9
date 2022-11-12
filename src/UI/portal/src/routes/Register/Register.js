import {useState} from "react";
import {useDispatch, useSelector} from "react-redux";
import {userActions} from "../../actions/userActions";
import { useNavigate } from 'react-router-dom';
import "../../bootstrap.min.css"

export const Register = () => {
    const [userName, setUserName] = useState("");
    const [password, setPassword] = useState("");
    const [email, setEmail] = useState("");

    const navigate = useNavigate();
    const dispatch = useDispatch();

    const handleButtonClick = async (e) => {
        e.preventDefault()
        dispatch(userActions.register(userName, email, password));

        navigate("/");
    }

    return (

        <section className="py-5">
            <div className="container py-5">
                <div className="row mb-5">
                    <div className="col-md-8 col-xl-6 text-center mx-auto">
                        <p className="fw-bold text-success mb-2">Sign up</p>
                        <h2 className="fw-bold">Join the trend</h2>
                    </div>
                </div>
                <div className="row d-flex justify-content-center">
                    <div className="col-md-6 col-xl-4">
                        <div>
                            <form className="p-3 p-xl-4" onSubmit={handleButtonClick}>
                                <div className="mb-3">
                                    <input
                                        className="form-control"
                                        type="text"
                                        placeholder="Username"
                                        onChange={e => setUserName(e.target.value)}
                                    />
                                </div>
                                <div className="mb-3">
                                    <input
                                        className="form-control"
                                        type="email"
                                        placeholder="Email"
                                        onChange={e => setEmail(e.target.value)}
                                    />
                                </div>
                                <div className="mb-3">
                                    <input
                                        className="form-control"
                                        type="password"
                                        placeholder="Password"
                                        onChange={e => setPassword(e.target.value)}
                                    />
                                </div>
                                <div className="mb-3" />
                                <div>
                                    <button
                                        className="btn btn-primary shadow d-block w-100"
                                        type="submit">
                                        Sign up{" "}
                                    </button>
                                </div>
                            </form>
                        </div>
                    </div>
                </div>
            </div>
        </section>

    );
};