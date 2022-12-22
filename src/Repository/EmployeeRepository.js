import Repository from "./Repository";

const resource = "/Employees";

export default {
    getAllEmployee() {
        return Repository.get(`${resource}`);
    },

    getEmployeeByID(employeeID) {
        return Repository.get(`${resource}/${employeeID}`);
    },
    getNewEmployeeCode() {
        return Repository.get(`${resource}/newCode`);
    },
    getEmployeeByPagding(pageSize,pageNumber) {
        return Repository.get(`${resource}/filter?PageSize=${pageSize}&PageNumber=${pageNumber}`);
    },
    getEmployeeByFilterAndPagding(pageSize,pageNumber,filter) {
        return Repository.get(`${resource}/filter?keyword=${filter}&PageSize=${pageSize}&PageNumber=${pageNumber}`);
    },
    getInfoEmployee(id) {
        return Repository.get(`${resource}/filter?keyword=${id}`);
    },
    postEmployee(employee) {
        return Repository.post(`${resource}` , employee);
    },
    deleteBatch(EmployeeIDs) {
        return Repository.post(`${resource}/deleteBatch` , EmployeeIDs);
    },
    exportExcel() {
        return Repository.post(`${resource}/exportExcel`);
    },
    updateEmployee(employeeID,payload) {
        return Repository.put(`${resource}/${employeeID}`,payload);
    },
     deleteEmployeeByID(employeeID) {
        return Repository.delete(`${resource}/${employeeID}`);
    },

}