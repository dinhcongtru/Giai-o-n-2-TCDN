<template>
    <div class="row-dialog" :id="id" @keydown = "handleshortcuts">
            <div class="pogup">
                <div class="head-pog">
                    <div class="head-tolBar">
                        <div class="tt-head-pog" id="inf-editnv">{{ title }}</div>
                        <div class="midle-head">
                            <div class="check-head-pog">
                                <ms-radio
                                   
                                    radioName="radio"
                                    :radioData="radio"
                                    v-model="datagender"
                                
                                    />
                                <div class="custom">
                                    <input type="checkbox" class="check-custom" name="" id="" >
                                <span style="padding-left: 10px">{{Resource.MODAL.isCustom}}</span>
                                </div>
                                
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
                                        <div class="ma">Mã số thuế
                                            <!-- <span style="color: red;">*</span> -->
                                        </div>
                                        <ms-input ref="input" type="text" :style="'inp-ma'"  :id="'txtEmployeeCode'"  tabindex="1" :autoFocus="true" v-model="employee.telephoneNumber" autocomplete="off"/>
                                        
                                    </div>
                                    <div class="colum-name">
                                            <div class="name">Mã nhà cung cấp
                                                <span style="color: red;">*</span>
                                            </div>
                                            <ms-input type="text" :style="'inp-name'"  v-model="employee.employeeCode"  tabindex="2"  required :error="error.employeeCode" :blur="validateEmployeeCode(employee.employeeCode)" autocomplete="off"/>
                                        
                                    </div>
                                </div>
                                <div class="chucvu">
                                    <div class="colum-cv">
                                        <div class="name-cv">Tên nhà cung cấp
                                            <span style="color: red;">*</span>
                                        </div>
                                        <ms-input type="text" class="inp-ma chucdanh"  name="input"  tabindex="3" id="txtChucDanh" v-model="employee.employeeName" required :error="error.employeeName" :blur="validateEmployeeName(employee.employeeName)" autocomplete="off"/>
                                    </div>
                                </div> 
                                <div class="info-donvi">
                                    <div class="name-dv">
                                        <div class="font700">Địa chỉ</div>
                                    </div>
                                   <input type="text" placeholder="VD: Số 82 Duy Tân, Dịch Vọng Hậu, Cầu Giấy, Hà Nội" class="inp-ma chucdanh padding20" tabindex="4" v-model="employee.addRess" autocomplete="off">
                                </div>      
                        </div>
                        <div class="cont-r">
                            <div class="info-ma colum-r">
                                <div class="colum-ma">
                                        <div class="ma">Điện thoại
                                            <!-- <span style="color: red;">*</span> -->
                                        </div>
                                        <ms-input type="text" :style="'inp-ma'" :id="'txtEmployeeCode'" tabindex="5" v-model="employee.phoneNumber" autocomplete="off"/>
                                        
                                    </div>
                                    <div class="colum-name">
                                            <div class="name">Website
                                                
                                            </div>
                                            <ms-input type="text" :style="'inp-name'" tabindex="6" autocomplete="off"/>
                                        
                                    </div>
                            </div>
                            <div class="info-donvi">
                                    <div class="name-dv">
                                        <div class="font700">Nhóm nhà cung cấp
                                            
                                        </div>
                                    </div>
                                    <ms-dropdown
                                    type="text"
                                    title="Đơn vị không được để trống."
                                   
                                    id="DepartmentName"
                                    tabindex="7"
                                    idStyle = "id5"
                                    :departmentList="departmentList"
                                    :departmentId="employee.departmentID"
                                    :departmentName="employee.departmentName"
                                    
                                    @departmentId="getDepartmentId($event)"
                                    @departmentName="getDepartmentName($event)"
                                />
                               
                                </div> 
                                <div class="info-donvi">
                                    <div class="name-dv">
                                        <div class="font700">Nhân viên mua hàng
                                           
                                        </div>
                                    </div>
                                    <ms-dropdown
                                    type="text"
                                    title="Đơn vị không được để trống."
                                  
                                    id="DepartmentName"
                                    tabindex="8"
                                    idStyle = "id4"
                                    :departmentList="departmentList"
                                    :departmentId="employee.departmentID"
                                    :departmentName="employee.departmentName"
                                    
                                    @departmentId="getDepartmentId($event)"
                                    @departmentName="getDepartmentName($event)"
                                />
                               
                                </div>
                        </div>
                    </div>
                    <div class="content-pog-b" style="width:100%">
                        <div class="con-ms-ul-tabs tab-other">
                            <ul class="ul-tabs ms-tabs-ul ms-ul-tabs-left">
                                <li v-for="(item, index) in MsTabs" :key="index" class="ms-tabs-li" :class="{activeItem : tabActive == index}" @click="setActiveTab(index)">
                                    <div class="item-tabs" type="button">{{ item.name }}</div>
                                </li>
                            </ul>
                          
                           
                            <!-- <comp-tabs/> -->
                        </div>
                        <div class="border1px">
                        <div class="content-pog-t" v-if="tabActive === 0">
                            <div class="cont-l margin8" >
                                <div class="info-ma colum-r">
                                    <div class="colum-ma">
                                            <div class="ma">Người liên hệ </div>
                                            <div class="merge-row">
                                                <ms-dropdown
                                                    type="text"
                                                    title="Đơn vị không được để trống."
                                                    idStyle = "id3"
                                                    id="width162"
                                                    tabindex="9"
                                                    label="Xưng hô"
                                                    :departmentList="departmentList"
                                                    :departmentId="employee.departmentID"
                                                    :departmentName="employee.departmentName"
                                                   
                                                    @departmentId="getDepartmentId($event)"
                                                    @departmentName="getDepartmentName($event)"
                                                />
                                                <!-- <ms-input type="text" :style="'inp-ma'" :id="'width162'"   autocomplete="off"/> -->
                                                <ms-input type="text" :style="'inp-name'" placeholder="Họ và tên" tabindex="10" autocomplete="off"/>
                                            </div>
                                        </div>
                                </div>
                                <div class="chucvu">
                                    <div class="colum-ma margin6">
                                    
                                        <ms-input type="text" class="inp-ma chucdanh" placeholder="Email" name="input"  tabindex="11" id="txtChucDanh"   autocomplete="off"/>
                                    </div>
                                </div> 
                                <div class="info-ma first end">
                                    <div class="colum-name didong tkgh">
                                        
                                        <input type="text" class="inp-name inp-didong tkgh" name="input" placeholder="Số điện thoại" tabindex="12" id="txtTKNH"  autocomplete="off">
                                    </div>
                                
                                </div>
                            </div>
                            <div class="cont-r">
                                <div class="chucvu">
                                    <div class="colum-ma">
                                        <div class="name-cv">Đại diện theo PL</div>
                                        <ms-input type="text" class="inp-ma chucdanh" placeholder="Đại diện theo PL" name="input"  tabindex="13" id="txtChucDanh"  autocomplete="off"/>
                                    </div>
                                </div> 
                            
                            </div>
                        </div>
                        <div class="content-pog-bot margin8" v-if="tabActive === 1">
                            <div class="info-ma">
                                <div class="colum-name didong">
                                    <div class="name didong">Điều khoản thanh toán</div>
                                        <ms-dropdown
                                            type="text"
                                            title="Đơn vị không được để trống."
                                            :autoFocus="true"
                                            id="width206"
                                            tabindex="10"
                                            idStyle = "id2"
                                            :departmentList="departmentList"
                                            :departmentId="employee.departmentID"
                                            :departmentName="employee.departmentName"
                                            
                                            @departmentId="getDepartmentId($event)"
                                            @departmentName="getDepartmentName($event)"
                                        />
                                </div>
                                <div class="colum-name coding">
                                        <div class="name coding">Số ngày được nợ</div>
                                        <input type="text" class="inp-name inp-coding" name="input"  tabindex="11" id="txtDTCoDinh"  autocomplete="off">
                                </div>
                                <div class="colum-name email">
                                    <div class="name email">Số nợ tối đa</div>
                                    <input type="text" class="inp-name inp-email" name="input"  tabindex="12" id="txtEmail"  autocomplete="off">
                                    
                                </div>
                            </div>
                            <div class="info-ma end">
                                <div class="colum-name didong payment">
                                    <div class="name didong">Tài khoản công nợ phải trả</div>
                                        <ms-dropdown
                                            type="text"
                                            title="Đơn vị không được để trống."
                                            idStyle = "id1"
                                            id="width206"
                                            tabindex="13"
                                            
                                            :departmentList="departmentList"
                                            :departmentId="employee.departmentID"
                                            :departmentName="employee.departmentName"
                                            
                                            @departmentId="getDepartmentId($event)"
                                            @departmentName="getDepartmentName($event)"
                                        />
                                </div>
                                
                            </div>
                        </div>       
                    </div>
                    </div>   
                    
                </div>
                <div class="f-pog">
                    <button class="f-pog-l closeEmployee" tabindex="14" id="cancel-add-form" title="Hủy(Ctrl F9)" @click="onCancelModal">{{Resource.BUTTON.HUY}}</button>
                    <div class="f-pog-r">
                        <ms-button  class="cat-pog" tabindex="15" id="cancel-add-form" title="Cất" type="submit" @click="save()"/>
                        <ms-button  tabindex="16" :id="idButton" :title="titleButton" class="add-pog" type="submit" :class="[id == 'edit-vendor']" @blur="tabOrder" @click="saveAndAdd(e)"/>
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
// import CompTabs from './CompTabs.vue';
import msButton from '../../components/base/ms-button.vue';
import msDropdown from "../../components/base/ms-dropdown.vue";
import msRadio from "../../components/base/ms-radio.vue";
import msDialog from '../../components/base/ms-dialog.vue'
import Resource from "@/Resource/Resource";
import keyCode from "@/enum/keyCode";
import msInput from "@/components/base/ms-input.vue";
import {RepositoryFactory} from '@/Repository/RepositoryFactory';
 const EmployeeRepository = RepositoryFactory.get('Employees');
