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
                                    <ms-input  
                                        class="colum-ma"
                                        labelText="Mã số thuế"
                                        ref="input"
                                        styleClass="inp-ma"
                                        id="txtVendorCode"
                                        tabInput="1"
                                        v-model="vendor.taxCode"
                                        autocomplete="off"  
                                        :maxlength="20"
                                    />  
                                    <ms-input 
                                        class="colum-name"
                                        labelText="Mã nhà cung cấp"
                                        :isRequire="true"
                                        styleClass="inp-name"
                                        v-model="vendor.vendorCode"
                                        tabInput="2"
                                        :error="error.vendorCode" 
                                        :blur="validateVendorCode(vendor.vendorCode)" 
                                        autocomplete="off"
                                        :maxlength="20"
                                    />
                                </div>
                                <div class="chucvu">
                                    <ms-input 
                                        class="colum-cv"
                                        labelText="Tên nhà cung cấp"
                                        :isRequire="true"
                                        styleClass="inp-ma chucdanh"
                                        tabInput="3"
                                        id="txtChucDanh" 
                                        v-model="vendor.vendorName"
                                        :error="error.vendorName" 
                                        :blur="validateVendorName(vendor.vendorName)" 
                                        autocomplete="off"
                                        :maxlength="255"
                                    />
                                </div> 
                                <ms-input
                                    class="info-donvi"
                                    labelText="Địa chỉ"
                                    styleClass="inp-ma chucdanh padding20"
                                    tabInput="4" 
                                    v-model="vendor.address" 
                                    autocomplete="off"
                                    placeholder="VD: Số 82 Duy Tân, Dịch Vọng Hậu, Cầu Giấy, Hà Nội"
                                 />
                        </div>
                        <div class="cont-r">
                            <div class="info-ma colum-r">
                                <ms-input 
                                    class="colum-ma"
                                    classLaybel="ma"
                                    labelText="Điện thoại"
                                    styleClass="inp-ma"
                                    id="txtEmployeeCode" 
                                    tabInput="5" 
                                    :maxlength="20"
                                    v-model="vendor.phoneNumber" 
                                    autocomplete="off"
                                />

                                    <ms-input 
                                        class="colum-name"
                                        classLaybel="name"
                                        labelText="Website"
                                        styleClass="inp-name"
                                        tabInput="6" 
                                        :maxlength="100"
                                        v-model="vendor.website" 
                                        autocomplete="off"
                                    />

                            </div>
                            <div class="info-donvi" id="id5">
                                <div class="name-dv">
                                    <div class="font700">{{ labelText }}
                                        
                                    </div>
                                </div>
                                <div class="combobox inf-chucvu">
                                    <!-- <multiselect
                                    v-model="vendor.groupVendorID"
                                    :options="listGroupVendor"
                                    label="label"
                                    mode="tags"
                                    :close-on-select="true"
                                    :clearOnSelect="true"
                                    :searchable="true"
                                    noResultsText="Không có dữ liệu hiển thị."
                                    :disabled="false"
                                    :canClear="false"
                                    :canDeselect="false"
                                    /> -->
                                    <!-- <multiselect
                                     v-model="value" 
                                    :options="listGroupVendor"
                                     :multiple="true" 
                                     :close-on-select="false" 
                                     :clear-on-select="false"
                                    :preserve-search="true"
                                     placeholder="Pick some" 
                                    noResultsText="Không có dữ liệu hiển thị."
                                     label="groupVendorCode"
                                    track-by="groupVendorCode" 
                                    :preselect-first="true"/> -->
                                    <!-- <VueMultiselect
                                    v-model="value"
                                    :options="listPaymentTerm"
                                    :preserve-search="false"
                                    :multiple="true"
                                    :close-on-select="false"
                                    placeholder="Pick some"
                                    label="code"
                                    track-by="code" 
                                    /> -->
                                </div>
                            </div>
                            <ms-combobox
                                tabInput="7"
                                :options="listGroupVendor"
                                v-model="vendor.groupVendorCode"
                                :appendToBody="true"
                                :searchable="true"
                                idStyle="id5"
                                :listHeader="headerVendor"
                                :closeOnSelect="false"
                                labelText="Nhóm nhà cung cấp"
                                label="label"
                                :lazyLoading="true"
                                
                            />
                            <!-- <DxTagBox :items="simpleProducts"/> -->
                            <ms-combobox
                                tabInput="8"
                                :options="listEmployeeOptions"
                                v-model="vendor.employeeID"
                                :appendToBody="true"
                                :searchable="true"
                                idStyle="id4"
                                :listHeader="headerEmployee"
                                :closeOnSelect="true"
                                labelText="Nhân viên mua hàng"
                                label="label"
                                :lazyLoading="true"
                                @eLoadNext="loadNextEmployee"
                                
                            /> 
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
                                                <ms-dropdown-info
                                                    type="text"
                                                    title=""
                                                    idStyle = "id3"
                                                    id="width162"
                                                    tabindex="9"
                                                    label="Xưng hô"
                                                    :itemData="votive"
                                                    v-model="vendor.vocative"
                                                    :autoFocus="true"
                                                />
                                                <!-- <ms-input type="text" :style="'inp-ma'" :id="'width162'"   autocomplete="off"/> -->
                                                <ms-input 
                                                    
                                                    
                                                    :isHasLabel="false"
                                                    styleClass="inp-name"
                                                    id="txtContact" 
                                                    placeholder="Họ và tên" 
                                                    tabInput="10" 
                                                    v-model="vendor.contact" 
                                                    autocomplete="off"
                                                />
                                                <!-- <ms-input type="text" :style="'inp-name'" id="txtContact" placeholder="Họ và tên" tabindex="10" v-model="vendor.contact" autocomplete="off"/> -->
                                            </div>
                                        </div>
                                </div>
                                <div class="chucvu">
                                    <ms-input
                                         styleparent="width100"
                                        class="colum-ma margin6"
                                        :isHasLabel="false"
                                        styleClass="inp-ma chucdanh"
                                        id="txtChucDanh" 
                                        placeholder="Email" 
                                        tabInput="11"
                                        v-model="vendor.contactEmail"  
                                        autocomplete="off"
                                    />

                                </div> 
                                <div class="info-ma first">
                                    <ms-input 
                                         class="colum-name didong tkgh"
                                        :isHasLabel="false"
                                        styleClass="inp-name inp-didong tkgh"
                                        placeholder="Số điện thoại" 
                                        tabInput="12" 
                                        id="txtTKNH" 
                                        v-model="vendor.contactPhone" 
                                        autocomplete="off"
                                    />
                                
                                </div>
                            </div>
                            <div class="cont-r">
                                <div class="chucvu">
                                    <ms-input
                                         styleparent="width100"
                                        class="colum-ma"
                                        classLaybel="name-cv"
                                        labelText="Đại diện theo PL"
                                        styleClass="inp-ma chucdanh"
                                        placeholder="Đại diện theo PL" 
                                        name="input" 
                                        v-model="vendor.agentLaw" 
                                        tabInput="13" 
                                        id="txtChucDanh"  
                                        autocomplete="off"
                                    />

                                </div> 
                            
                            </div>
                        </div>
                        <div class="content-pog-bot margin8" v-if="tabActive === 1">
                            <div class="info-ma">
                                <ms-combobox
                                :class="'GroupVendor'"
                                v-model="vendor.rule"
                                :options="listPaymentTerm"
                                :appendToBody="true"
                                :autoFocus="true"
                                tabInput="10"
                                idStyle="id2"
                                :searchable="true"
                                :listHeader="headerPaymentTerm"
                                :closeOnSelect="true"
                                labelText="Điều khoản thanh toán"
                                label="label"
                                :lazyLoading="true"
                              
                            />
                                <ms-input 
                                     class="colum-name coding"
                                     classLaybel="name coding"
                                     labelText="Số ngày được nợ"
                                     styleClass="inp-name inp-coding paddingLeft"
                                     :isNumberInput="true"
                                     name="input" 
                                     v-model="vendor.dayOwed" 
                                     tabInput="11" 
                                     id="txtDTCoDinh"  
                                     autocomplete="off"
                                     :maxlength="18"
                                />

                                <ms-input 
                                     class="colum-name email"
                                     classLaybel="name email"
                                     labelText="Số nợ tối đa"
                                     styleClass="inp-name inp-email paddingLeft"
                                     :isNumberInput="true"
                                     name="input" 
                                     v-model="vendor.maximumDebt" 
                                     tabInput="12" 
                                     id="txtEmail"  
                                     autocomplete="off"
                                     :maxlength="18"
                                />
 
                            </div>
                            <div class="info-ma end">
                                <ms-combobox
                                :class="'GroupVendor'"
                                :options="listaccountPaymentTerm"
                                v-model="vendor.acountPayable"
                                :appendToBody="true"
                                tabInput="13"
                                :isPlus="false"
                                idStyle="id1"
                                :searchable="true"
                                :listHeader="accountPaymentTerm"
                                :closeOnSelect="true"
                                labelText="Tài khoản công nợ phải trả"
                                label="label"
                                :lazyLoading="true"
                                
                            />
                                
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
        <ms-loading v-if="isLoading" />
        </div>
