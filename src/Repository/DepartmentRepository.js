import Repository from "./Repository";

const resource = "/Departments";

export default {
    getAllDepartments() {
        return Repository.get(`${resource}`);
    },
}