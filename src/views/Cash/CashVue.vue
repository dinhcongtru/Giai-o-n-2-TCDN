<template>
            <div class="content">
                <div class="h-content-main">
                    <div class="h-content">
                        <router-link to="/app/CA/CAProcess" class="toolBar ">Quy trình</router-link>                 
                        <router-link to="/app/CA/CAReceipt/CAReceiptPaymentList" class="toolBar">Thu, chi tiền</router-link>
                        <div class="toolBar">Kiểm kê</div>
                        <div class="toolBar">Dự báo dòng tiền</div>
                        <div class="toolBar">Báo cáo</div>
                    </div>
                </div>
                <router-view />             
             </div> 
</template>
<script>
import Resource from '@/Resource/Resource';
export default {
    data(){
        return{
            Resource : Resource
        }
    }
}
</script>
<style scoped>
@import url(../../css/main.css);
</style>