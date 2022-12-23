<template> 
        <div class="content-main">
            <div class="m-content">
            <div class="m-content-left">
                <div class="row-top">
                    <div class="row-top-title">NGHIỆP VỤ TIỀN MẶT</div>
                    <div class="row-top-main">
                        <div class="img-top-main">
                            <div class="div-face-button receive">Thu tiền</div>
                            <div class="div-face-button payment">Chi tiền</div>
                            <div class="div-face-button audit">Kiểm kê quỹ</div>
                        </div>
                        
                    </div>
                </div>
                <div class="row-botton">
                    <div class="row-item flex-grow"  @click="onToggle">
                        <span class="icon-customer"></span>
                        <div class="row-text">Khách hàng</div>
                    </div>
                    <router-link to="/app/DI/DIVendor" class="row-item flex-grow text-decoration">
                        <span class="icon-provide"></span>
                        <div class="row-text">Nhà cung cấp</div>
                    </router-link>
                    <div class="row-item flex-grow text-decoration"  @click="onToggle">
                        <span class="icon-employee"></span>
                        <div class="row-text">Nhân viên</div>
                    </div>
                    <div class="row-item flex-grow  border0"  @click="onToggle">
                        <span class="icon-setting"></span>
                        <div class="row-text">Tùy chọn</div>
                    </div>
                </div>
            </div>
            <div class="m-content-right">
                <div class="row-top-title">BÁO CÁO</div>
                <div class="content-right-main">
                    <ul>
                        <li class="right-item">Bảng kê số dư theo ngày</li>
                        <li class="right-item">Dòng tiền</li>
                        <li class="right-item">S03a1-DN: Sổ nhật ký thu tiền</li>
                        <li class="right-item">Sổ kế toán chi tiết quỹ tiền mặt</li>
                        <li class="right-item botton-none">S03a2-DN: Sổ nhật ký chi tiền</li>
                    </ul>
                </div>
                <div class="footer">Tất cả báo cáo</div>
            </div>
        </div>
        </div>
  <ms-dialog-vue
        v-if="isError"
        dialogName="validate"
        :statusValidate="errorMgs"
        @closeDialog="onCloseDialog"
    />
</template>
<script>
import Resource from '@/Resource/Resource';
import msDialogVue from '@/components/base/ms-dialog.vue';
export default {
    name:"CashProcess",
    components:{msDialogVue},
    data(){
    return{
        Resource : Resource,
        isError: false,
        errorMgs: false,
    }
    },
    methods:{
        onCloseDialog() {
            this.isError = !this.isError;
            
        },
        onToggle(){
            this.isError = !this.isError;
            this.errorMgs = Resource.ERROR.toolDevelop;
            return false;
        }
    }
}
</script>
<style scoped>
@import url(../../css/main.css);
</style>