</template>
<script>
// import vMultiselect from "@vueform/multiselect";
// import "@vueform/multiselect/themes/default.css";
// import TheInputVue from "./TheInput.vue";
// import CompTabs from './CompTabs.vue';
import msCombobox from '../../components/base/combobox/ms-combobox.vue'
import msButton from '../../components/base/button/ms-button.vue';
// import msDropdown from "../../components/base/ms-dropdown.vue";
import msLoading from "../../components/base/ms-loading.vue";
import msDropdownInfo from "../../components/base/ms-dropdownInfo.vue";
import msRadio from "../../components/base/ms-radio.vue";
import msDialog from '../../components/base/dialog/ms-dialog.vue'
import Resource from "@/Resource/Resource";
import keyCode from "@/enum/keyCode";
import msInput from "@/components/base/input/ms-input.vue";
import {RepositoryFactory} from '@/Repository/RepositoryFactory';
 const VendorRepository = RepositoryFactory.get('Vendors');
import DepartmentRepository from "@/Repository/DepartmentRepository";
// import DxTagBox from 'devextreme-vue/tag-box';
// import Multiselect from 'vue-multiselect'
export default {
    name: "VendorDetails",
    components: {
        
        msCombobox,
        msButton,
        msRadio,
        msDialog,
        msInput,
        msDropdownInfo,
        msLoading,
        
    },
    props: {
        id: String,
        vendorCode: String,
        vendorID: String,
        
    },
    created() {
        // Xử lý UI form thêm hoặc sửa trước khi mở form
        this.handleUI();
        
        this.getNewVendorCode();
        
        // Lấy danh sách phòng ban đưa lên UI
        //  this.getDepartment();
        // Lấy dữ liệu nhân viên đưa vào trong form
       
          this.userInfo(this.vendorCode);

        
    },
    // Tự động focus mã nhân viên khi form mở
    mounted() {
        // this.$refs.input.focus();
        this.$refs.input.$refs.input.focus();
        this.vendorOld = structuredClone(this.vendor);
        this.getPaymentEmployee();
        this.getGroupVendor();
        this.vendor.acountPayable = this.listaccountPaymentTerm[0].code
        // this.vendor = {...this.vendor, acountPayable: this.listaccountPaymentTerm[0].code}
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
                if(JSON.stringify(this.vendor) == JSON.stringify(this.vendorOld)){
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
        // getGender(event) {
        //     this.employee.gender = event;
        // },
        /**
         * Thực hiện lấy dữ liệu đơn vị gồm id và tên
         **  Author: Đinh Công Trứ(2/11/2022)
         */
        //  getDepartmentId(event) {
        //     this.employee.departmentID = event;
    
        // },
        // getDepartmentName(event) {
        //     this.employee.departmentName = event;
            
            
        // },
        /**
         * Thực hiện xử lý validate dữ liệu mã nhân viên
         **  Author: Đinh Công Trứ(2/11/2022)
         */
        validateVendorCode(code) {
            if (!code) {
                this.error.vendorCode = false;
            } else {
                this.error.vendorCode = true;
            }

        },
        /**
         * Thực hiện xử lý validate dữ liệu tên nhân viên
         **  Author: Đinh Công Trứ(2/11/2022)
         */
        validateVendorName(name) {
            let me = this;
            return () => {
                
                if (!name) {
                    me.error.vendorName = "form-input-error";
                } else {
                    me.error.vendorName = "";
                }
            }
        },
        /**
         * Thực hiện validate dữ liệu khi ấn lưu
         **  Author: Đinh Công Trứ(2/11/2022)
         */
        validateForm() {
            if (!this.vendor.vendorCode) {
                this.error.vendorCode = "form-input-error";
                this.isError = !this.isError;
                this.errorMgs = Resource.ERROR.vendorCodeError;
               return false;
               
            } else {
                this.error.vendorCode = "";
            }
            if (!this.vendor.vendorName) {
                this.error.vendorName = "form-input-error";
                this.isError = !this.isError;
                this.errorMgs = Resource.ERROR.vendorNameError;
               return false;
               
            } else {
                this.error.vendorName = "";
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
       async getNewVendorCode() {
            if (this.id =="add-vendor" || this.id == "nhanban-form") {
                const me = this;
                try{
                   await VendorRepository.getNewVendorCode()
                    .then((response) => {                    
                        this.vendor.vendorCode = response.data.VendorCode;  
                        this.vendorOld.vendorCode = response.data.VendorCode;                        
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

      async userInfo(id) {
            try {
                if (this.id == "edit-vendor") {
                    const me = this;
                    me.isLoading = true;
                   await VendorRepository.getInfoVendor(id)
                    .then((response) => {
                            me.vendor = response.data.data[0];
                            me.vendorOld=structuredClone(me.vendor);
                           
                            
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
                     me.isLoading = false;
                }
                if (this.id == "nhanban-form") {                  
                    const me = this;
                    VendorRepository.getInfoVendor(id)
                    .then((response) => {
                     
                     me.vendor = response.data.data[0];
                     this.vendorOld=structuredClone(this.vendor);   
                    me.getNewVendorCode();

                 })
                 
                }
            } catch (error) {
                this.isError = !this.isError;
                this.errorMgs = Resource.ERROR.loadDataError;
            }
        },
         /**------------------------------------------
        * Lấy danh sách nhân viên mua hàng.
        *Author: Đinh Công Trứ(2/11/2022)
        --------------------------------------------*/
        async getPaymentEmployee() {
            try{
                let me = this;
                let pageSize = 10;
                me.isLoading = true;
                await VendorRepository.getVendorByPagding(pageSize)
                    .then((res) => {
                    for (const i of res.data.data) {
                        let object = {};
                        object.label = i.employeeName;
                        object.value = i.employeeID;
                        object.code = i.employeeCode;
                        //build mảng các department để truyền vào combobox
                        me.listEmployeeOptions.push(object);
                    }
                    // console.log(this.listEmployeeOptions);
                    })
                    .catch((error) => {
                    console.log(error);
                    });
                    me.isLoading = false;
            } catch(error){
                this.isError = !this.isError;
                this.errorMgs = Resource.ERROR.loadDataError;
            }
        },
        /**------------------------------------------
        * Lấy danh sách nhóm nhà cung cấp.
        *Author: Đinh Công Trứ(2/11/2022)
        --------------------------------------------*/
        async getGroupVendor() {
            try{
                let me = this;
                let pageSize =10;
                me.isLoading = true;
                await VendorRepository.getVendorByPagding(pageSize)
                    .then((res) => {
                    for (const y of res.data.data) {
                        let object = {};
                        object.label = y.groupVendorName;
                        object.value = y.groupVendorID;
                        object.code = y.groupVendorCode;
                        //build mảng các department để truyền vào combobox
                        me.listGroupVendor.push(object);
                        // console.log(t
                    }
                    })
                    .catch((error) => {
                    console.log(error);
                    });
                    me.isLoading = false;
            } catch(error){
                this.isError = !this.isError;
                this.errorMgs = Resource.ERROR.loadDataError;
            }
        },
        /**------------------------------------------
    *Lazyloading thêm data từ server.
    *Author: Đinh Công Trứ(2/11/2022)
    --------------------------------------------*/
   async loadNextEmployee() {
        try{
        let me = this;
        me.emPageSize = me.emPageSize + 10;
        // me.listEmployeeOptions = [];
       me.isLoading = true;
    // console.log(this.emPageSize);
      await VendorRepository.getVendorByPagding(me.emPageSize)
        .then((res) => {
          //build combobox cho employee
          me.listEmployeeOptions = [];
        //   let x = res.data.Data;
          for (const x of res.data.data) {
                        let obj = {};
                        obj.label = x.employeeName;
                        obj.value = x.employeeID;
                        obj.code = x.employeeCode;
                        
                        me.listEmployeeOptions.push(obj);
                    }
    });
  
    me.isLoading = false;
        // this.getPaymentEmployee()
        }catch(error) {
            this.isError = !this.isError;
            this.errorMgs = Resource.ERROR.loadDataError;
            }
     },
        /**
         * Thực hiện Lưu dữ liệu thêm mới hoặc sửa nhà cung cấp
         **  Author: Đinh Công Trứ(2/11/2022)
         */

        save() {
            const validate = this.validateForm();

            const me = this;
            // Thêm mới nhà cung cấp
            if (validate && this.id == "add-vendor") {
                try {
                    VendorRepository.postVendor(this.vendor)
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
            // Nhân bản nhà cung cấp
            if (validate && this.id == "nhanban-form") {
               
               try {
                VendorRepository.postVendor(this.vendor)
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
            // Sửa nhà cung cấp
            if ( validate && this.id == "edit-vendor") {
               
               try {
                VendorRepository.updateVendor(this.vendorID,this.vendor)
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
         * Thực hiện xử lý Lưu nhà cung cấp khi ấn button thêm và cất
         **  Author: Đinh Công Trứ(2/11/2022)
         */
        saveAndAdd() {
            const validate = this.validateForm();

            const me = this;
            if (validate && this.id == "add-form") {
                
                try {
                    VendorRepository.postVendor(this.vendor)
                    .then(function () {
                        me.$emit("refreshData");
                        me.vendor = {};
                        
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
            // nhân bản nhà cung cấp
            if (validate && this.id == "nhanban-form") {
               
                try {
                VendorRepository.postVendor(this.vendor)
                .then(function () {
                        me.$emit("refreshData");
                        me.vendor = {};
                        
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
            // Sửa nhà cung cấp
            if (validate && this.id == "edit-vendor") {
                
                try {
                VendorRepository.updateVendor(this.vendorID,this.vendor)
                .then(() => {
                        me.$emit("refreshData");
                        me.vendor = {};                      
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
            labelText:"",
            keyCode : keyCode,
            Resource :Resource,
            vendorOld: {},
            posts: [],
            isWarring: false,
            isDialog: false,
            error: {
                vendorCode: true,
                vendorName: true,
                departmentName: true,
            },
            isLoading :false,
            vendor: {},
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
                
            votive:[
                { name : "Anh"},
                { name : "Bà"},
                { name : "Bạn"},
                { name : "Chị"}, 
                { name : "Ông"},
             

            ],
           
            MsTabs:[
                { name : "Thông tin liên hệ"},
                { name : "Điều khoản thanh toán"},
                
            ],
            tabActive : 0,
            //option cho danh sách nhân viên
            listEmployeeOptions: [], 
            //pagesize Employy default 10
            emPageSize:10,
            //option cho danh sách nhóm nhà cung cấp
            listGroupVendor:[],
            listPaymentTerm:[
                {label:"Điều Khoản 1",value:"Điều Khoản 1",code:"DK1"},
                {label:"Điều Khoản 2",value:"Điều Khoản 2",code:"DK2"},
                {label:"Điều Khoản 3",value:"Điều Khoản 3",code:"DK3"},
                {label:"Điều Khoản 4",value:"Điều Khoản 4",code:"DK4"},
    
            ],
            listaccountPaymentTerm:[
                {label:"Phải trả cho người bán",value:"331",code:"331"},
                {label:"Phải trả người lao động khác",value:"3348",code:"3348"},
                {label:"Thuế TTĐB (nếu có)",value:"3332 ",code:"3332 "},
                {label:"Thuế bảo vệ môi trường",value:"33381  ",code:"33381  "},
            ],
            simpleProducts: [
                {
                    id: 1, name: "Iphone11"
                },
                {
                    id: 2, name: "Iphone12"
                },
                {
                    id: 3, name: "Iphone13"
                },
                {
                    id: 4, name: "Iphone14"
                },
                {
                    id: 5, name: "Iphone15"
                }
            ],
            headerEmployee: [
                {
                text: "Mã nhân viên",
                fieldName: "code",
                columnWidth: 150,
                },
                {
                text: "Tên nhân viên",
                fieldName: "label",
                columnWidth: 231,
                },
            ],
            headerVendor:[
                { fieldName: 'code', text: 'Mã Nhóm KH, NCC', columnWidth: 150, },
                { fieldName: 'label', text: 'Tên Nhóm KH, NCC', columnWidth: 231, },
            ],
            headerPaymentTerm:[
                      { fieldName: 'code', text: 'Mã điều khoản thanh toán', columnWidth: 180, },
                      { fieldName: 'label', text: 'Tên điều khoản thanh toán', columnWidth: 240, },
                    ],
            accountPaymentTerm:[
                { fieldName: 'code', text: 'Số tài khoản', columnWidth: 130,},
                { fieldName: 'label', text: 'Tên tài khoản', columnWidth: 180, },
            ],
            value:[]
        };
    },
};
</script>
<style>
/* vendorDEtils*/


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
    width: 60%;
    align-items: end;
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
#txtContact{
    margin-left: 8px;
}
.GroupVendor{
    margin-right: 8px;
    width: 222px;
    margin-top: 0;
}
.end{
    padding-top: 12px !important;
}
.paddingLeft{
    padding-right: 8px !important;
}
.width100{
    width:100%;
}
</style>