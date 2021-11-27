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
        return axios.post(
            url,
            payload,
            _defaultHeaders
        )
    },
    put: (url, payload) => {
        return axios.put(
            url,
            JSON.stringify(payload),
            _defaultHeaders
        )
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