//  const DepartmentRepository = RepositoryFactory.get('Departments');
import DepartmentRepository from "@/Repository/DepartmentRepository";


export default {
    name: "VendorDetails",
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
         * Xử lý tabcomponents
         * Author : Đinh Công Trứ (2/11/2022)
         */
         setActiveTab(index){
            this.tabActive = index;
         },
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
        tabOrder() {
            this.$refs.input.$refs.input.focus();
        },
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
                this.title = Resource.TITLE.AddVendor;
                this.idButton = "save";
                this.titleButton = Resource.BUTTON.CATTHEM;
            }
          
            // Sửa nhà cung cấp
            if (this.id == "edit-vendor") {
                this.title = Resource.TITLE.EditVendor;
                this.idButton = "edit";
                this.titleButton = Resource.BUTTON.CATTHEM;
            }
            // Nhân bản nhân viên
            if (this.id == "nhanban-form") {
                this.title = Resource.TITLE.AddVendor;
                this.idButton = "edit";
                this.titleButton = Resource.BUTTON.CATTHEM;
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
                this.isError = !this.isError;
                this.errorMgs = Resource.ERROR.vendorCodeError;
               return false;
               
            } else {
                this.error.employeeCode = "";
            }
            if (!this.employee.employeeName) {
                this.error.employeeName = "form-input-error";
                this.isError = !this.isError;
                this.errorMgs = Resource.ERROR.vendorNameError;
               return false;
               
            } else {
                this.error.employeeName = "";
            }
            // if (!this.employee.departmentName) {
            //     this.error.departmentName = "form-input-error";
            //     this.isError = !this.isError;
            //     this.errorMgs = Resource.ERROR.departmentError;
            //    return false;
            // }

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
            if (this.id == "edit-vendor" || this.id == "nhanban-form" || this.id =="add-vendor") {
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
                if (this.id == "edit-vendor") {
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
            if (validate && this.id == "add-vendor") {
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
            } else{
                this.isWarring =  false;
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

            } else{
                this.isWarring =  false;
            }
            // Sửa nhân viên
            if ( validate && this.id == "edit-vendor") {
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
               
            } else{
                this.isWarring =  false;
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
               
            } else{
                this.isWarring =  false;
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
   
            } else{
                this.isWarring =  false;
            }
            // Sửa nhân viên
            if (validate && this.id == "edit-vendor") {
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
                
            } else{
                this.isWarring =  false;
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
            datagender:"Tổ chức",
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
            },
            radio:["Tổ chức","Cá nhân"],
                
            
            MsTabs:[
                { name : "Thông tin liên hệ"},
                { name : "Điều khoản thanh toán"},
                
            ],
            tabActive : 0,
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
.con-ms-ul-tabs{
    position: relative;
}
.ms-tabs-ul{
    margin: 0;
    width: 100%;
    height: 100%;
    padding-left: 0;
    position: relative;
    display: flex;
}
.tab-other .ms-tabs-ul{
    z-index: 2;
    align-items: flex-end;
}
ul {
    list-style-type: none;
}
.tab-other .ms-tabs-ul .ms-tabs-li{
    height: 26px;
    margin-right: 2px;
    border: 1px solid #c9ccd2;
    border-bottom: none;
    border-radius: 0px 2px 0px 0;
}
.tab-other .ms-tabs-ul .activeItem{
    background: #cce4f9;
    height: 32px;
}
.item-tabs{
    border-radius: unset!important;
    height: 100%;
    margin: 0;
    padding: 0 8px;
    display: flex;
    place-items: center;
}
ul li .item-tabs:hover{
    color: #2ca01c;
    cursor: pointer;
}
.margin0{
    margin: 0;
}
.margin8{
    margin-left: 8px;
}
#width162{
    width: 162px !important;
    margin-right: 8px;
}
.border1px{
    border: 1px solid #babec5;
    padding: 12px 0px;
    min-height: 216px;
}
.margin6{
    margin-top: 6.5px;
}
.font700{
    font-weight: 700;
}
.merge-row {
    display: flex;
}
.margin8{
    margin-left: 8px;
}
#width206{
    width: 206px !important;
}
.payment{
    margin-top: 12px ;
}
</style>