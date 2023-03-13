<template>
    <table class="ms-table">
        <thead>
            <tr>
                <th align="center" class="theadCheck">
                    <input 
                        id="checkbox-header"
                        ref="checkBoxAll"
                        type="checkbox" 
                        class="input-check-table"
                        @click="onToggleCheckAll"
                        :checked="selectAll"
                        
                        
                        />
                </th>
                <th align="left" class="th2" style="min-width:220px">MÃ NHÀ CUNG CẤP</th>
                <th align="left" class="th3" style="min-width:150px">NHÓM KH, NCC</th>
                <th align="left" style="min-width:240px">TÊN NHÀ CUNG CẤP</th>
                <th align="left" style="min-width:133px">MÃ SỐ THUẾ</th>
                <th align="left" style="min-width:150px" title="Số chứng minh nhân dân">SỐ CMND</th>
                <th align="left" style="min-width:200px">CHI NHÁNH</th>
                
                <th class="cn-item">CHỨC NĂNG</th>
            </tr>
        </thead>
        <tbody>
            <tr v-for="(vendor, index) in vendors" class="trTable" :key="vendor.vendorID" :class="{'background-table': vendor.IsChecked}" @dblclick="openFormDetail(index, 'edit-vendor')">
                <td align="center" class="tdCheck" :class="{ 'background-table': vendor.IsChecked }"><input type="checkbox" :id= vendor.vendorID class="input-check-table" ref="checkboxRow" v-model="vendor.IsChecked" @click="handleClickCheck()"></td>
                <td  class="td2" :class="{ 'background-table': vendor.IsChecked }">{{ vendor.vendorCode }}</td>
                <td  class="td3" :class="{ 'background-table': vendor.IsChecked }">{{ vendor.groupVendorCode }}</td>
                <td>{{ vendor.vendorName || "" }}</td>
                <td>{{ vendor.taxCode || "" }}</td>
                <td>{{ vendor.identityNumber || "" }}</td>
                <td>{{ vendor.address || "" }}</td>
                <!-- <td>{{ employee.departmentName || "" }}</td>
                <td>{{ employee.bankAccountNumber || "" }}</td>
                <td>{{ employee.bankName || "" }}</td>
                <td>{{ employee.bankBranchName || "" }}</td> -->
                <td class="td-tool" :class="[featureDropdown == index && zIndex5,vendor.IsChecked && backGroundTbl,]" >
                    <div class="cn-edit">
                        <div class="edit-table" id="edit-nv" @click.self="onToggleModal(index, 'edit-vendor')">{{Resource.CONTENT.btnEdit}}</div>
                        <span class="icon-edit"  @click="onToggleRepair(index,vendor.vendorCode,vendor.vendorID)" :class="[index == featureDropdown && isDropdown && dropdownIconBorder && 'boder1pxblue']">
                            <div class="detail-cn" v-show="showDelete == index">
                                <div class="xoa" id="delete" @click="onDeleteVendor()">{{Resource.TOTALTOOL.xoa}}</div>
                                <div class="nhanban" @click.self="onToggleModalNhanban(index, 'nhanban-form')">{{Resource.TOTALTOOL.nhanban}}</div>
                            <div class="ngungsd">{{Resource.TOTALTOOL.ngungsd}}</div>
                    </div>
                        </span>
                    </div>
                    <div
                        class="overlay-feature"
                        v-if="isDropdown"
                        @click="onToggleRepair()"
                    ></div>
                   
                </td>
                <vendor-details v-if="isModal == index" :vendorCode="vendor.vendorCode" :vendorID="vendor.vendorID" :id="id" @closeModal="onToggleModal" @refreshData="resfreshToPageOne" />

            </tr>
        </tbody>
        <ms-dialog
            v-show="isDialog"
            dialogName="delete"
            :vendorCode="codeVendor"
            :vendorID="idVendor"
            @closeDialog="onDeleteVendor"
            @deleteSuccess="refreshData"
        />
       
        <ms-dialog
            v-if="isError"
            dialogName="validate"
            :statusValidate="errorMgs"
            @closeDialog="onCloseDialog"
        />
        <ms-loading v-if="isLoading" />
    </table> 
</template>
<script>

import msDialog from './base/dialog/ms-dialog.vue';
import msLoading from './base/ms-loading.vue';
// import TheModal from './TheModal.vue';
import VendorDetails from '@/views/Vendor/VendorDetails.vue';
// import {RepositoryFactory} from '../Repository/RepositoryFactory';
//  const EmployeeRepository = RepositoryFactory.get('Employees');
 import Resource  from '@/Resource/Resource';
 import functionTable  from '@/methods/methods'
