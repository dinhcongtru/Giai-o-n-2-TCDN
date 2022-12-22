<template>
  <div class="container" id="space-page">
    <the-sibar></the-sibar>
    <div class="main" @keydown="handleshortcuts">
      <the-header/>
      <router-view></router-view>
    </div>
    <!-- <receipt-details /> -->
  </div>
  
</template>

<script>
import TheSibar from './components/TheSibar.vue';
// import TheMain from './components/TheMain.vue';
// import CashVue from './views/Cash/CashVue.vue';
import TheHeader from "./components/TheHeader.vue"
// import ReceiptDetails from './views/Receipt/ReceiptDetalis.vue'


export default {
  name: 'App',
  components: {
    TheSibar,TheHeader
  },
}
</script>

<style>
@import url(./css/main.css);
</style>