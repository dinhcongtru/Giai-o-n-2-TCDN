<template>
    <div class="ms-content-main" @click="closeRepairOption" @keydown="handleshortcuts">
        <div class="ms-toolBar">
            <div class="ms-toolBar-content">
                <div class="title">{{Resource.CONTENT.contentVendor}}</div>
                <ms-button :style="'btn-add'" :id="'add-employee'" title="Thêm mới " @click="onToggleModal"/>
            </div>
        </div>
        <div class="inner-overview flex">
            <div class="w30 FF8C00">
                <div class="inner-total">4.646,0</div>
                <span class="size14">Nợ quá hạn</span>
                <span class="funnel-icon"></span>
            </div>
            <div class="w30 margin7 afb3b8">
                <div class="inner-total">4.646,0</div>
                <span class="size14">Tổng nợ phải trả</span>
                <span class="funnel-icon"></span>
            </div>
            <div class="w30 d637">
                <div class="inner-total">4.646,0</div>
                <span class="size14">Đã thanh toán (30 ngày gần đây)</span>
                <span class="funnel-icon"></span>
            </div>
        </div>
        <div class="ms-content">
            <div class="row">
                
                <div class="data-table" >
                    <div class="row-r">
                    <div class="toolBar-left">
                        <div class="mi-arrow-check-all"></div>
                        <div class="delete-all">
                            <ms-button  class="excuteDeleteBatch" title="Thực hiện hàng loạt" :class="[{ dropup: isDelActive }]" @click="onToggleDelBatch()" />
                            <span class="icon-delete"></span>
                            <ms-button v-if="isDelActive" id="deleteBatch"  @click="onDeleteBatch()" title="Xóa"/>
                            <div
                                class="overlay-feature"
                                v-if="isDelActive"
                                @click="ToggleDropdown"
                            ></div>
                        </div>
                        <div class="delete-all filter">
                            <ms-button class="excuteDeleteBatch" title="Lọc" />
                            <span class="icon-delete"></span>
                        </div>
                    </div>
                   
                    <div class="row-right">
                        <ms-input type="text" :autoFocus="true" :isHasLabel="false" placeholder="Tìm theo mã, tên nhân viên" id="search-employee"  styleClass="input-search" ref="search" v-model="filter" autocomplete="off"/>
                        <span class="icon-search"></span>
                        <span class="icon-reload" title="Làm mới" @click="onRefresh"></span>
                        <span class="icon-xuatkhau" title="Xuất ra Excel" @click="exportExcel()"></span>
                        <span class="mi-setting__list" title="Tùy chỉnh giao diện" @click="openSettingDialog"></span>
                    </div>
                </div>
                    <div class="main-table">
                        <!-- <ms-gird
                        ref="tableVendor"
                        :listHeader="listHeader"
                        :listData="vendors"
                        :pageSize="pageSize"
                        :pageNumber="pageNumber"
                        :vendors="vendors"
                        :idForm="isShowModal && idForm" 
                        @checkAll="toggleCheckAll($event)" 
                        @addForm="onToggleModal" 
                        @pageFilter="changePageNumber($event)" 
                        @refreshData="onRefresh"
                        :isShowSkeleton="isShowSkeleton"
                        functionText="Sửa"
                        @onUpdate="updateObject"
                        @onDelete="handleDelete"
                        @eShowFunc="showFunction"
                        @handleHideFunc="hideFunction"
                        @eMultiSelect="getVendorSelected"
                        /> -->
                        <the-table ref="tableVendor" :pageSize="pageSize" :pageNumber="pageNumber"  :vendors="vendors" :idForm="isShowModal && idForm" @checkAll="toggleCheckAll($event)" @addForm="onToggleModal" @pageFilter="changePageNumber($event)" @refreshData="onRefresh" @focusInput="focusInput" />
                    </div>
                </div>
                <div class="pagination">
                    <div class="total__page">
                        <p>{{Resource.TOOLBAR.totalCout}} <span>{{ totalPage || 0 }}</span>{{Resource.TOOLBAR.reCord}}</p>
                    </div>
                    <div class="f-nv">
                        <ms-dropdown-page 
                            type="text"
                            @pageSize="getPageSize($event)" />
                            <div class="pagination-table-row margleft-16">
                                <span class="pagination-table-row-number">{{
                                    updatePage(totalPage)
                                }}</span>
                                <span class="pagination-table-row-title">bản ghi</span>
                            </div>
                            <div class="pagination-button">
                                <div
                                    class="pagination-button-prev margleft-16"
                                    :class="[{ button_prev : `${this.pageSize * (this.pageNumber - 1) + 1}` == 1}]"
                                    :disabled = "`${this.pageSize * (this.pageNumber - 1) + 1}` == 1"
                                    @click="onPrevPage"
                                ></div>
                                <div
                                    class="pagination-button-next margleft-16"
                                    :class="[{ button_next : `${this.pageSize * this.pageNumber}` > totalPage}]"
                                    :disabled = "`${this.pageSize * this.pageNumber}` > totalPage"
                                    @click="onNextPage"
                                ></div>
                            </div>
                    </div>
                </div>
            </div>
        </div>
       <!--
      Truyền list header(cột của bảng tùy chỉnh giao diện) và list các cột(=>hàng của bảng tùy chỉnh giao diện)
    -->
    <ms-dialog-setting
      ref="settingDialog"
      :isRight="true"
      @eUpdateColumn="updateColumn"
      :listData="listColumnConfig"
    />
    <ms-dialog
    v-show="isDialog"
    dialogName="deleteBatch"            
    @closeDialog="onDeleteEmployee"
    @deleteSuccess="onRefresh"
    :selected="selected"
    @closeSelect="toggleCheckAll($event)"
    />
