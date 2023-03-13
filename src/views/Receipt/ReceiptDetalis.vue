<template>
    <div class="base-view-detail" :id="id" @keydown = "handleshortcuts">
        <div class="header-layout">
            <div class="top-header">
                <div class="recent-log-btn"></div>
                <div class="title-payment">{{ title }}</div>
                <div class="header-detail-input">
                    <ms-dropdown-info
                        type="text"                 
                        id="width334"
                        tabindex="1"
                        :autoFocus="true"
                        :isReadonly="true"
                        ref="input"
                        idStyle="id9"
                        :itemData="optionReceipt"
                        v-model="optionDefault"
                    />
                </div>
                <div class="header-detail-buttons">
                    <div class="guide-tour">
                        <div class="mi-tour"></div>
                        <div class="tour-laybel">Hướng dẫn</div>
                    </div>
                    <div class="con-ms-tooltip" title="Tùy chỉnh giao diện" @click="openSettingDialog">
                        <div class="mi-setting-detail" ></div>
                    </div>
                    <div class="con-ms-tooltip" title="Giúp(F1)">
                        <div class="mi-help"></div>
                    </div>
                    <div class="con-ms-tooltip" @click="onCloseModal" title="Đóng(ESC)">
                        <div class="mi-close"></div>
                    </div>
                </div>
            </div>
        </div>
        <div class="body-layout">
            <div class="layout-top">
                <div class="w-4/5 right-separate">
                    <div class="merge">
                        <div class="obj-one">
                            <ms-combobox
                                class="obj-ma revMargin"
                                v-model="payment.account_object_id"
                                :options="listObject"
                                :appendToBody="true"
                                tabInput="2"
                                idStyle="id8"
                                value="value"
                                :searchable="true"
                                :listHeader="headerObjectCode"
                                :closeOnSelect="true"
                                labelText="Mã đối tượng"
                                label="label"
                                :lazyLoading="true"    
                                @eLoadNext="getObjectOptions"
                                @onSelected="handleSelected"
                                :disabled="this.id == 'edit-form' ? true : false"
                                @eSearching="getObjectOptions"                         
                            />

                            <ms-input
                            class="obj-name"
                            classLaybel="obj-ma-title"
                            styleClass="input-objName"
                            :tabInput="3"
                            labelText="Tên đối tượng"
                            autocomplete="off"
                            :maxlength="255"
                            v-model="payment.account_object_name"
                             @input="bindDescription(payment.account_object_name)"
                            :disabled="this.id == 'edit-form' ? true : false"
                            />

                        </div>
                        <div class="obj-two">
                            <ms-input
                            class="obj-ma"
                            classLaybel="obj-ma-title"
                            styleClass="input-nhan"
                            :tabInput="4"
                            labelText="Người nhận"
                            autocomplete="off"
                            
                            />
                            <ms-input
                            class="obj-name"
                            classLaybel="obj-ma-title"
                            styleClass="input-objName"
                            :tabInput="5"
                            labelText="Địa chỉ"
                            autocomplete="off"
                            
                            />
                        </div>
                        <div class="obj-three">
                            <ms-input
                            class="regen"
                            classLaybel="obj-ma-title"
                            styleClass="w-full"
                            :tabInput="6"
                            labelText="Lý do chi"
                            
                            v-model="payment.journal_memo"
                            :disabled="this.id == 'edit-form' ? true : false"
                            @input="onMasterReasonInput(payment.journal_memo)"
                            autocomplete="off"
                            
                            />
                        </div>
                    </div>
                    <div class="obj-four">
                        <ms-combobox
                                class="obj-ma revMargin"
                                labelClass="obj-ma-title"
                                tabInput="7"
                                :options="listEmployeeOptions"
                                
                                :appendToBody="true"
                                :searchable="true"
                                idStyle="id7"
                                :listHeader="headerEmployee"
                                :closeOnSelect="true"
                                labelText="Nhân viên"
                                label="label"
                                :lazyLoading="true"
                                @eLoadNext="loadNextEmployee"
                                
                            /> 
                        <div class="merge-row">
                            <ms-input
                            class="kem-theo"
                            classLaybel="obj-ma-title"
                            styleClass="input-number"
                            :tabInput="8"
                            labelText="Kèm theo"
                            :isNumberInput="true"
                            :maxlength="18"
                            placeholder="Số lượng"
                            autocomplete="off"
                            
                            />
                            <div class="title-gim">chứng từ gốc</div>
                        </div>
                        
                    </div>
                    <div class="obj-five">
                        <div class="row-tham-chieu">Tham chiếu</div>
                    </div>

                </div>
                <div class="w-1/5 width20">
                    <div class="m-row left-separate">
                        <div class="obj-ma-title">Ngày hạch toán</div>
                        <DxDateBox 
                            drop-down-button-template="loadIcon" 
                            ref="posted_date"
                            v-model:value="payment.posted_date"
                            :format="dateFormat"
                            placeholder="Ngày hạch toán"
                            :value="formatDate()" 
                            :disabled="this.id == 'edit-form' ? true : false"
                            @change="onPostedDateChanged($event)" 
                            @pick="onPostedDatePicked($event)"
                            tabIndex="9">
                            <template #loadIcon="{}">
                                <div class="iconDatePicker"
                        >
                                    
                                </div>
                            </template>
                        </DxDateBox>
                    </div>
                    <div class="m-row left-separate">
                        <div class="obj-ma-title">Ngày phiếu chi</div>
                        <DxDateBox 
                        drop-down-button-template="loadIcon" 
                        ref="ref_date"
                        v-model="payment.ref_date"
                        :format="dateFormat"
                        placeholder="Ngày phiếu chi"
                        :disabled="this.id == 'edit-form' ? true : false"
                        :value="formatDate()" 
                        tabIndex="10">
                            <template #loadIcon="{}">
                            <div class="iconDatePicker"
                        >
                                
                            </div>
                            </template>
                        </DxDateBox>
                    </div>
                    <ms-input
                            class="m-row left-separate"
                            classLaybel="obj-ma-title"
                            styleClass="datepacker sochi"
                            :tabInput="11"
                            labelText="Số phiếu chi"
                            :blur="validatePaymentCode(payment.ca_payment_code)" 
                            ref="ca_payment_code"
                            v-model="payment.ca_payment_code"
                             :disabled="this.id == 'edit-form' ? true : false"
                            autocomplete="off"                           
                            />
 
                </div>
                <div class="w-1/4 summary-info">
                    <div class="summary-info-title">Tổng tiền</div>
                    <h1 class="summary-info-number">
                        {{ payment.total_amount ? payment.total_amount : "0,0" }}
                    </h1>
                </div>
            </div>
            <div class="layout-bottom">
                <div class="header-bottom">
                    <div class="title-bottom">Hạch toán</div>
                    <ms-combobox
                        styleInput="w100"
                        labelClass="labelPayment"
                        class="merge-bottom revMargin"
                        styleDrop="right20"
                        v-model="payment.currency_id"
                        :disabled="this.id == 'edit-form' ? true : false"
                        :options="typeMoney"
                        :appendToBody="true"
                        :isPlus="false"
                        tabInput="12"
                        idStyle="id6"
                        :searchable="true"
                        :listHeader="headerTypeMoney"
                        :closeOnSelect="true"
                        labelText="Loại tiền"
                        label="label"
                        :lazyLoading="true"
                        
                    />
                </div>
                <div class="layout-bot-main">
                    <div class="main-layout-table">
                        <ms-grid-editable
                        ref="gridPaymentDetail"
                        :isCheckable="false"
                        :isCount="true"
                        :showFooter="true"
                        :listHeader="listHeader"
                        :listData="payment_detail_list"
                        :disabled="this.id == 'edit-form' ? true : false"
                        @eAddRow="addRow"
                        @eRemoveRow="removeRow"
                        @eRemoveAllRow="handleRemoveAllRow"
                        @eNumberInput="handleNumberInput"
                        @eComboSelected="handleComboSelected"
                    />
                       
                    </div>
                </div>
                <div class="main-child">
                    <div class="ms-child">
                        <button class="btn-addRow btn-btn" tabindex="13" @click="addRow">Thêm dòng</button>
                        <button class="btn-deleteRow btn-btn" id="deleteAllRows" tabindex="14" @click="onDelleteAllRows">Xóa hết dòng</button>
                    </div>
                    <div class="ms-child-2">
                        <div class="merge-child">
                            <div class="mi-attach"></div>
                            <div>Đính kèm</div>
                        </div>
                        <div class="max-size-upload">Dung lượng tối đa 5MB</div>
                    </div>
                    <input type="text" readonly name="" class="upload-main" id="" placeholder="Kéo/thả tệp vào đây hoặc bấm vào đây">
                </div>
            </div>
        </div>
        <div class="footer-layout">
            <div class="f-pog">
                <button class="f-pog-l closeEmployee white unhover btn-btn" tabindex="15" id="cancel-add-form" title="Hủy(Ctrl F9)" @click="onCancelModal">{{Resource.BUTTON.HUY}}</button>
                <div class="f-pog-r">
                    <button  class="cat-pog white unhover btn-btn" tabindex="16" id="cancel-add-form" title="Cất" type="submit" @click="save()" >Cất </button>
                    <button  tabindex="17" :id="idButton" :title="titleButton" class="add-pog flex1 btn-btn" type="submit" :class="[id == 'edit-form']" @blur="tabOrder" @click="saveAndAdd(e)">Cất và In</button>
                </div>
            </div>
        </div>
        <ms-dialog-setting
        ref="settingDialog"
        :isRight="true"
        @eUpdateColumn="updateColumn"
        :listData="listColumnConfig"
        />
        <ms-dialog
             ref="mMessage"
             v-show="isDialog"
            dialogName="deleteAllRows"
            @eConfirmDelete="removeAllRow"
            @closeDialog="this.isDialog = false"
        />
