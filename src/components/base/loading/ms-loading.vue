<template>
    <div class="m-loading" ref="mLoading" v-show="isShowLoading">
      <ms-loader :size="40" />
    </div>
  </template>
  <script>
  import MSLoader from "@/components/base/loading/ms-loader.vue";
  export default {
    components: {
      "ms-loader": MSLoader,
    },
    data() {
      return {
        isShowLoading: false,
      };
    },
    methods: {
      /**------------------------------------------
      *Hàm show loading.
      *Author: quyetkaito (08/04/2022)
      --------------------------------------------*/
      show() {
        this.isShowLoading = true;
      },
  
      /**------------------------------------------
      *Hàm ẩn loading.
      *Author: quyetkaito (08/04/2022)
      --------------------------------------------*/
      hide() {
        this.isShowLoading = false;
      },
    },
  };
  </script>
  
  <style scoped>
  /*Loader - nền toàn trang cho loading*/

/* .loader {
    position: fixed;
    bottom: 0;
    top: 0;
    left: 0;
    right: 0;
    z-index: 9999;
    background-color: #00000051;
} */

.m-loading {
    background-color: rgba(225, 225, 225, 0.5);
    position: fixed;
    height: 100%;
    width: 100%;
    left: 0;
    top: 0;
    display: flex;
    align-items: center;
    justify-content: center;
    z-index: 100001;
    transition: all .3s ease;
}


/*-------------------
    loading W3School
----------------------*/

/* .m-loading-icon {
    position: absolute;
    top: 50%;
    left: 50%;
    transform: (-50%, -50%);
    border: 5px solid #f3f3f3; */
    /* Light grey */
    /* border-top: 5px solid var(--default-color); */
    /* Blue */
    /* border-radius: 50%;
    width: 30px;
    height: 30px;
    animation: spin 2s linear infinite;
} */

@keyframes spin {
    0% {
        transform: rotate(0deg);
    }
    100% {
        transform: rotate(360deg);
    }
}
  </style>