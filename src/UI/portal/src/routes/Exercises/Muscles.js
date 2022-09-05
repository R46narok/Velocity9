import {useState} from "react";
import {muscleService} from "../../services/muscleService";

export const Muscles = () => {
    const [muscles, setMuscles] = useState([]);

    const [group, setGroup] = useState("");
    const [description, setDescription] = useState("");
    const [headCount, setHeadCount] = useState(0);

    const handleButtonClick = async (e) => {
        e.preventDefault();
        setMuscles(await muscleService.load())
    }

    const handleSubmit = async (e) => {
        await muscleService.add(group, description, headCount);
    }

    return (
        <>
            <form onSubmit={handleSubmit}>
                <div className="form-group first">
                    <label htmlFor="username">Group</label>
                    <input type="text" className="form-control" id="group" onChange={(e) => setGroup(e.target.value)}/>

                </div>

                <div className="form-group second mb-4">
                    <label htmlFor="text">Head count</label>
                    <input type="number" className="form-control" id="num" onChange={(e) => setHeadCount(e.target.value)}/>
                </div>

                <div className="form-group last mb-4">
                    <label htmlFor="text">Description</label>
                    <input type="text" className="form-control" id="desc" onChange={(e) => setDescription(e.target.value)}/>
                </div>

                <input type="submit" value="Add" className="btn btn-block btn-primary"/>
            </form>

            <table className="table">
                <thead>
                <tr>
                    <th scope="col">Group</th>
                    <th scope="col">Description</th>
                    <th scope="col">Head count</th>
                </tr>
                </thead>
                <tbody>
                {
                    muscles.map(m => {
                        return (<tr>
                            <th scope="row">{m.group}</th>
                            <td>{m.description}</td>
                            <td>{m.headCount}</td>
                        </tr>)
                    })
                }

                </tbody>
            </table>
            <button className="btn btn-danger" onClick={handleButtonClick}>Reload</button>
        </>
    )
}