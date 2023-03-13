import Repository from "./Repository";

const resource = "/Vendors";

export default {
    getAllVendor() {
        return Repository.get(`${resource}`);
    },

    getVendorByID(vendorID) {
        return Repository.get(`${resource}/${vendorID}`);
    },
    getNewVendorCode() {
        return Repository.get(`${resource}/newCode`);
    },
    getVendorByPagding(pageSize,pageNumber) {
        return Repository.get(`${resource}/filter?PageSize=${pageSize}&PageNumber=${pageNumber}`);
    },
    getVendorByFilterAndPagding(pageSize,pageNumber,filter) {
        return Repository.get(`${resource}/filter?keyword=${filter}&PageSize=${pageSize}&PageNumber=${pageNumber}`);
    },
    getInfoVendor(id) {
        return Repository.get(`${resource}/filter?keyword=${id}`);
    },
    postVendor(vendor) {
        return Repository.post(`${resource}` , vendor);
    },
    deleteBatch(VendorIDs) {
        return Repository.post(`${resource}/deleteBatch` , VendorIDs);
    },
    exportExcel() {
        return Repository.post(`${resource}/exportToExcel`);
    },
    updateVendor(vendorID,payload) {
        return Repository.put(`${resource}/${vendorID}`,payload);
    },
     deleteVendorByID(vendorID) {
        return Repository.delete(`${resource}/${vendorID}`);
    },

}