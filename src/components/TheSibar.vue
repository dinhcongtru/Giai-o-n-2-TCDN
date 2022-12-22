<template>
    <div class="sibar" :class="[isSmallSibar && sibarWidth]">
            <div class="sibar-t" v-if="isSmallSibar == false">
                <span class="icon-sibar"></span>
                <img src="../assets/img/Logo_Module_TiengViet_White.66947422.svg" alt="" style="width:96px;height=32px">
            </div>
            <div class="sibar-b">
                <div class="menu-item--active">
                    <span class="icon-dashboard"></span>
                    <div class="menu-item-title" v-if="!isSmallSibar">{{Resource.SIBAR.dashboard}}</div>
                </div>
                <router-link to="/app/CA" class="menu-item--active" @click="pushRouter">
                    <span class="icon-cash"></span>
                    <div class="menu-item-title" v-if="!isSmallSibar">{{Resource.SIBAR.cash}}</div>
                </router-link>
                <router-link to="/app/DI/ReceiptDetails" class="menu-item--active">
                    <span class="icon-send"></span>
                    <div class="menu-item-title" v-if="!isSmallSibar">{{Resource.SIBAR.send}}</div>
                </router-link>
                <div class="menu-item--active">
                    <span class="icon-cart"></span>
                    <div class="menu-item-title" v-if="!isSmallSibar">{{Resource.SIBAR.cart}}</div>
                </div>
                <div class="menu-item--active">
                    <span class="icon-sell"></span>
                    <div class="menu-item-title" v-if="!isSmallSibar">{{Resource.SIBAR.sell}}</div>
                </div>
                <div class="menu-item--active">
                    <span class="icon-manager"></span>
                    <div class="menu-item-title" v-if="!isSmallSibar">{{Resource.SIBAR.manager}}</div>
                </div>
                <div class="menu-item--active">
                    <span class="icon-house"></span>
                    <div class="menu-item-title" v-if="!isSmallSibar">{{Resource.SIBAR.house}}</div>
                </div>
                <div class="menu-item--active">
                    <span class="icon-tool"></span>
                    <div class="menu-item-title" v-if="!isSmallSibar">{{Resource.SIBAR.tool}}</div>
                </div>
                <div class="menu-item--active">
                    <span class="icon-defau"></span>
                    <div class="menu-item-title" v-if="!isSmallSibar">{{Resource.SIBAR.defaults}}</div>
                </div>
                <div class="menu-item--active">
                    <span class="icon-tax"></span>
                    <div class="menu-item-title" v-if="!isSmallSibar">{{Resource.SIBAR.tax}}</div>
                </div>
                <div class="menu-item--active">
                    <span class="icon-price"></span>
                    <div class="menu-item-title" v-if="!isSmallSibar">{{Resource.SIBAR.price}}</div>
                </div>
                <div class="menu-item--active">
                    <span class="icon-total"></span>
                    <div class="menu-item-title" v-if="!isSmallSibar">{{Resource.SIBAR.total}}</div>
                </div>
                <div class="menu-item--active">
                    <span class="icon-budget"></span>
                    <div class="menu-item-title" v-if="!isSmallSibar">{{Resource.SIBAR.budget}}</div>
                </div>
                <div class="menu-item--active">
                    <span class="icon-report"></span>
                    <div class="menu-item-title" v-if="!isSmallSibar">{{Resource.SIBAR.report}}</div>
                </div>
                <div class="menu-item--active">
                    <span class="icon-finance"></span>
                    <div class="menu-item-title" v-if="!isSmallSibar">{{Resource.SIBAR.finance}}</div>
                </div>
            </div>
        </div>
</template>
<script>
import Resource from '@/Resource/Resource';
export default {
    name:"TheSibar",
    data(){
        return{
            isSmallSibar: false,
            sibarWidth: "sibar-small",
            Resource: Resource
        };
    },
    methods:{
        pushRouter(){
            this.$router.push("/");
            this.$router.push("/app/CA/CAProcess");
        }
    }
};
</script>
<style>
@import url(../css/main.css);
</style>