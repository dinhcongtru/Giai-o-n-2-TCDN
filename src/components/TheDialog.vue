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
import {RepositoryFactory} from '../Repository/RepositoryFactory';
import Resource from '@/Resource/Resource';
 const EmployeeRepository = RepositoryFactory.get('Employees');
export default {
    name: "TheDialogInfo",
    props: ["dialogName", "employeeID", "employeeCode", "statusValidate","selected"],
    data() {
        return {
            Resource :Resource,
            deleteStatus: "",
            waringValidateStatus: null,
            buttonId: null,
            buttonText: "Kh??ng",
            employees: null,
            employee: {}
        };
    },
    methods: {
        /**
         * Th???c hi???n chuy??n th??ng b??o ????ng dialog v??o component cha
         **  Author: ??inh C??ng Tr???(1/11/2022)
         */
        onClose() {
            this.$emit("closeDialog");
        },
        /**
         * Th???c hi???n chuy??n th??ng b??o ????ng dialog v??o component cha
         **  Author: ??inh C??ng Tr???(1/11/2022)
         */
        onCloseDialog(){
            this.$parent.$emit("closeModal");
        },
        /**
         * Th???c hi???n l??u khi ng?????i d??ng click v??o button c?? dialog
         **  Author: ??inh C??ng Tr???(1/11/2022)
         */
        handleWarring(){
           
            this.$emit("save");
        },
        
        /**
         * Th???c hi???n x??? l?? api x??a d??? li???u nh??n vi??n
         **  Author: ??inh C??ng Tr???(1/11/2022)
         */
        handleDelete() {
            let _this = this;
            
            // x??a 1 nh??n vi??n theo ID
            EmployeeRepository.deleteEmployeeByID(this.employeeID)
            .then(() => {
                   
                   _this.onClose();
                   _this.$emit("deleteSuccess");
               })
               .catch((error) => {
                   if(error){
                        alert("b??? l???i");
                   }
               });
            
        },

        // x??a nhi???u nh??n vi??n 
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
         * Th???c hi???n th??ng b??o status khi x??a nh??n vi???n ho???c hi???n th??? l???i khi l??u ho???c s???a form
         ** Author: ??inh C??ng Tr???(1/11/2022)
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