<template>
    <!-- Dialog Container dùng chung cho các dialog khác nhau -->
    <div class="m-dialog m-add-dialog" v-show="isShow">
      <div
        ref="mDialog"
        class="m-dialog-box"
        :class="{ 'h-625 dialog-right w-800': isRight, 'h-625 w-900': isCenter&&!isRight, 'dialog-full':isFull }"
      >
        <!-- Header -->
        <slot name="header"/>
        <!-- Header end-->
        <!-- Form -->
        <div class="m-dialog-form" :class="{'m-form-full':isFull}">
          <!-- Detail -->
          <div class="form-container" :class="{'overflow':isRight}">
              <!-- Nội dung của dialog hiện ở đây -->
              <slot name="content" />
              <!-- Hết nội dung của dialog -->
          </div>
          <!-- Detail end-->
  
          <!-- Footer -->
          <slot name="footer"/>
          <!-- Footer end-->
        </div>
        <!-- Form end -->
  
        <!-- Nút Resize -->
        <div v-if="isRight" class="resize flex" @click="resizeForm()">
          <div class="flex align-y-center">
            <div class="m-icon m-icon-16 m-chevron-left" ref="iconResize"></div>
          </div>
        </div>
        <!-- //Nút Resize -->
      </div>
    </div>
</template>
<script>
  export default {
    props: {
      //có phải dạng dialog nửa bên phải không?
      isRight: {
        type: Boolean,
        default: false,
      },
      //có phải dạng dialog ở giữa hay không?
      isCenter:{
        type:Boolean,
        default: true, //mặc định là dialog ở giữa
      },
      //dialog có full màn hình không?
      isFull:{
        type:Boolean,
        default:false,
      }
  
    },
  
    data() {
      return {
        //dialog có full màn hình không?
        isResizeFull: false,
        //dialog có đang hiện không?
        isShow: false,
      };
    },
  
    methods: {
      /**-------------------------------------------------------
       * Hàm thực hiện resize form toàn màn hình, nửa màn hình. 
       * Author: quyetkaito (29/03/2022)
       ---------------------------------------------------------*/
      resizeForm() {
        if (this.isResizeFull) {
          //nếu đang full màn thì co nhỏ lại
          // console.log("classList hiện tại", this.$refs.mDialog.classList);
          this.$refs.mDialog.classList.add("dialog-right");
          this.$refs.mDialog.classList.remove("dialog-resize-full");
          this.$refs.mDialog.classList.add("w-800");
          this.isResizeFull = false;
          //quay 180 độ mũi tên
          this.$refs.iconResize.classList.remove("rotation-180");
        } else {
          //nếu ko full màn thì cho full màn
          // console.log("classList hiện tại", this.$refs.mDialog.classList);
          this.$refs.mDialog.classList.remove("dialog-right");
          this.$refs.mDialog.classList.remove("w-800");
          this.$refs.mDialog.classList.add("dialog-resize-full");
          this.isResizeFull = true;
          //quay 180 độ mũi tên
          this.$refs.iconResize.classList.add("rotation-180");
        }
      },
  
      /**--------------------------
     * Hàm thực hiện show dialog.
     * Author: quyetkaito (29/03/2022)
    ----------------------------*/
      show() {
        this.isShow = true;
      },
  
      /**
       * Hàm thực hiện hide dialog.
       * Author: quyetkaito (29/03/2022)
       */
      hide() {
        this.isShow = false;
      },
  
      /**---------------------------------
       * Hàm bind phím tắt từ ngoài vào.
       * Author: quyetkaito (31/03/2022)
      -----------------------------------*/
      addEventListener(event, handler){
          this.$refs.mDialog.addEventListener(event,handler);
      }
    },
  };
</script>
  
  <style scoped>
  @import url("@/css/components/base-dialog.css");
  </style>