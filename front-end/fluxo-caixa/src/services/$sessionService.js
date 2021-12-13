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
    },
    getParseTokenSession() {
        var token = this.getTokenSession();
        var base64Url = token.split('.')[1];
        var base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/');
        var jsonPayload = decodeURIComponent(atob(base64).split('').map(function (c) {
            return '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2);
        }).join(''));

        return JSON.parse(jsonPayload);
    }
}