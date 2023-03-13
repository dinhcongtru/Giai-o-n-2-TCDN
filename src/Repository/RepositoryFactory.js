
import EmployeeRepository from "./EmployeeRepository";
import DepartmentRepository from "./EmployeeRepository";
import VendorRepository from "./VendorRepository";
const repositories = {
    Employees: EmployeeRepository,
    Departments: DepartmentRepository,
    Vendors: VendorRepository
};

export const RepositoryFactory = {
    get:name => repositories[name]
};