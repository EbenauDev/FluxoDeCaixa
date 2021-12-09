import axios from "axios";

const _defaultHeaders = {
    headers: {
        "Content-Type": "application/json"
    },
}
export default {
    setHeaders: (key, value) => {
        _defaultHeaders.headers[key] = value;
    },
    post: (url, payload) => {
        return new Promise((resolve, reject) => {
            axios.post(
                url,
                payload,
                _defaultHeaders
            ).then((response) => {
                resolve(response.data);
            }).catch(error => {
                reject(error);
            });
        })
    },
    put: (url, payload) => {
        return new Promise((resolve, reject) => {
            axios.put(
                url,
                JSON.stringify(payload),
                _defaultHeaders
            ).then((response) => {
                resolve(response.data);
            }).catch(error => {
                reject(error);
            });
        })
    },
    path: (url, payload) => {
        return axios.patch(
            url,
            JSON.stringify(payload),
            _defaultHeaders
        )
    },
    get: (url) => {
        return axios.get(
            url,
        )
    },
    delete: (url) => {
        return axios.delete(
            url,
        )
    },
}