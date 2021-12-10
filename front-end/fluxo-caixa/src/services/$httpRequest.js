import axios from "axios";

const _baseURL = process.env.VUE_APP_BASE_URL;
console.log(process.env);
const _defaultHeaders = {
    headers: {
        "Content-Type": "application/json"
    },
}
export default {
    setHeaders: (key, value) => {
        _defaultHeaders.headers[key] = value;
    },
    post: (context, payload) => {
        return new Promise((resolve, reject) => {
            axios.post(
                `${_baseURL}${context}`,
                payload,
                _defaultHeaders
            ).then((response) => {
                resolve(response.data);
            }).catch(error => {
                reject(error);
            });
        })
    },
    put: (context, payload) => {
        return new Promise((resolve, reject) => {
            axios.put(
                `${_baseURL}${context}`,
                JSON.stringify(payload),
                _defaultHeaders
            ).then((response) => {
                resolve(response.data);
            }).catch(error => {
                reject(error);
            });
        })
    },
    path: (context, payload) => {
        return axios.patch(
            `${_baseURL}${context}`,
            JSON.stringify(payload),
            _defaultHeaders
        )
    },
    get: (url) => {
        return axios.get(
            url,
        )
    },
    delete: (context) => {
        return axios.delete(
            `${_baseURL}${context}`,
        )
    },
}