<ms-loading  v-if="isLoading"/>


</div> 
</template>
<script>
// import VendorDetailsVue from './VendorDetails.vue';
import msDialogSetting from "@/components/base/dialog/ms-dialogSetting.vue"
import msButton from '../../components/base/button/ms-button.vue';
import msInput from '../../components/base/input/ms-input.vue';
import TheTable from '../../components/TheTable.vue';
import msDialog from '../../components/base/dialog/ms-dialog.vue';
import msLoading from '../../components/base/loading/ms-loading.vue';
import Resource  from '@/Resource/Resource';
import keyCode from '@/enum/keyCode';
import axios from 'axios';
// import msGird from '@/components/base/grid/ms-grid.vue'
//import my js
import Enum from "@/js/enum";
import {RepositoryFactory} from '@/Repository/RepositoryFactory';
const VendorRepository = RepositoryFactory.get('Vendors');

// import TheModal from './TheModal.vue';
import msDropdownPage from '../../components/base/ms-dropdownPage.vue';
export default {
name:"TheVendor",
components:{msButton, msInput ,msDropdownPage ,msDialog,msLoading,TheTable,msDialogSetting},
mounted(){
this.focusInput();

    /**
     * Thực hiện render dữ liệu nhân viên
     **  Author: Đinh Công Trứ(26/10/2022)
        */
        this.onLoadData();
        
},
watch:{
    //  /**
    //      * Thực hiện đưa dữ liệu tổng sô bản ghi 
    //      **  Author: Đinh Công Trứ(26/10/2022)
    //      */
    //      employees() {
    //          this.totalPage;
    //     },
     // Thực thi hàm Tìm kiếm
     filter() {
            this.onFilterData(this.filter);
            
        },
        // Thực thi load lại trang
    refresh() {
        this.onLoadData();
    },
    /**
     * Thực hiện xử lý load dữ liệu chuyển trang
     **  Author: Đinh Công Trứ(26/10/2022)
     */
    pageNumber() {
        // Nếu ở trang 2 trở lên mà muốn tìm kiếm thì trang sẽ trở về 1
        if (this.filter) {
            this.onFilterData(this.filter, this.pageNumber);
        } else {
            // Load lại trang
            this.onLoadData();
        }
    },
    /**
    //  * Thực hiện Load lại dữ liệu khi chuyển pageSize
    //  **  Author: Đinh Công Trứ(26/10/2022)
    //  */
    pageSize() {
        this.onLoadData();
    },
},
    

data(){
return{
    isSmallSibar: false,
    sibarWidth: "sibar-small",
    isShowModal: false,
    isPaginationActive: false,
    totalPage: null,
    pageSize: "25",
    pageNumber: "1",
    filter: "",
    refresh: false,
    title: "",
    isChecked: false,
    idForm: "add-vendor",
    selected: [],
    isDialog: false,
    isLoading: false,
    idEmployee: [],
    // Employeeid: null,
    // codeEmployee: null,
    isDelActive: false,
    pageNumber2:1,
    Resource : Resource,
    keyCode: keyCode,
    closeSelect: false,
    isDropdown: false,
    pageFilter: this.pageNumber,
    // danh sách nhà cung cấp
    vendors:{},
     //danh sách các tiêu đề và fieldName của bảng
     listHeader: [

     ],
     isShowSkeleton: false, //có đang hiện skeleton ko?
     formMode: 0, //trạng thái form đang là gì?
};
},
methods: {
    onDelBatch(){
        this.isDialog = false;
        this.selected=[];
    },
focusInput(){
    this.$refs.search.$refs.input.focus();
},
handleshortcuts(e){
    if(e.keyCode == keyCode.keyCode.insert){
        this.onToggleModal();
    }
},
  /**Thực hiện ẩn TheDropdown
     * Author: Đinh Công Trứ(30/10/2022)
     */
     ToggleDropdown() {
        this.isDelActive = !this.isDelActive;
    },
 /**
         * Thực hiện xử lý tìm kiếm dữ liệu hiển thị vào trong bảng
         **  Author: Đinh Công Trứ(26/10/2022)
         */
         onFilterData(filter, pageNumber) {
            this.isLoading = true;
            if (pageNumber > 1) {
                this.pageFilter = pageNumber;
            } else {
                this.pageFilter = 1;
            }
            try {
                VendorRepository.getVendorByFilterAndPagding(this.pageSize,this.pageNumber,filter)
                .then((response) => {
                    this.vendors = response.data.data;
                    this.totalPage = response.data.totalCount;
                    this.$emit("pageFilter", this.pageFilter);
                    this.isLoading = false;
                })
                .catch((error) => {
                    console.log(error);
                    if(error.response.status == 500){
                            this.isError = !this.isError;
                            this.errorMgs = Resource.ERROR.filterDataError;
                        }else {
                            this.isError = !this.isError;
                            this.errorMgs = error.response.data.userMsg;
                        }
                });
            } catch (error) {
                console.log(error);
            }
            
        },
        /**
         * Thực hiện xử lý load data vào table
         **  Author: Đinh Công Trứ(26/10/2022)
         */
         onLoadData() {
            this.isLoading = true;
            try {
                
                VendorRepository.getVendorByPagding(this.pageSize,this.pageNumber)
                .then((response) => {
                    this.vendors = response.data.data;
                    this.vendors.forEach((element) => {
                        element.IsChecked = false;
                    });
                 
                    this.totalPage = response.data.totalCount;

                    this.isLoading = false;
                    // this.$emit("focusInput");

                })
                .catch((error) => {
                    console.log(error);
                    if(error.response.status == 500){
                            this.isError = !this.isError;
                            this.errorMgs = Resource.ERROR.loadDataError;
                        }else {
                            this.isError = !this.isError;
                            this.errorMgs = error.response.data.userMsg;
                        }

                });
            } catch (error) {
                this.isError = !this.isError;
                this.errorMgs = "Lấy dữ liệu thất bại!, Vui lòng thử lại sau.";
            }
           
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
        this.codeEmployee = code;
        this.idEmployee = id;
    }
},
/**
 * Thực hiện xử lý ẩn hiện dropdown deleteBatch
 **  Author: Đinh Công Trứ(26/10/2022)
 */
 onToggleDelBatch() {
   
    if(this.selected.length > 0){
        this.isDelActive = true;
    }else{
        this.isDelActive = false;

    }
},
/**
 * Thực hiện xử lý UI khi click feature Xóa hiện thị Dialog Xóa nhân viên
 **  Author: Đinh Công Trứ(30/10/2022)
 */
 onDeleteEmployee() {
    
    this.isDialog = !this.isDialog;
    this.isDropdown = false;
    this.featureDropdown = null;
    this.showDelete = null;
    this.selected = [];
    this.$refs.tableVendor.unCheckAll();
    this.onRefresh();
    // reload lại data
},
/**
 * Thực hiện ẩn hiện form TheModal
 * Author: Đinh Công Trứ(30/10/2022)
 */
 onToggleModal() {
    this.isShowModal = !this.isShowModal;
},
/**
 * Thực hiện thu nhỏ sibar
 * Author: Đinh Công Trứ(30/10/2022)
 */
 toggleSibar(){
    this.isSmallSibar = !this.isSmallSibar;
    this.emitter.emit("toggle-sibar", this.isSmallSibar);
 },
 /**
 * Thực hiện UI xử lí chuyền dữ liệu ẩn hiện phân trang chọn pageSize
 **  Author: Đinh Công Trứ(26/10/2022)
 */
onTogglePagination() {
    this.isPaginationActive = !this.isPaginationActive;
},
/**
 * Thực hiện hiện tổng số bản ghi
 **  Author: Đinh Công Trứ(26/10/2022)
 */
getTotalPage(data) {
    this.totalPage = data;
},
/**
 * Thực hiện xử lý chuyển dữ liệu pageSize vào table
 **  Author: Đinh Công Trứ(26/10/2022)
 */
getPageSize(page) {
    
    this.pageSize = page;
    this.onLoadData();
},
/**
 * Thực hiện xử lý chuyển dữ liệu pageNumber vào table để chuyển trang
 **  Author: Đinh Công Trứ(26/10/2022)
 */
onNextPage() {
    if (Math.ceil(this.totalPage / this.pageSize) > this.pageNumber) {
        this.pageNumber++;
        this.pageNumber2++
        this.onLoadData();
    }
   
},
/**
 * Thực hiện xử lý chuyển dữ liệu pageNumber vào table để chuyển trang
 **  Author: Đinh Công Trứ(26/10/2022)
 */

onPrevPage() {
    if (this.pageNumber > 1) {
        this.pageNumber = this.pageNumber - 1;
        this.pageNumber2--
        this.onLoadData();
    }
},
/**
 * Thực hiện xử lý chuyển dữ liệu pageNumber vào table để tìm kiếm nhân viên
 **  Author: Đinh Công Trứ(26/10/2022)
 */
onFilterEmployee(value) {
    this.filter = value;
    
},
/**
 * Thực hiện xử lý chuyển dữ liệu pageNumber vào table làm mới
 **  Author: Đinh Công Trứ(26/10/2022)
 */
onRefresh() {
    this.refresh = !this.refresh;
    this.onLoadData();
    this.pageNumber = this.pageNumber2;
},
/**
 * Thực hiện xử lý Số trang và hiện lên view
 **  Author: Đinh Công Trứ(26/10/2022)
 */
updatePage(totalPage) {        
    return `${this.pageSize * (this.pageNumber - 1) + 1} - ${
        this.pageSize * this.pageNumber > totalPage
            ? totalPage
            : this.pageSize * this.pageNumber
    } `;
   
},
/**
 * Thực hiện xử lý xóa toàn bộ bản ghi
 **  Author: Đinh Công Trứ(26/10/2022)
 */

toggleCheckAll(e) {
    
    this.isChecked = !this.isChecked;
    this.selected = e;
    this.isChecked = false;
    this.isDialog = false;
    this.isDelActive = false;

   
   
},
/**
 * Thực hiện xử lý chuyển trang về trang đầu tiên khi tìm kiếm nhân viên
 **  Author: Đinh Công Trứ(26/10/2022)
 */
changePageNumber(e) {
    this.pageNumber = e;
},
/**
 * Thực hiện xử lý UI khi click feature Xóa hiện thị Dialog Xóa hàng loạt
 **  Author: Đinh Công Trứ(30/10/2022)
 */
onDeleteBatch(){
    if(this.selected.length > 0){
        this.isDialog = !this.isDialog;
        this.showDelete = null;
        this.isDelActive = false;
       
    }
   
},
 /**------------------------------------------
     * Hàm xử lý nhận sự kiện sửa từ table.
     * @param {Object} object - 1 đối tượng cần sửa.
     * Author: quyetkaito (23/02/2022)
     --------------------------------------------*/
    async updateObject({ data }) {
      //lưu lại đối tượng đang thao tác
      this.objectTemp = data;
      //mở dialog và formMode là sửa.
      this.openDialog({
        data: await this.getObject(),
        formMode: Enum.FormMode.Put,
      });
    },
    /**-----------------------------------------------
     * Hàm xử lý khi nút [Xóa] được click.
     * @param {Object} vendor - 1 đối tượng cần xóa (nhận được từ grid)
     * Author: quyetkaito (25/02/2022)
     ------------------------------------------------*/
     handleDelete(vendor) {
      if (vendor) {
        this.objectTemp = vendor;
        //hiện dialog cảnh báo
        this.showPopup(
          2,
          `Bạn có thực sự muốn xóa nhà cung cấp <${this.objectTemp.account_object_code}> không?`
        );
      }
    },
     /**---------------------------------------------------------------
     * Hàm thực hiện show table function Xóa/Nhân bản/ Ngừng sử dụng.
     * @param {Object} object - đối tượng nhà cung cấp.
     * Author: quyetkaito (08/03/2022)
     ----------------------------------------------------------------*/
     showFunction(object) {
      if (this.isShowFunc) {
        this.isShowFunc = false;
      } else {
        this.isShowFunc = true;
        //lưu lại đối tượng
        this.objectTemp = object;
      }
    },
      /**------------------------------------
     * Hàm thực hiện hide table function.
     * Author: quyetkaito (08/03/2022)
     -------------------------------*/
     hideFunction() {
      this.isShowFunc = false;
    },
 /**-----------------------------------------------------
     * Lấy danh sách id của các vendor được chọn từ bảng.
     * @param {Array} value - mảng các id nhà cung cấp.
     * Author: quyetkaito (14/03/2022)
     -------------------------------------------------------*/
     getVendorSelected(value) {
      //lưu danh sách các vendor được chọn
      this.listVendorChecked = Object.values(value);
    },
    /**
     * Thực hiện xử lý sự kiện mở tùy chỉnh giao diện
     * Author: Đinh Công Trứ(25/11/2022)  
     */
    openSettingDialog(){
          //show setting dialog
      this.$refs.settingDialog.show();
    },
/**
 * Thực hiện xử lý xuất Excel
 **  Author: Đinh Công Trứ(25/11/2022)
 */
exportExcel(){
    try {
        VendorRepository.exportExcel()
        .then((res) => {
                res.config.responseType = "blob";
                axios(res.config).then((res) => {
                    const url = window.URL.createObjectURL(
                        new Blob([res.data])
                    );
                    const link = document.createElement("a");
                    link.href = url;
                    link.setAttribute("download", "nhacungcap.xlsx");
                    document.body.appendChild(link);
                    link.click();
                });
            })
            .catch((err) => {
                console.log(err);
            });
    } catch (error) {
        console.log(error);
    }
}
},
}
</script>
<style>

</style>