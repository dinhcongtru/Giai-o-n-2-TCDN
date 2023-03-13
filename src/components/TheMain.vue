<template>
            <div class="ms-content-main" @click="closeRepairOption">
                <div class="ms-toolBar">
                    <div class="ms-toolBar-content">
                        <div class="title">{{Resource.CONTENT.contentPage}}</div>
                        <ms-button class="btn-add" id="add-employee" title="Thêm mới nhân viên " @click="onToggleModal"/>
                    </div>
                </div>
                <div class="ms-content">
                    <div class="row">
                      
                        <div class="data-table" >
                            <div class="row-r">
                            <div class="toolBar-left">
                                <div class="mi-arrow-check-all"></div>
                                <div class="delete-all">
                                    <ms-button  class="excuteDeleteBatch" title="Thực hiện xóa hàng loạt" :class="[{ dropup: isDelActive }]" @click="onToggleDelBatch()" />
                                    <span class="icon-delete"></span>
                                    <ms-button v-if="isDelActive" id="deleteBatch" @click="onDeleteBatch()" title="Xóa"/>
                                
                                </div>
                                <div class="delete-all filter">
                                    <ms-button class="excuteDeleteBatch" title="Lọc" />
                                    <span class="icon-delete"></span>
                                </div>
                            </div>
                        
                            <div class="row-right">
                                <ms-input type="text" :autoFocus="true" placeholder="Tìm theo mã, tên nhân viên" id="search-employee" class="input-search" v-model="filter" autocomplete="off"/>
                                <span class="icon-search"></span>
                                <span class="icon-reload" title="Làm mới" @click="onRefresh"></span>
                                <span class="icon-xuatkhau" title="Xuất ra Excel" @click="exportExcel()"></span>
                                <span class="mi-setting__list"></span>
                            </div>
                             </div>
                        <div class="main-table">
                            <the-table @totalPage="getTotalPage($event)" @focusInput="focusInput" :pageSize="pageSize" :pageNumber="pageNumber" :filter="filter" :refresh="refresh" :idForm="isShowModal && idForm" @checkAll="toggleCheckAll($event)" @addForm="onToggleModal" @pageFilter="changePageNumber($event)" @refreshData="onRefresh" />
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
                <ms-dialog
            v-show="isDialog"
            dialogName="deleteBatch"            
            @closeDialog="onDeleteEmployee"
            :selected="selected"
            @closeSelect="toggleCheckAll($event)"
        />
        <ms-loading v-if="isLoading" />
        </div> 
</template>
<script>
import msButton from './base/button/ms-button.vue';
import msInput from './base/ms-input.vue';
import TheTable from './TheTable.vue';
import msDialog from './base/dialog/ms-dialog.vue';
import msLoading from './base/ms-loading.vue';
import Resource  from '@/Resource/Resource';
import keyCode from '@/enum/keyCode';
import axios from 'axios';
import {RepositoryFactory} from '../Repository/RepositoryFactory';
 const EmployeeRepository = RepositoryFactory.get('Employees');

// import TheModal from './TheModal.vue';
import msDropdownPage from './base/ms-dropdownPage.vue';
export default {
    name:"TheMain",
    components:{msButton,TheTable, msInput ,msDropdownPage ,msDialog,msLoading},
    mounted(){
        this.focusInput();
    },
    data(){
        return{
            isSmallSibar: false,
            sibarWidth: "sibar-small",
            isShowModal: false,
            isPaginationActive: false,
            totalPage: null,
            pageSize: "10",
            pageNumber: "1",
            filter: "",
            refresh: false,
            title: "",
            isChecked: false,
            idForm: "add-form",
            selected: [],
            isDialog: false,
            isLoading: false,
            idEmployee: [],
            isDelActive: false,
            pageNumber2:1,
            Resource : Resource,
            keyCode: keyCode,
            closeSelect: false
            
           
            
        };
    },
    methods: {
        focusInput(){
            // this.$refs.search.$refs.ref.focus();
        },
        handleshortcuts(e){
            if(e.keyCode == keyCode.keyCode.insert){
                this.onToggleModal();
            }
        },
        /**
         * Thực hiện xử lý ẩn hiện dropdown deleteBatch
         **  Author: Đinh Công Trứ(26/10/2022)
         */
         onToggleDelBatch() {
            this.isDelActive = !this.isDelActive;
            
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
            this.selected= [];
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
            this.filter = "";
        },
        /**
         * Thực hiện xử lý chuyển dữ liệu pageNumber vào table để chuyển trang
         **  Author: Đinh Công Trứ(26/10/2022)
         */
        onNextPage() {
            if (Math.ceil(this.totalPage / this.pageSize) > this.pageNumber) {
                this.pageNumber++;
                this.pageNumber2++
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
            this.filter = "";
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
       
        /**
         * Thực hiện xử lý xuất Excel
         **  Author: Đinh Công Trứ(25/11/2022)
         */
        exportExcel(){
            try {
                EmployeeRepository.exportExcel()
                .then((res) => {
                        res.config.responseType = "blob";
                        axios(res.config).then((res) => {
                            const url = window.URL.createObjectURL(
                                new Blob([res.data])
                            );
                            const link = document.createElement("a");
                            link.href = url;
                            link.setAttribute("download", "nhanvien.xlsx");
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
@import url(../css/main.css);
</style>