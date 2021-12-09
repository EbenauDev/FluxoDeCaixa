export default {
    setSessionToken(tokenJWt) {
        if (window && window.sessionStorage) {
            window.sessionStorage.setItem("sessionToken", tokenJWt);
        } else {
            throw new Error("sessionStorage is not defined!");
        }
    },
    deleteSessionToken() {
        if (window && window.sessionStorage) {
            window.sessionStorage.removeItem("sessionToken");
        } else {
            throw new Error("sessionStorage is not defined!");
        }
    },
    getTokenSession() {
        if (window && window.sessionStorage) {
            return window.sessionStorage.getItem("sessionToken");
        } else {
            throw new Error("sessionStorage is not defined!");
        }
    }
}