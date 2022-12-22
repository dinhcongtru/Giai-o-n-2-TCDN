<template>
    <div class="row-dialog" :id="id" @keydown = "handleshortcuts">
            <div class="pogup">
                <div class="head-pog">
                    <div class="head-tolBar">
                        <div class="tt-head-pog" id="inf-editnv">{{ title }}</div>
                        <div class="check-head-pog">
                            <ms-radio
                                            :tabindex="'6'"
                                            :genderValue="employee.gender"
                                             @gender="getGender($event)"
                                        />
                            <div class="custom">
                                <input type="checkbox" class="check-custom" name="" id="" tabindex="20">
                            <span style="padding-left: 10px">{{Resource.MODAL.isCustom}}</span>
                            </div>
                            <div class="eye">
                                <input type="checkbox" class="check-ey" name="" id="" tabindex="21" @blur="tabOrder">
                                <span style="padding-left: 10px">{{Resource.MODAL.isProduct}}</span>
                            </div>
                        </div>
                    </div>
                    <div class="close-pog">
                        <span class="icon-ques" title="Giúp(F1)"></span>
                        <span class="icon-close closeEmployee" title="Đóng(ESC)" @click="onCloseModal"></span>
                    </div>
                </div>
                <div class="content-pog">
                    <div class="content-pog-t">
                        <div class="cont-l" >
                                <div class="info-ma">
                                    <div class="colum-ma">
                                        <div class="ma">{{Resource.NAME_DISPLAY.EMPLOYEECODE}}
                                            <span style="color: red;">*</span>
                                        </div>
                                        <ms-input type="text" :style="'inp-ma'" :id="'txtEmployeeCode'" placeholder="Mã" v-model="employee.employeeCode"  tabindex="1" :autoFocus="true" required :error="error.employeeCode" :blur="validateEmployeeCode(employee.employeeCode)" autocomplete="off"/>
                                        
                                    </div>
                                    <div class="colum-name">
                                            <div class="name">{{Resource.NAME_DISPLAY.EMPLOYEENAME}}
                                                <span style="color: red;">*</span>
                                            </div>
                                            <ms-input type="text" :style="'inp-name'" v-model="employee.employeeName" placeholder="Tên" tabindex="2"  required :error="error.employeeName" :blur="validateEmployeeName(employee.employeeName)" autocomplete="off"/>
                                        
                                        </div>
                                </div>
                                <div class="info-donvi">
                                    <div class="name-dv">
                                        <div style="font-size:14px; font-weight: 700;">{{Resource.NAME_DISPLAY.DEPARTMENT}}
                                            <span style="color: red;">*</span>
                                        </div>
                                    </div>
                                    <ms-dropdown
                                    type="text"
                                    title="Đơn vị không được để trống."
                                    placeholder="Đơn vị"
                                    id="DepartmentName"
                                    tabindex="3"
                                    label="Đơn vị"
                                    :departmentList="departmentList"
                                    :departmentId="employee.departmentID"
                                    :departmentName="employee.departmentName"
                                    :departmentError="error.departmentName"
                                    @departmentId="getDepartmentId($event)"
                                    @departmentName="getDepartmentName($event)"
                                />
                               
                                </div>
                               
                                <div class="chucvu">
                                    <div class="colum-cv">
                                        <div class="name-cv">{{Resource.NAME_DISPLAY.POSITION}}</div>
                                        <input type="text" class="inp-ma chucdanh" name="input" placeholder="Chức danh" tabindex="4" id="txtChucDanh" v-model="employee.jobPositionName" autocomplete="off">
                                    </div>
                                </div> 
                        </div>
                        <div class="cont-r">
                            <div class="info-ma colum-r">
                                <div class="colum-ma ngsinh">
                                    <div class="ma ngsinh">{{Resource.NAME_DISPLAY.DATEOFBIRTH}}</div>
                                    <input type="date" class="inp-ma date-ngsinh date" style="font-family:rotosans;" name="input" tabindex="5" id="txtNgaySinh" v-model="employee.dateOfBirth">
                                
                                </div>
                                <div class="colum-name colum-gt">
                                        <div class="name name-gt">{{Resource.NAME_DISPLAY.GENDER}}</div>
                                        <ms-radio
                                            tabindex="6"
                                            radioName="radio"
                                            :radioData="radioData"
                                            :value="employee.gender"
                                             
                                        />
                                </div>
                            </div>
                            <div class="info-ma colum-next">
                                <div class="colum-name cmnd">
                                        <div class="name cmnd" title="Số chứng minh nhân dân">{{Resource.NAME_DISPLAY.INDENTITYNUMBER}}</div>
                                        <input type="text" class="inp-name cmnd" id="txtCMND" name="input" placeholder="Số CMND" tabindex="7" v-model="employee.identityNumber" autocomplete="off">
                                        
                                </div>
                                <div class="colum-ma ngaycap">
                                    <div class="ma ngaycap">{{Resource.NAME_DISPLAY.IDENTITIDATE}}</div>
                                    <input type="date" class="inp-ma date-cmnd date" style="font-family:rotosans;" name="input" tabindex="8" id="txtNgayCap" v-model="employee.identitiDate" >
                                </div>
                            </div>
                            <div class="chucvu noicap">
                                <div class="colum-cv noicap">
                                    <div class="name-cv noicap">{{Resource.NAME_DISPLAY.IDENTITIPLACE}}</div>
                                    <input type="text" class="inp-ma chucdanh noicap" name="input" placeholder="Nơi cấp" tabindex="9" id="txtNoiCap" v-model="employee.identitiPlace" autocomplete="off">
                                </div>
                            </div> 
                        </div>
                    </div>
                    <div class="content-pog-b" style="width:100%">
                        <div class="chucvu diachi">
                            <div class="colum-cv diachi">
                                <div class="name-cv diachi">{{Resource.NAME_DISPLAY.ADDRESS}}</div>
                                <input type="text" class="inp-ma chucdanh noicap diachi" name="input" placeholder="Địa chỉ" tabindex="10" id="txtDiaChi" v-model="employee.address" autocomplete="off">
                            </div>
                        </div> 
                        <div class="info-ma first">
                            <div class="colum-name didong">
                                <div class="name didong">{{Resource.NAME_DISPLAY.PHONENUMBER}}</div>
                                <input type="text" class="inp-name inp-didong" name="input" placeholder="ĐT di động" tabindex="11" id="txtDTDiDong" v-model="employee.phoneNumber" autocomplete="off">
                            </div>
                            <div class="colum-name coding">
                                    <div class="name coding">{{Resource.NAME_DISPLAY.TELEPHONENUMBER}}</div>
                                    <input type="text" class="inp-name inp-coding" name="input" placeholder="ĐT cố định" title="Điện thoại cố định" tabindex="12" id="txtDTCoDinh" v-model="employee.telephoneNumber" autocomplete="off">
                            </div>
                            <div class="colum-name email">
                                <div class="name email">{{Resource.NAME_DISPLAY.EMAIL}}</div>
                                <input type="text" class="inp-name inp-email" name="input" placeholder="Email" tabindex="13" id="txtEmail" v-model="employee.email" autocomplete="off">
                                
                            </div>
                        </div>
                        <div class="info-ma first end">
                            <div class="colum-name didong tkgh">
                                <div class="name didong tkgh">{{Resource.NAME_DISPLAY.BANKNUMBER}}</div>
                                <input type="text" class="inp-name inp-didong tkgh" name="input" placeholder="Tài khoản ngân hàng" tabindex="14" id="txtTKNH" v-model="employee.bankAccountNumber" autocomplete="off">
                            </div>
                            <div class="colum-name coding nghang">
                                    <div class="name coding nghang">{{Resource.NAME_DISPLAY.BANKNAME}}</div>
                                    <input type="text" class="inp-name inp-coding nghang" name="input" placeholder="Tên ngân hàng" tabindex="15" id="txtTenNH" v-model="employee.bankName" autocomplete="off">
                            </div>
                            <div class="colum-name email chinhanh">
                                <div class="name email chinhanh">{{Resource.NAME_DISPLAY.BRANCHNAME}}</div>
                                <input type="text" class="inp-name inp-email chinhanh" name="input" placeholder="Chi nhánh" tabindex="16" id="txtChiNhanh" v-model="employee.bankBranchName" autocomplete="off">
                            </div>
                        </div>
                    </div>
                </div>
                <div class="f-pog">
                    <button class="f-pog-l closeEmployee" tabindex="17" id="cancel-add-form" title="Hủy(Ctrl F9)" @click="onCancelModal">{{Resource.BUTTON.HUY}}</button>
                    <div class="f-pog-r">
                        <ms-button class="cat-pog" tabindex="18" id="cancel-add-form" title="Cất" type="submit" @click="save()"/>
                        <ms-button tabindex="19" :id="idButton" :title="titleButton" class="add-pog" type="submit" :class="[id == 'edit-form']" @click="saveAndAdd(e)"/>
                    </div>
                </div>
            </div>
            <ms-dialog
            v-if="isError"
            dialogName="validate"
            :statusValidate="errorMgs"
            @closeDialog="onCloseDialog"
        />
        <ms-dialog
            v-if="isWarring"
            dialogName="warring" 
            @save="save()"          
            @closeDialog="onCancelDialog"
        />
            
        </div>
