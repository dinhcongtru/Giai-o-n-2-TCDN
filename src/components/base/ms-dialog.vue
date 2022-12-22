<template>
    <div class="row-dialog"  @click.self="onClose">
        <div class="pogup warring">
            <div class="content-dialog">
                <span v-if="dialogName == 'delete' || dialogName == 'deleteBatch' || dialogName == 'checkCode'" class="icon-warrring" id="icon-warrring"></span>
                <span v-if="dialogName == 'validate'" class="dialog-body-validate"></span>
                <span v-if="dialogName == 'warring'" class="mi-48"></span>
                <div class="title-warring" style="font-size: 14px;">{{ renderStatus() }}</div>
            </div>
            <div class="foot-dialog" :class="dialogName == 'validate' && 'alig-centre'">
                <button class="btn-add btn-warring delete-btn" id="warring"  @click="onClose" v-if="dialogName == 'warring'" >{{Resource.DIALOG.HUY}}</button>
                <button class="btn-add btn-warring delete-btn" id="margin40" v-if="dialogName == 'delete'|| dialogName == 'validate'|| dialogName == 'deleteBatch'" @click="onClose" :class="dialogName == 'validate' && 'pri-btn'"> {{ (dialogName == "delete"  && buttonText) || Resource.DIALOG.DONG }}</button>
                <div class="dialog-footer-btn">
                    <button class="btn-add btn-warring delete-btn" v-if="dialogName == 'warring'" id="warring-item"  @click="onCloseDialog" :class="dialogName == 'validate' && 'pri-btn'"> {{ Resource.DIALOG.KHONG }}</button>
                    <button
                        class="btn-add btn-warring"
                        id="{{buttonId}}"
                        @click="handleDelete"
                        v-if="dialogName == 'delete'"
                    >
                        {{Resource.DIALOG.CO}}
                    </button>
                    <button
                        class="btn-add btn-warring"
                        id="{{buttonId}}"
                        @click="handleDeleteBatch"
                        v-if="dialogName == 'deleteBatch'"
                    >
                    {{Resource.DIALOG.CO}}
                    </button>
                    <button
                        class="btn-add btn-warring btn-waring"
                        id="{{buttonId}}"
                        @click="handleWarring"
                        v-if="dialogName == 'warring'"
                    >
                    {{Resource.DIALOG.CO}}
                    </button>
                </div>
            </div>
        </div>
    </div>
</template>
<script>
import {RepositoryFactory} from '@/Repository/RepositoryFactory';
import Resource from '@/Resource/Resource';
 const EmployeeRepository = RepositoryFactory.get('Employees');
export default {
    name: "msDialog",
    props: ["dialogName", "employeeID", "employeeCode", "statusValidate","selected"],
    data() {
        return {
            Resource :Resource,
            deleteStatus: "",
            waringValidateStatus: null,
            buttonId: null,
            buttonText: "Không",
            employees: null,
            employee: {}
        };
    },
    methods: {
        /**
         * Thực hiện chuyên thông báo đóng dialog vào component cha
         **  Author: Đinh Công Trứ(1/11/2022)
         */
        onClose() {
            this.$emit("closeDialog");
        },
        /**
         * Thực hiện chuyên thông báo đóng dialog vào component cha
         **  Author: Đinh Công Trứ(1/11/2022)
         */
        onCloseDialog(){
            this.$parent.$emit("closeModal");
        },
        /**
         * Thực hiện lưu khi người dùng click vào button có dialog
         **  Author: Đinh Công Trứ(1/11/2022)
         */
        handleWarring(){
           
            this.$emit("save");
        },
        
        /**
         * Thực hiện xử lý api xóa dữ liệu nhân viên
         **  Author: Đinh Công Trứ(1/11/2022)
         */
        handleDelete() {
            let _this = this;
            
            // xóa 1 nhân viên theo ID
            EmployeeRepository.deleteEmployeeByID(this.employeeID)
            .then(() => {
                   
                   _this.onClose();
                   _this.$emit("deleteSuccess");
               })
               .catch((error) => {
                   if(error){
                        alert("bị lỗi");
                   }
               });
            
        },

        // xóa nhiều nhân viên 
        handleDeleteBatch(){
            let _this = this;
            EmployeeRepository.deleteBatch({EmployeeIDs:this.selected})
            .then(function()  { 
                    _this.onClose();
                    _this.$emit("deleteSuccess");
                    _this.$emit("closeSelect");
                })
                .catch((error) => {
                    console.log("error",error)
                });
             
        },
        /**
         * Thực hiện thông báo status khi xóa nhân viện hoặc hiển thị lỗi khi lưu hoặc sửa form
         ** Author: Đinh Công Trứ(1/11/2022)
         */
        renderStatus() {
            if (this.dialogName == 'deleteBatch'){
                return Resource.TEXTDIALOG.deleteBatch;
            }
            if (this.dialogName == "delete") {
                return Resource.TEXTDIALOG.delete +`<${this.employeeCode}>`+ Resource.TEXTDIALOG.textno;
            }
            if (this.dialogName == "validate") {
                return this.statusValidate;
            }
            
            if (this.dialogName == "warring") {
                return Resource.TEXTDIALOG.warring;
            }
        },
    },
};
</script>
<style scoped>
  #margin40{
    margin-right: 180px;
  }
  #warring-item{
    margin-right:8px ;
  }
  #warring{
    width: 60px;
  }
  .btn-waring{
    width: 55px;
    display: flex;
    justify-content: center;
    align-items: center;
  }
</style>