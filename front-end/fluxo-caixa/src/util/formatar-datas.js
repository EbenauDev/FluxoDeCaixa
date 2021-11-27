export default (inputFormat, value) => {
    const _formaters = {
        "DD/MM/YYYY": (valueForToISOString) => {
            const [day, month, year] = valueForToISOString.split("/");
            return `${year}-${month}-${day}T00:00:00`;
        }
    }
    return _formaters[inputFormat](value);
}