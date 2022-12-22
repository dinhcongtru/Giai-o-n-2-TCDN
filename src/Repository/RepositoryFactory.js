
import EmployeeRepository from "./EmployeeRepository";
import DepartmentRepository from "./EmployeeRepository";

const repositories = {
    Employees: EmployeeRepository,
    Departments: DepartmentRepository
};

export const RepositoryFactory = {
    get:name => repositories[name]
};