</template>
<script>
// import TheInputVue from "./TheInput.vue";
import msButton from "@/components/base/ms-button.vue";
import msDropdown from "@/components/base/ms-dropdown.vue";
import msRadio from "@/components/base/ms-radio.vue";
import msDialog from '@/components/base/ms-dialog.vue';
import Resource from "@/Resource/Resource";
import keyCode from "@/enum/keyCode";
import msInput from "@/components/base/ms-input.vue";
import {RepositoryFactory} from '../Repository/RepositoryFactory';
 const EmployeeRepository = RepositoryFactory.get('Employees');
//  const DepartmentRepository = RepositoryFactory.get('Departments');
import DepartmentRepository from "@/Repository/DepartmentRepository";


export default {
    name: "TheModal",
    components: {
        msButton,
        msDropdown,
        msRadio,
        msDialog,
        msInput,
    },
    props: {
        id: String,
        employeeCode: String,
        employeeID: String,
        
    },
    beforeMount() {
        // Xử lý UI form thêm hoặc sửa trước khi mở form
        this.handleUI();
        // Xử lý Lấy mã nhân viên mới
        this.getNewEmployeeCode();
        // Lấy danh sách phòng ban đưa lên UI
        this.getDepartment();
        // Lấy dữ liệu nhân viên đưa vào trong form
        this.userInfo(this.employeeCode);
    },
    // Tự động focus mã nhân viên khi form mở
    mounted() {
        // this.$refs.input.focus();
        
        this.employeeOld = structuredClone(this.employee);
    },
    
    methods: {
        /**
         * Xử lý phím tắt trong MODAL
         * Author : Đinh Công Trứ (2/11/2022)
         * 
         */
        handleshortcuts(e){
            if(e.keyCode == keyCode.keyCode.esc){
                this.onCloseModal();
            }
            if( e.ctrlKey && e.keyCode == keyCode.keyCode.cancel){
                this.onCancelModal();
            }
            if(e.ctrlKey && e.keyCode == keyCode.keyCode.save){
                this.save();
            }
           

        },
        /**
         * Thực hiện xử lý tab order
         **  Author: Đinh Công Trứ(2/11/2022)
         */
        // tabOrder() {
        //     this.$refs.input.focus();
        // },
        /**
         * Thực hiện xử lý đóng form
         **  Author: Đinh Công Trứ(2/11/2022)

         */
        onCloseModal() {
                   

            try {
                if(JSON.stringify(this.employee) == JSON.stringify(this.employeeOld)){
                    // this.$parent.isDialog = false;
                  
                    this.$emit("closeModal");
                }else{
                    this.isWarring = true;
     
                }
            } catch (error) {
                console.log(error);
            }

        },
        onCancelModal() {
    
            this.$emit("closeModal");
      
        },
        /**
         * Thực hiện xử lý đóng dialog
         **  Author: Đinh Công Trứ(2/11/2022)
         */
        onCloseDialog() {
            this.isError = !this.isError;
            
        },
        onCancelDialog(){
            this.isWarring = !this.isWarring;
        },
        /**
         * Thực hiện xử lý UI form thêm nhân viên, sửa nhân viên
         **  Author: Đinh Công Trứ(2/11/2022)
         */
        handleUI() {
            if (this.id == "add-vendor") {
                this.title = "Thông tin nhà cung cấp";
                this.idButton = "save";
                this.titleButton = Resource.BUTTON.CATTHEM;
            }
            // Thêm nhân viên
            if (this.id == "add-form") {
                this.title = Resource.TITLE.AddForm;
                this.idButton = "save";
                this.titleButton = Resource.BUTTON.CATTHEM;
            }
            // Sửa nhân viên
            if (this.id == "edit-form") {
                this.title = Resource.TITLE.EditForm;
                this.idButton = "edit";
                this.titleButton = Resource.BUTTON.CATSUA;
            }
            // Nhân bản nhân viên
            if (this.id == "nhanban-form") {
                this.title = Resource.TITLE.AddForm;
                this.idButton = "edit";
                this.titleButton = Resource.BUTTON.CATNHAN;
            }
        },
        /**
         * Thực hiện lấy dữ giới tính
         **  Author: Đinh Công Trứ(2/11/2022)
         */
        getGender(event) {
            this.employee.gender = event;
        },
        /**
         * Thực hiện lấy dữ liệu đơn vị gồm id và tên
         **  Author: Đinh Công Trứ(2/11/2022)
         */
         getDepartmentId(event) {
            this.employee.departmentID = event;
    
        },
        getDepartmentName(event) {
            this.employee.departmentName = event;
            
            
        },
        /**
         * Thực hiện xử lý validate dữ liệu mã nhân viên
         **  Author: Đinh Công Trứ(2/11/2022)
         */
        validateEmployeeCode(code) {
            if (!code) {
                this.error.employeeCode = "form-input-error";
            } else {
                this.error.employeeCode = "";
            }
        },
        /**
         * Thực hiện xử lý validate dữ liệu tên nhân viên
         **  Author: Đinh Công Trứ(2/11/2022)
         */
        validateEmployeeName(name) {
            let me = this;
            return () => {
                
                if (!name) {
                    me.error.employeeName = "form-input-error";
                } else {
                    me.error.employeeName = "";
                }
            }
        },
        /**
         * Thực hiện validate dữ liệu khi ấn lưu
         **  Author: Đinh Công Trứ(2/11/2022)
         */
        validateForm() {
            if (!this.employee.employeeCode) {
                this.error.employeeCode = "form-input-error";
               
            } else {
                this.error.employeeCode = "";
            }
            if (!this.employee.employeeName) {
                this.error.employeeName = "form-input-error";
                this.isError = !this.isError;
                this.errorMgs = Resource.ERROR.employeeNameError;
               return false;
               
            } else {
                this.error.employeeName = "";
            }
            if (!this.employee.departmentName) {
                this.error.departmentName = "form-input-error";
                this.isError = !this.isError;
                this.errorMgs = Resource.ERROR.departmentError;
               return false;
            }

            return true;
        },
        /**
         * Thực hiện format date từ api trả về
         **  Author: Đinh Công Trứ(2/11/2022)
         */
        formatDate(date) {
            if (date) {
                var d = new Date(date),
                    month = "" + (d.getMonth() + 1),
                    day = "" + d.getDate(),
                    year = d.getFullYear();

                if (month.length < 2) month = "0" + month;
                if (day.length < 2) day = "0" + day;

                return [year, month, day].join("-");
            } else {
                return null;
            }
        },
        /**
         * Thực hiện lấy mã nhân viên mới nhất
         **  Author: Đinh Công Trứ(2/11/2022)
         */
        getNewEmployeeCode() {
            if (this.id == "add-form" || this.id == "nhanban-form" || this.id =="add-vendor") {
                const me = this;
                try{
                    EmployeeRepository.getNewEmployeeCode()
                    .then((response) => {                    
                        this.employee.employeeCode = response.data.EmployeeCode;  
                        this.employeeOld.employeeCode = response.data.EmployeeCode;                        
                    })
                    .catch((error) => {
                        if(error.response.status == 500){
                            me.isError = !me.isError;
                            me.errorMgs = Resource.ERROR.loadDataError;
                        }else {
                            me.isError = !me.isError;
                            me.errorMgs = error.response.data.userMsg;
                        }
                    });
                } catch (error){
                    console.log(error);
                }
             }
        },
        /**
         * Thực hiện lấy Danh sách đơn vị
         **  Author: Đinh Công Trứ(2/11/2022)
         */
        getDepartment() {
            const me = this;
            DepartmentRepository.getAllDepartments()
            .then((res) => {
                    this.departmentList = res.data;
                })
                .catch((error) => {
                    if(error.response.status == 500){
                            me.isError = !me.isError;
                            me.errorMgs = Resource.ERROR.filterDataError;
                        }else {
                            me.isError = !me.isError;
                            me.errorMgs = error.response.data.userMsg;
                        }
                });
        },
        /**
         * Thực hiện lấy dữ liệu nhân viên đưa ra form
         **  Author: Đinh Công Trứ(2/11/2022)
         */

        userInfo(id) {
            try {
                if (this.id == "edit-form") {
                    const me = this;
                    EmployeeRepository.getInfoEmployee(id)
                    .then((response) => {
                            // me.employeeOld = response.data.data[0];
                            me.employee = response.data.data[0];
                            me.employee.dateOfBirth = this.formatDate(
                                response.data.data[0].dateOfBirth
                            );
                 
                            me.employee.identitiDate = this.formatDate(
                                response.data.data[0].identitiDate
                            );
                            me.employeeOld=structuredClone(me.employee);
                            
                            
                        })
                        .catch((error) => {
                            if(error.response.status == 500){
                            me.isError = !me.isError;
                            me.errorMgs = Resource.ERROR.loadDataError;
                            }else {
                                me.isError = !me.isError;
                                me.errorMgs = error.response.data.userMsg;
                            }
                        });
                    
                }
                if (this.id == "nhanban-form") {                  
                    const me = this;
                    EmployeeRepository.getInfoEmployee(id)
                    .then((response) => {
                     
                     me.employee = response.data.data[0];
                        
                      
                 
                     me.employee.dateOfBirth = this.formatDate(
                         response.data.data[0].dateOfBirth
                     );
                     me.employee.identitiDate = this.formatDate(
                         response.data.data[0].identitiDate
                     );
                     me.employee.gender = response.data.data[0].gender;
                     this.employeeOld=structuredClone(this.employee); 

                     
                     
                 })
                //  this.employee.employeeCode = ""
                 this.getNewEmployeeCode()   ;
                       
                }
            } catch (error) {
                this.isError = !this.isError;
                this.errorMgs = Resource.ERROR.loadDataError;
            }
        },
        /**
         * Thực hiện Lưu dữ liệu thêm mới hoặc sửa nhân viên
         **  Author: Đinh Công Trứ(2/11/2022)
         */

        save() {
            const validate = this.validateForm();

            const me = this;
            // Thêm mới nhân viên
            if (validate && this.id == "add-form") {
                if (this.employee.dateOfBirth) {
                    this.employee.dateOfBirth = new Date(
                        this.employee.dateOfBirth
                    );
                }
                if (this.employee.identitiDate) {
                    this.employee.identitiDate = new Date(
                        this.employee.identitiDate
                    );
                }
                if(this.employee.gender){
                    this.employee.gender = parseInt(this.employee.gender)
                }
                try {
                    EmployeeRepository.postEmployee(this.employee)
                    .then( res =>  {
                        if(res){
                        this.$emit("closeModal");
                        me.$emit("refreshData");
                        }
                    })
                        .catch(function (error) {
                            if(error.response.status == 500){
                                me.isError = !me.isError;
                                me.errorMgs = Resource.ERROR.errorPost;
                            }else {
                                me.isError = !me.isError;
                                me.errorMgs = error.response.data.userMsg;
                            }
                        });
                } catch (error) {
                    console.log(error);
                }
            }
            // Nhân bản nhân viên
            if (validate && this.id == "nhanban-form") {
                if (this.employee.dateOfBirth) {
                    this.employee.dateOfBirth = new Date(
                        this.employee.dateOfBirth
                    );
                }
                if (this.employee.identitiDate) {
                    this.employee.identitiDate = new Date(
                        this.employee.identitiDate
                    );
                }
                if(this.employee.gender){
                    this.employee.gender = parseInt(this.employee.gender)
                }
               try {
                EmployeeRepository.postEmployee(this.employee)
                .then( res =>  {
                        if(res){
                        this.$emit("closeModal");
                        me.$emit("refreshData");
                        }
                    })
                    .catch(function (error) {
                        if(error.response.status == 500){
                            me.isError = !me.isError;
                            me.errorMgs = Resource.ERROR.errorPost;
                        }else {
                            me.isError = !me.isError;
                            me.errorMgs = error.response.data.userMsg;
                        }
                    });
               } catch (error) {
                    console.log(error);
               }

            }
            // Sửa nhân viên
            if (validate && this.id == "edit-form") {
                if (this.employee.dateOfBirth) {
                    this.employee.dateOfBirth = new Date(
                        this.employee.dateOfBirth
                    );
                }
                if (this.employee.identitiDate) {
                    this.employee.identitiDate = new Date(
                        this.employee.identitiDate
                    );
                }

                if(this.employee.gender){
                    this.employee.gender = parseInt(this.employee.gender)
                }
               try {
                EmployeeRepository.updateEmployee(this.employeeID,this.employee)
                .then(() => {
                        this.$emit("closeModal");
                        me.$emit("refreshData");
                    })
                    .catch((error) => {
                        if(error.response.status == 500){
                            me.isError = !me.isError;
                            me.errorMgs = Resource.ERROR.errorEdit;
                        }else {
                            me.isError = !me.isError;
                            me.errorMgs = error.response.data.userMsg;
                        }
                    });
               } catch (error) {
                    console.log(error);
               }
               
            }
        },
        /**
         * Thực hiện xử lý Lưu nhân viên khi ấn button thêm và cất
         **  Author: Đinh Công Trứ(2/11/2022)
         */
        saveAndAdd() {
            const validate = this.validateForm();

            const me = this;
            if (validate && this.id == "add-form") {
                if (this.employee.dateOfBirth) {
                    this.employee.dateOfBirth = new Date(
                        this.employee.dateOfBirth
                    );
                }
                if (this.employee.identitiDate) {
                    this.employee.identitiDate = new Date(
                        this.employee.identitiDate
                    );
                }
                if(this.employee.gender){
                    this.employee.gender = parseInt(this.employee.gender)
                }
                try {
                    EmployeeRepository.postEmployee(this.employee)
                    .then(function () {
                        me.$emit("refreshData");
                        me.employee = { };
                        
                    })
                    .catch(function (error) {
                        if(error.response.status == 500){
                            me.isError = !me.isError;
                            me.errorMgs = Resource.ERROR.errorPost;
                        }else {
                            me.isError = !me.isError;
                            me.errorMgs = error.response.data.userMsg;
                        }
                    });
                } catch (error) {
                    console.log(error);
                }
               
            }
            // nhân bản nhân VIÊN
            if (validate && this.id == "nhanban-form") {
                if (this.employee.dateOfBirth) {
                    this.employee.dateOfBirth = new Date(
                        this.employee.dateOfBirth
                    );
                }
                if (this.employee.identitiDate) {
                    this.employee.identitiDate = new Date(
                        this.employee.identitiDate
                    );
                }
                if(this.employee.gender){
                    this.employee.gender = parseInt(this.employee.gender)
                }
                try {
                EmployeeRepository.postEmployee(this.employee)
                .then(function () {
                        me.$emit("refreshData");
                        me.employee = { };
                        
                    })
                    .catch(function (error) {
                        if(error.response.status == 500){
                            me.isError = !me.isError;
                            me.errorMgs = Resource.ERROR.errorPost;
                        }else {
                            me.isError = !me.isError;
                            me.errorMgs = error.response.data.userMsg;
                        }
                    });
               } catch (error) {
                    console.log(error);
               }
   
            }
            // Sửa nhân viên
            if (validate && this.id == "edit-form") {
                if (this.employee.dateOfBirth) {
                    this.employee.dateOfBirth = new Date(
                        this.employee.dateOfBirth
                    );
                }
                if (this.employee.identitiDate) {
                    this.employee.identitiDate = new Date(
                        this.employee.identitiDate
                    );
                }
                if(this.employee.gender){
                    this.employee.gender = parseInt(this.employee.gender)
                }
                try {
                EmployeeRepository.updateEmployee(this.employeeID,this.employee)
                .then(() => {
                        me.$emit("refreshData");
                        me.employee = { };                      
                    })
                    .catch((error) => {
                        if(error.response.status == 500){
                            me.isError = !me.isError;
                            me.errorMgs = Resource.ERROR.errorEdit;
                        }else {
                            me.isError = !me.isError;
                            me.errorMgs = error.response.data.userMsg;
                        }
                    });
               } catch (error) {
                    console.log(error);
               }
                
            }

        },
    },
    data() {
        return {
            keyCode : keyCode,
            Resource :Resource,
            employeeOld: {},
            posts: [],
            isWarring: false,
            isDialog: false,
            error: {
                employeeCode: true,
                employeeName: true,
                departmentName: true,
            },
            employee: {},
            idButton: "",
            titleButton: "",
            title: "",
            errorMgs: false,
            isError: false,
            departmentList: {},
            radioData:{
                Male: "Nam",
                Female: "Nữ",
                Orther: "Khác"
            }
        };
    },
};
</script>
<style>
#cancel-add-form{
    margin: 12px 0px 12px 0px;
    width: 80px;
    height: 36px;
    border-radius: 2px;
    background: transparent;
    font-family: 'rotosans';
    font-weight: 800;
    font-size: 14px;
    text-align: left;
    text-align: center;
    line-height: 0;
    color: #111;
    outline: none;
    border: 1px solid #8d9096;
    cursor: pointer;
}
#cancel-add-form:hover{
    background-color: #b8bcc3;
}
#cancel-add-form:focus{
    border:1px solid #2ca01c;
}
#save{
    margin: 0px 0px 0px 8px;
}
#save:focus{
    border:1px solid #2ca01c;
}
#edit{
    margin: 0px 0px 0px 8px;
}
#edit:focus{
    border:1px solid #2ca01c;
}
</style>