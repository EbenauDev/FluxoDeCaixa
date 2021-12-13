//new Intl.NumberFormat('pt-BR', { style: 'currency', currency: 'BRL' }).format(value);

if (!Number.prototype.toReal) {
    Object.defineProperty(Number.prototype, "toReal", {
        value: function () {
            return new Intl.NumberFormat('pt-BR', { style: 'currency', currency: 'BRL' }).format(this);
        },
        writable: true,
        enumerable: false,
    })
}

if (!String.prototype.toReal) {
    Object.defineProperty(String.prototype, "toReal", {
        value: function () {
            return new Intl.NumberFormat('pt-BR', { style: 'currency', currency: 'BRL' }).format(this);
        },
        writable: true,
        enumerable: false,
    })
}