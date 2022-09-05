export const muscleService = {
    load, add
}

function load() {
    const requestOptions = {
        method: "GET",

    }
    return fetch("http://localhost:6001/api/Muscle/GetAll", requestOptions)
        .then(handleResponse);
}

function add(group, description, headCount) {
    const requestOptions = {
        method: "POST",
        headers : {
            "Content-Type": "application/json"
        },
        body: JSON.stringify({group, description, headCount})
    }

    return fetch("http://localhost:6001/api/Muscle", requestOptions)
        .then(handleResponse);
}

function handleResponse(response) {
    return response.text().then(text => {
        const data = text && JSON.parse(text);
        if (!response.ok) {

            const error = (data && data.message) || response.statusText;
            return Promise.reject(error);
        }

        return data.result;
    });
}