export default {
    name: "TheTable",
    components: { msDialog, msLoading ,VendorDetails},
    watch: {
        /**
         * Thực hiện đưa dữ liệu tổng sô bản ghi lên component BaseTable
         **  Author: Đinh Công Trứ(26/10/2022)
         */
        // employees() {
        //     this.$emit("totalPage", this.totalPage);
        // },
       
        // Emit data IsChecked lên component cha
        isChecked() {
            this.$emit("checkAll", this.isChecked);
        },
        // Xử lý chuyển idForm prop vào data id mở modal
        idForm() {
            if (this.idForm) {
                this.id = this.idForm;
                this.onToggleModal();
            }
        },
    },
    methods: {
        /**
         * xử lý uncheckbox
         * Đinh công Trứ(30/10/2022)
         */
         unCheckAll(){
            this.$refs.checkBoxAll.checked = false;
            this.selected = [];
            
         },
        /**Truyền thông báo ra bên ngoài load lại data
         * Author: Đinh Công Trứ(30/10/2022)
         */
         refreshData(){
            this.$emit("refreshData");
        },
        onCloseDialog() {
            this.isError = !this.isError;
            
        },
         /**Thực hiện ẩn TheDropdown chức năng
         * Author: Đinh Công Trứ(30/10/2022)
         */
         ToggleDropdown() {
            this.isOpenDropdown = !this.isOpenDropdown;
        },
        /**
         * Thực hiện checked và bỏ checked tất cả các bản ghi
         **  Author: Đinh Công Trứ(4/11/2022)
         */
         onToggleCheckAll() {
            try {
                var select = this.selectAll;
                this.selected = [];
                const me = this;
                this.vendors.forEach(function (vendor) {
                    vendor.IsChecked = !select;
                    if (!select) {
                        me.selected.push(vendor.vendorID);
                    } else {
                        me.selected = [];
                    }
                });
                this.selectAll = !select;
                this.$emit("checkAll", this.selected);
            
            } catch (error) {
                console.log("Check all bị lỗi");
            }
           
        },

        /**
         * Thực hiện xử lý từng checkbox khi check bằng tay đủ tất cả hoặc
         * bỏ chọn 1 checkbox khi đang checkbox all
         **  Author: Đinh Công Trứ(4/11/2022)
         */
         handleClickCheck() {
            try {
                this.selected = [];
                let checkRow = this.$refs.checkboxRow;
                
                // sử lý lấy id được chọn
                for(const checkbox of checkRow){
                    if(checkbox.checked == true){
                        this.selected.push(checkbox.id);
                    }
                    this.$emit("checkAll", this.selected);
                    
                }
                // sử lý bỏ checkbox
                let temp = 0;
                for(const checkbox of checkRow){
                    if(checkbox.checked == false){
                        this.selectAll = false
                        temp++;
                        
                        break;
                    }
                }
                if(temp == 0){
                    this.selectAll = true;
                }              
            } catch (error) {console.log(error);}
        },


        /**
         * Thực hiện xử lý dropdown Xóa
         **  Author: Đinh Công Trứ(26/10/2022)
         */
        onToggleRepair(index, code, id) {
            if (this.isDropdown) {
                this.showDelete = null;
                this.isDropdown = !this.isDropdown;
                this.featureDropdown = null;
            } else {
                this.showDelete = index;
                this.isDropdown = !this.isDropdown;
                this.featureDropdown = index;
                this.codeVendor = code;
                this.idVendor = id;
            }
        },
        
        /**
         * Thực hiện xử lý UI khi click feature Xóa hiện thị Dialog Xóa nhân viên
         **  Author: Đinh Công Trứ(30/10/2022)
         */
        onDeleteVendor() {
            this.isDialog = !this.isDialog;
            this.isDropdown = false;
            this.featureDropdown = null;
            this.showDelete = null;
            this.$emit("focusInput");
        },
        openFormDetail(index, edit){
            // console.log('bbbbbbbb');
            this.isShowModal = true;
            this.isModal = index;
                // Mở form edit nếu có argument edit
                if (edit) {
                    this.id = edit;
                }
                // Mở form add nhân viên nếu có argument edit

                if (!this.isModal) {
                    this.isModal = 0;
                }
        },
        /**
         * Thực hiện xử lý UI ẩn hiện form nhân viên hoặc sửa
         **  Author: Đinh Công Trứ(30/10/2022)
         */
        onToggleModal(index, edit) {
            // Nểu isShowModal = true thì chuyển về false và đóng form
            if (this.isShowModal) {
                this.isShowModal = !this.isShowModal;
                this.isModal = null;
                if (this.idForm) {
                    // Chuyền thông báo tới thằng cha là form add nhân viên đã đóng
                    this.$emit("addForm");
                }
            } else {
                // Mở form
                this.isShowModal = !this.isShowModal;
                this.isModal = index;
                // Mở form edit nếu có argument edit
                if (edit) {
                    this.id = edit;
                }
                // Mở form add nhân viên nếu có argument edit

                if (!this.isModal) {
                    this.isModal = 0;
                }
            }
        },
        onToggleModalNhanban(index, edit) {
            // Nểu isShowModal = true thì chuyển về false và đóng form
            if (this.isShowModal) {
                this.isShowModal = !this.isShowModal;
                this.isModal = null;
                if (this.idForm) {
                    // Chuyền thông báo tới thằng cha là form add nhân viên đã đóng
                    this.$emit("addForm");
                }
            } else {
                // Mở form
                this.isShowModal = !this.isShowModal;
                this.isModal = index;
                // Mở form edit nếu có argument edit
                if (edit) {
                    this.id = edit;
                }
                // Mở form add nhân viên nếu có argument edit

                if (!this.isModal) {
                    this.isModal = 0;
                }
            }
        },
        /**
         * Thực hiện xử lý chuyền thông báo tới thằng cha và thằng cha sẽ sử lý reset lại dữ liệu
         **  Author: Đinh Công Trứ(30/10/2022)
         */
        resfreshToPageOne() {
            this.$emit("refreshData");
        },
    },
    props: [ "idForm","vendors"],
    data() {
        return {
            isDropdown: false,
            // employees: null,
            // totalPage: 0,
            featureDropdown: null,
            showDelete: null,
            dropdownIconBorder: "repair-icon-border",
            backGroundTbl: "background-table",
            zIndex5: "z-index5",
            isDialog: false,
            codeVendor: null,
            idVendor: null,
            isShowModal: false,
            isModal: null,
            isLoading: false,
            selected: [],
            id: "",
            // pageFilter: this.pageNumber,
            selectAll: false,
            Resource : Resource,
            Method:functionTable,
            isDeleteBatch: false,
            
            errorMgs: false,
            isError: false,
        };
    },
};
</script>
<style scoped>
.ms-table{
    border-collapse: separate;
    border-spacing: unset;
}
.theadCheck{
    position: sticky;
    left: 0;
    border-right: 0;
    height: 40px;
}
table.ms-table thead th{
    text-align: left;
    background-color: #eceef1;
    padding: 0px 10px;
    border-collapse: collapse;
    /* border: 2px solid #f1f1f1; */
    height: 32px;
    border-right: 1px solid #c7c7c7;
    border-bottom: 1px solid #c7c7c7;
}
.tdCheck{
    position: sticky;
    left: 0;
    background-color: white;
    border-right: 0;
    height: 48px !important;
}
table.ms-table tbody td{
    border-collapse: collapse;
    /* border: 1px solid #f1f1f1; */
    height: 32px;
    border-left: 0;
    padding: 0px 10px;
    z-index: 5;
    background: white;
    border-bottom: 1px solid #c7c7c7;
    border-bottom: 1px solid #c7c7c7;
    border-right: 1px dotted #c7c7c7;
}
.cn-item{
    text-align: center !important;
    padding-right: 16px;
    min-width: 90px;
    border: 0;
    position: sticky;
    right: 0;
    border-bottom: 1px solid #c7c7c7;
    border-right: 1px dotted #c7c7c7;
    border-left: 1px dotted #c7c7c7 !important;
    border-right: 0 !important;
}
.td-tool{
    position: sticky;
    right: 0;
    background-color: white;
    border: 0;
    border-bottom: 1px solid #c7c7c7;
    border-right: 1px dotted #c7c7c7;
    border-left: 1px dotted #c7c7c7 !important;
}
.th2{
    position: sticky;
    left: 41px !important;
    height: 41px !important;

}
.th3{
    position: sticky;
    left: 282px !important;
}
.td2{
    position: sticky;
    left: 41px !important;
    background-color: white;    position: sticky;
    height: 41px !important;
    background-color: white;
}
.td3{
    position: sticky;
    left: 282px;
    background-color: white;
}
tr:has(input[type = checkbox]:checked) td {
    background-color: #dcf1d8;
}
table.ms-table tbody tr:hover{
    cursor: pointer;
}
table.ms-table tbody tr:hover td {
    background: #e5f3ff!important;
    cursor: pointer;
}
</style>