</div>
</template>
<script>
// import TheInputVue from "./TheInput.vue";
// import CompTabs from './CompTabs.vue';
// import TheButton from "../../components/TheButton.vue";
import msGridEditable from "@/components/base/grid/ms-gridEditTable.vue";
import msDialogSetting from '@/components/base/dialog/ms-dialogSetting.vue'
import { locale, loadMessages } from "devextreme/localization";
import deMessages from "devextreme/localization/messages/de.json";
import msCombobox from "@/components/base/combobox/ms-combobox.vue";
// import msDropdown from "../../components/base/ms-dropdown.vue";
import msDropdownInfo from "@/components/base/ms-dropdownInfo.vue";
// import TheRadio from "../../components/TheRadio.vue";
import msDialog from '@/components/base/dialog/ms-dialog.vue'
import Resource from "@/Resource/Resource";
import keyCode from "@/enum/keyCode";
import msInput from "@/components/base/input/ms-input.vue"
// import TheInput from "@/components/TheInput.vue";
import {RepositoryFactory} from '@/Repository/RepositoryFactory';
 const EmployeeRepository = RepositoryFactory.get('Employees');
//  const DepartmentRepository = RepositoryFactory.get('Departments');

import DxDateBox from 'devextreme-vue/date-box';
import {
formatCurrencyToSave,
  compareDate,
  formatNumber
  
} from "@/js/base";
// import Enum from "@/js/enum";
export default {
    name: "ReceiptDetail",
    components: {
        msCombobox,
        msDropdownInfo,
        DxDateBox,
        msInput,
        msGridEditable,
        msDialogSetting,
        msDialog
    },
    props: {
        id: String,
        employeeCode: String,
        employeeID: String,
        
    },
    created() {
        loadMessages(deMessages);
        locale(navigator.language);
    },
    beforeMount() {
        // Xử lý UI form thêm hoặc sửa trước khi mở form
        this.handleUI();
        // Xử lý Lấy mã nhân viên mới
        // this.getNewEmployeeCode();
        // Lấy danh sách phòng ban đưa lên UI
        // this.getDepartment();
        // Lấy dữ liệu nhân viên đưa vào trong form
        this.userInfo(this.employeeCode);
    },
    // Tự động focus mã nhân viên khi form mở
    mounted() {
        // this.$refs.inputFocus?.focus();
        this.payment.currency_id = this.typeMoney[0].value;
        this.employeeOld = structuredClone(this.employee);
        this.payment.journal_memo = "Chi tiền cho";
        this.payment.posted_date = this.formatDate();
        this.payment.ref_date = this.formatDate();
    },
    
    methods: {
         /**------------------------------------------
    *Xử lý khi ngày hạch toán được chọn
    *Author: quyetkaito (25/04/2022)
    --------------------------------------------*/
    onPostedDatePicked(event) {
      // console.log("ngày đang chọn", event);
      // console.log("ngày hạch toán hiện tại:", this.payment.posted_date);
      // console.log("ngày chứng từ hiện tại:", this.payment.ref_date);
      if (
        this.id == "add-form" ||
        this.id == "nhanban-form"
      ) {
        //nếu khi thêm mới mà ngày hạch toán bằng ngày chứng từ=> thay đổi theo
        if (compareDate(this.payment.ref_date, this.payment.posted_date) == 0) {
          //gán ngày chứng từ bằng ngày hạch toán(nếu là thêm mới)
          this.payment.ref_date = event;
          this.payment.posted_date = event;
        } else {
          this.payment.posted_date = event;
        }
      }
    },
     /**------------------------------------------
    *Loại bỏ css lỗi cho, bắt sự kiện khi ngày tháng (thay đổi)
    *Author: quyetkaito (23/04/2022)
    --------------------------------------------*/
    onPostedDateChanged() {
      //bỏ css lỗi khi validate
      this.$refs.posted_date.children[0].children[0].classList.remove(
        "error"
      );
    },
    /**------------------------------------------
    *Hàm bind lý do chi + ten đối tượng
    *@param {String} value: tên đối tượng
    *Author: quyetkaito (21/04/2022)
    --------------------------------------------*/
    bindDescription(value) {
      this.payment.journal_memo = `Chi tiền cho ${value}`;
      if (this.payment_detail_list.length > 0) {
        this.payment_detail_list[0].description = `Chi tiền cho ${value}`;
      }
    },
  /**------------------------------------------
    * Xử lý khi lý cho chi nhập vào.
    *Author: quyetkaito (25/04/2022)
    --------------------------------------------*/
    onMasterReasonInput(value) {
      //thay đổi lý do chi detail theo
      if (this.payment_detail_list.length > 0) {
        for (let i = 0; i < this.payment_detail_list.length; i++) {
          this.payment_detail_list[i].description = `${value}`;
        }
      }
    },
     /**------------------------------------------
    *Nhận value từ ô input số của table.
    *@param {String} value: giá trị của số nhận được từ table.
    *@param {String} fieldName: tên cột của ô input number đó
    *Author: quyetkaito (17/04/2022)
    --------------------------------------------*/
    handleNumberInput({ fieldName }) {
      // console.log("nhan dc", value);
      // console.log("fieldName", fieldName);

      //tạo vòng for tính tổng
      let total = 0;
      for (let i = 0; i < this.payment_detail_list.length; i++) {
        total =
          total + formatCurrencyToSave(this.payment_detail_list[i][fieldName]);
      }

      //total bên ngoài => chuyển thành text
      this.payment.total_amount = formatNumber(total.toString());
      if (this.payment.total_amount == 0) {
        //format nếu số = 0.
        this.payment.total_amount = "0,0";
      }

      //(total trong bảng) tìm trong header để bind data đúng cột đó thôi
      for (let i = 0; i < this.listHeader.length; i++) {
        if (this.listHeader[i].fieldName == fieldName) {
          // console.log("đang tính toán cột: ", fieldName);
          this.listHeader[i].total_amount = this.payment.total_amount;
        }
      }
    },
    /**-----------------------------------
     * Thêm hàng mới trong bảng hạch toán
     * Author: quyetkaito (03/04/2022)
     -------------------------------------*/
     async addRow() {
      //số phiếu chi detail hiện tại.
      let detail_length = this.payment_detail_list.length;
      //thêm hàng mới
      if (detail_length == 0) {
        await this.payment_detail_list.push({});
      } else {
        await this.payment_detail_list.push({
          description: this.payment_detail_list[detail_length - 1].description,
          amount: "0,0",
          account_object_id: this.payment_detail_list[detail_length - 1]
            ? this.payment_detail_list[detail_length - 1].account_object_id
            : "",
          account_object_name: this.payment_detail_list[detail_length - 1]
            ? this.payment_detail_list[detail_length - 1].account_object_name
            : "",
        });
      }

      //focus vào ô đầu tiên trong hảng
      this.$refs.gridPaymentDetail.focus(this.payment_detail_list.length);
    },
    /**------------------------------------------------
     * Hàm thực hiện xóa một hàng trong list hạch toán
     * @param {Number} index: xác định hàng đang xóa.
     * @author: quyetkaito (31/03/2022)
     -------------------------------------------------*/
     removeRow(index) {
      //xóa dòng đó khỏi mảng
      this.payment_detail_list.splice(index, 1);
      //tính lại tổng tiển
      this.calculateTotal();
    },
     /**------------------------------------------
    *Hàm tính cột tổng tiền của detail.
    *Author: quyetkaito (23/04/2022)
    --------------------------------------------*/
    calculateTotal() {
      try {
        //tính lại tổng tiền
        let total = 0;
        for (let i = 0; i < this.payment_detail_list.length; i++) {
          total =
            total + formatCurrencyToSave(this.payment_detail_list[i]["amount"]);
        }

        //total bên ngoài => chuyển thành text
        this.payment.total_amount = formatNumber(total.toString());
        if (this.payment.total_amount == 0) {
          //format nếu số = 0.
          this.payment.total_amount = "0,0";
        }

        //(total trong bảng) tìm trong header để bind data đúng cột tổng tiền
        for (let i = 0; i < this.listHeader.length; i++) {
          if (this.listHeader[i].fieldName == "amount") {
            this.listHeader[i].total_amount = this.payment.total_amount;
          }
        }
      } catch (error) {
        //log ra lỗi
        console.log(error);
      }
    },

     /**------------------------------------------
     * Hàm xử lý khi nút xóa hết dòng được chọn.
     * Author: quyetkaito (31/03/2022)
     -----------------------------------------*/
     handleRemoveAllRow() {
      //cảnh báo xóa dữ liệu nếu đang có data
      this.isError = !this.isError;
      this.errorMgs = Resource.ERROR.deleteAllRows;
    //   this.dialogName = "deleteAllRows";
    },
     /**----------------------------------------------------
     * Hàm thực hiện xóa hết hàng của bảng hạch toán
     * Author: quyetkaio (03/04/2022)
     ------------------------------------------------------*/
     removeAllRow() {
        this.isDialog = false;
      //clear list
      this.payment_detail_list = [];
      //push một đối tượng mới
      this.payment_detail_list.push(
        {description:this.payment.journal_memo,debit_account:"231",credit_account:"150",amount:"0,0",account_object_id:"1235112",account_object_name:"Nguyễn mạnh hùng"}
      );
    },
    onDelleteAllRows(){
        this.isDialog = !this.isDialog;
        this.isDropdown = false;
        this.featureDropdown = null;
        this.showDelete = null;
    },
        /**
         * Xử lý tab order
         * Author : Đinh Công Trứ (2/11/2022)
         */
        tabOrder() {
            this.$refs.input.$refs.input.focus();
        },
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
                this.title = "Phiếu chi pc161";
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
        validatePaymentCode(code){
                if (!code) {
                this.error.ca_payment_code = false;
            } else {
                this.error.ca_payment_code = true;
            }
            
        },
        /**
         * Thực hiện validate dữ liệu khi ấn lưu
         **  Author: Đinh Công Trứ(2/11/2022)
         */
        validateForm() {
        let valid = true;
      this.listError = [];
      //validate master
      //1. số phiếu chi không được bỏ trống
      if (!this.payment.ca_payment_code) {
                this.error.ca_payment_code = "form-input-error";
                this.isError = !this.isError;
                this.errorMgs = Resource.ERROR.errorChi;      
        valid = false;
      }
      //2.ngày hạch toán phải lớn hơn hoặc bằng ngày chứng từ
      if (compareDate(this.payment.posted_date, this.payment.ref_date) == -1) {
        // console.log("ngay hach toan hien tai", this.payment.posted_date);
        // console.log("ngay pc hien tai", this.payment.ref_date);
                 this.error.posted_date = "form-input-error";
                this.isError = !this.isError;
                this.errorMgs = Resource.ERROR.post_date;   
        valid = false;
      }
      //validate detail
      //1.tài khoản nợ không được phép bỏ trống
      this.payment_detail_list.forEach((element, index) => {
        if (!element.debit_account) {
          let refName = "debit_account" + index;
          this.$refs.gridPaymentDetail.addError(refName);
          this.error.debit_account = "form-input-error";
                this.isError = !this.isError;
                this.errorMgs = Resource.ERROR.debit_account;   
          valid = false;
        }
        //gửi event css lỗi cho grid.
      });
      //todo
      //thêm toàn bộ css cảnh báo lỗi cho ô input
      for (let i = 0; i < this.listError.length; i++) {
        if (
          this.listError[i].id != "posted_date" &&
          this.listError[i].id != "ref_date"
        ) {
          if (this.$refs[this.listError[i].id]) {
            this.$refs[this.listError[i].id].addError();
          }
        }
        if (
          this.listError[i].id == "posted_date" ||
          this.listError[i].id == "ref_date"
        ) {
          this.$refs[
            this.listError[i].id
          ].children[0].children[0].classList.add("error");
        }
      }
      return valid;
     },
        /**
         * Thực hiện format date ngày hiện tại
         **  Author: Đinh Công Trứ(2/11/2022)
         */
        formatDate() {
          
                var today = new Date(),
                    month = "" + (today.getMonth() + 1),
                    day = "" + today.getDate(),
                    year = today.getFullYear();

                if (month.length < 2) month = "0" + month;
                if (day.length < 2) day = "0" + day;

                return [year, month, day].join("-");
           
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
            if ( validate && this.id == "edit-form") {
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
                
            } else{
                this.isWarring =  false;
            }

        },
        /**
         * Thực hiện xử lý sự kiện mở tùy chỉnh giao diện
         * Author: Đinh Công Trứ(25/11/2022)  
         */
        openSettingDialog(){
            //show setting dialog
        this.$refs.settingDialog.show();
        },
    },
    data() {
        return {
            keyCode : keyCode,
            Resource :Resource,
            paymentOld: {},
            error: {
                ca_payment_code:true,
                posted_date:true,
                debit_account:true,
            },
            isWarring: false,
            isDialog: false,
            payment: {},
            idButton: "",
            titleButton: "",
            title: "",
            errorMgs: false,
            isError: false,
            departmentList: {},
            dateFormat:"DD/MM/yyyy",
            radioData:{
                Male: "Nam",
                Female: "Nữ",
                Orther: "Khác"
            },
            resion:"Chi tiền cho",
            defaultSPC:"pc161",
            radio:{
                Group: "Tổ chức",
                Sigle: "Cá nhân",
                
            },
            MsTabs:[
                { name : "Thông tin liên hệ"},
                { name : "Điều khoản thanh toán"},
                { name : "Tài khoản ngân hàng"},
                { name : "Địa chỉ khác"},
                { name : "Ghi chú"},
            ],
            typeMoney:[
                 {label:"Việt nam đồng",value:"VND",code:"VND"},
                 {label:"Đô la Mỹ",value:"USA",code:"USA"},
                // {label:"",value:"",code:"Điều Khoản 1"},
                // {label:"Điều Khoản 2",value:"Điều Khoản 2",code:"DK2"},
                // {label:"Điều Khoản 3",value:"Điều Khoản 3",code:"DK3"},
                // {label:"Điều Khoản 4",value:"Điều Khoản 4",code:"DK4"},
                
            ],
            headerTypeMoney:[
                { fieldName: 'code', text: 'Số tài khoản', columnWidth: 130,},
                { fieldName: 'label', text: 'Tên tài khoản', columnWidth: 130,},
            ],
            moneyDefault:"",
            optionReceipt:[
                
                { name : "1. Chi khác"},
            ],
           
            //option danh sách mã đối tượng
            listObject:[
                {label:"CÔNG TY TNHH MBS COLLECTIVE",value:"1235112",code:"1235112",tax_code:"mbs",address:"Số 35 Đặng Thai Mai, Thành phố Hà Nội, Việt Nam",phone_number:"0235823958"},
                {label:"CÔNG TY CỔ PHẦN MISA",value:"1221342",code:"1221342",tax_code:"mbs",address:"Số 35 Đặng Thai Mai, Thành phố Hà Nội, Việt Nam",phone_number:"0235823958"},
                {label:"CÔNG TY TNHH THEANH28",value:"1213112",code:"1213112",tax_code:"mbs",address:"Số 35 Đặng Thai Mai, Thành phố Hà Nội, Việt Nam",phone_number:"0235823958"},
                {label:"CÔNG TY TNHH ABC",value:"12312",code:"12312",tax_code:"mbs",address:"Số 35 Đặng Thai Mai, Thành phố Hà Nội, Việt Nam",phone_number:"0235823958"},
               
            ],
            listEmployeeOptions:[
                 {label:"Nguyễn mạnh hùng",value:"Nguyễn mạnh hùng",code:"NV001",department:"phòng nhân sự",phone_number:"0235823958"},
                {label:"Nguyễn quang thắng",value:"Nguyễn quang thắng",code:"NV002",department:"phòng hành chính",phone_number:"0235823958"},
                {label:"Nguyễn thị hạnh",value:"Nguyễn thị hạnh",code:"NV003",department:"phòng kế toán",phone_number:"0235823958"},
                {label:"Nguyễn quỳnh anh",value:"Nguyễn quỳnh anh",code:"NV004",department:"phòfng giám đốc",phone_number:"0235823958"},
            ],
            headerEmployee:[
            {
                text: "Mã nhân viên",
                fieldName: "code",
                columnWidth: 100,
                },
                {
                text: "Tên nhân viên",
                fieldName: "label",
                columnWidth: 150,
                },
                {
                text: "Đơn vị",
                fieldName: "department",
                columnWidth: 130,
                },
                {
                text: "ĐT di động",
                fieldName: "phone_number",
                columnWidth: 100,
                },
            ],
            headerObjectCode:[
            {
                text: "Đối tượng",
                fieldName: "code",
                columnWidth: 150,
                },
                {
                text: "Tên đối tượng",
                fieldName: "label",
                columnWidth: 231,
                },
                {
                text: "Mã số thuế",
                fieldName: "taxCode",
                columnWidth: 150,
                },
                {
                text: "Địa chỉ",
                fieldName: "address",
                columnWidth: 250,
                },
                {
                text: "Điện thoại",
                fieldName: "phoneNumber",
                columnWidth: 150,
                },
            ],
            optionDefault:["1. Chi khác"],
            tabActive : 0,
             //table trong form chi tiết
             payment_detail_list: [
                {description:"chi tiền cho",debit_account:"231",credit_account:"150",amount:"0,0",account_object_id:"1235112",account_object_name:"Nguyễn mạnh hùng"},
              
            ],
            listHeader:[{
                text: "DIỄN GIẢI",
                fieldName: "description",
                type:"input",
                columnWidth: 220,
                style:"inputGrid"
                },
                {
                text: "TK NỢ",
                fieldName: "debit_account",
                type:"selectbox",
                comboHeader:[
                    { fieldName: 'code', text: 'Số tài khoản', columnWidth: 130,},
                    { fieldName: 'label', text: 'Tên tài khoản', columnWidth: 130,},
                ],
                id :"ids1",
                comboOption:[
                    {label:"Việt nam đồng",value:"VND",code:"VND"},
                    {label:"Đô la Mỹ",value:"USA",code:"USA"},
                ],
                columnWidth: 173,
                },
                {
                text: "TK CÓ",
                fieldName: "credit_account",
                type:"selectbox",
                id:"ids2",
                comboHeader:[
                    { fieldName: 'code', text: 'Số tài khoản', columnWidth: 130,},
                    { fieldName: 'label', text: 'Tên tài khoản', columnWidth: 130,},
                ],
                comboOption:[
                    {label:"Việt nam đồng",value:"VND",code:"VND"},
                    {label:"Đô la Mỹ",value:"USA",code:"USA"},
                   
                ],
                columnWidth: 142,
                },
                {
                text: "SỐ TIỀN",
                fieldName: "amount",
                type:"input-number",
                columnWidth: 164,
                },
                {
                text: "ĐỐI TƯỢNG",
                fieldName: "account_object_id",
                type:"selectbox",
                columnWidth: 151,
                },
                {
                text: "TÊN ĐỐI TƯỢNG",
                fieldName: "account_object_name",
                columnWidth: 630,
                },
                {
                text: "TK NGÂN HÀNG",
                fieldName: "phoneNumber",
                type:"selectbox",
                columnWidth: 200,
                },
                {
                text: "KHẾ ƯỚC VAY",
                fieldName: "phoneNumber",
                type:"selectbox",
                columnWidth: 150,
                },
                {
                text: "KHOẢN MỤC CP",
                fieldName: "phoneNumber",
                type:"selectbox",
                columnWidth: 151,
                },
                {
                text: "TÊN KHOẢN MỤC CP",
                fieldName: "phoneNumber",
                columnWidth: 250,
                },
                {
                text: "ĐƠN VỊ",
                fieldName: "phoneNumber",
                type:"selectbox",
                columnWidth: 150,
                },
                {
                text: "TÊN ĐƠN VỊ",
                fieldName: "phoneNumber",
                columnWidth: 150,
                },
            ],
        };
    },
};
</script>
<style>
@import url(../../css/main.css);
#width334{
    width: 334px !important;
}
.inputGrid{
    width: calc(100% - 16px);
}
.w100{
    width: 100px !important;
}
.white{
    color: white !important;
}
.flex1{
    display: flex !important;
    justify-content: center !important;
    align-content: center !important;
}
.unhover{
    background: #38393d !important;
}
.unhover:focus{
    border: 1px solid #8d9096 !important;
}
.sochi{
    width: 189px;
    height: 28px;
    padding-left: 12px;
    font-family: "Helvetica Neue","Segoe UI",helvetica,verdana,sans-serif;
    line-height: 14.35715;
}
.btn-btn:focus{
    border: 1px solid #dcdcdc !important;
}
button.btn-btn {
    outline: none;
}
.dx-texteditor-input{
    min-height: 32px;
}
.dx-datebox {
    /* max-width: 60%; */
    height: 32px;
    max-width: 205px !important;
}
.dx-texteditor-input{
    padding:3px 12px 8px !important;
    border: none !important;
}
.dx-calendar-cell.dx-calendar-selected-date.dx-calendar-contoured-date{
    border-radius: 50%;
    background-color: rgba(44,160,28,.2);
    color: #08bf1e !important;
    font-weight: 600;
    width: 36px;
    height: 34px;
    box-shadow:none !important;
}
.dx-calendar-cell.dx-state-hover{
    cursor: pointer;
    border: 1px solid #2ca01c;
    transition: .1s ease-in-out;
    /* box-shadow: none !important; */
    border-radius: 50%;
}
/* .dx-calendar-body td{
    border: 1px solid #2ca01c !important;
} */
.labelPayment{
    width: 61px;
    margin: 0;
}
.right20{
    right: -20px;
}
.revMargin{
    margin-top: 0 !important;
}
table .table thead tr th{
    border:none !important;
}
table .table tbody tr td{
    border:none !important;
}
</style>