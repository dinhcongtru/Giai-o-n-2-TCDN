<template lang="">
    <div class="colum">
        <div class="m-label" :class="classLaybel" v-show="isHasLabel">{{ labelText }}
            <span style="color: red;" v-show="isRequire">*</span>
        </div>
        <!-- Nếu là input bình thường -->
        <div v-if="!isTextArea" :class="styleparent">
        <input
            class="form-input"
            ref="input"
            :name="name"
            :maxlength="maxlength"
            :disabled="disabled"
            :style="{ height: height + 'px' }"
            :class="[styleClass, {'form-input-error': isError},{'text-align-right': isNumberInput}]"
            :type="type"           
            :placeholder="placeholder"
            :value="modelValue"
            @input="sendChange" 
            @blur="blur" 
            @change="$emit('onChange', modelValue)" 
            :tabindex="tabInput"
            autocomplete="off"

        />
  </div>
     <!-- Nếu là textarea -->
     <div v-if="isTextArea">
      <textarea
            ref="input"
            class="m-input m-textarea"
            :placeholder="placeholder"
            
            :style="{ height: height + 'px!important' }"
            @input="sendChange"
            :value="modelValue"
            @change="$emit('onChange', modelValue)"
          />
      </div>
    </div>
</template>
<script>
import * as base from "@/js/base";
export default {
name: "MSinput",

props: {
    //input có label hay không?
    isHasLabel: {
      type: Boolean,
      default: true,
    },
    //label của input là gì
    labelText: {
      type: String,
      default: "label-text",
    },
     //hiện title khi hover cho label viết tắt
     labelTitle: {
      type: String,
      default: null,
    },
    //loại input
    type: {
        type:String,
        default:"text"
    },
    //placeholder của input
    placeholder: {
      type: String,
      default: "",
    },
    //input này có bắt buộc không?
    isRequire: {
      type: Boolean,
      default: false,
    },
     //chiều rộng là bao nhiêu? mặc định 100% chiều dài phần tử cha
     width: {
      type: Number,
      default: 200,
    },
    //chiều cao của input là bao nhiêu?
    height: {
      type: Number,
      default: 28,
    },
    //số ký tự có thể nhập là bao nhiêu?
    maxlength: {
      type: Number,
      default: 255,
    },
    //giá trị khởi tạo cho input
    modelValue: {
      type: [String, Number],
      default: "",
    },
    styleClass:{
        type: String,
        default:""
    },
     //thêm class lỗi nếu lỗi
     error: {
      type: Boolean,
      default: false,
    },
     //có phải input dạng số hay không?
     isNumberInput: {
      type: Boolean,
      default: false,
    },
    //có phải input dạng text area không?
    isTextArea: {
      type: Boolean,
      default: false,
    },
    //name của input
    name: {
      type: String,
      default: "",
    },
    //input có bị disabled ko?
    disabled: {
      type: Boolean,
      default: false,
    },
    //class label
    classLaybel:{
        type:String,
        default:""
    },
    tabInput:{
        type:String,
        default:""
    },
    styleparent:{
      type:String,
      default:""
    },
    autoFocus:{
      type: Boolean,
      default:false,
    },
    blur: Function


},
data() {
  return {
      isError: false,
   
  };
},
watch: {
error() {
      this.isError = !this.isError;
  },
},
emits: ["update:modelValue"],
mounted() {
  if(this.autoFocus){
    this.$refs.input.focus();
  }
},

methods: {
/**----------------------------------
     * Hàm gửi dữ liệu tới component cha
     * và xử lý dữ liệu.
     * Author: Đinh công Trứ (05/12/2022)
    -------------------------------------*/
    sendChange(event) {
      try {
        //khi nhập thì bỏ error border
        this.removeError();
        //nếu là dạng số thì format
        if (this.isNumberInput) {
          //gọi hàm format định dạng số ngay khi nhập liệu từ base
          this.onNumberInput(event);
        }
        this.$emit("onChange", true);
        this.$emit("update:modelValue", event.target.value);
      } catch (error) {
        //log ra lỗi
        console.log(error);
      }
    },
onInput(e) {
  this.$emit("update:modelValue", e.target.value);
},
//   validate(){
//     if (this.modelValue) {
//               this.isError = false;
//           } else {
//               this.isError = true;
//           }
// },

    /**-----------------------------------------------------------
 * Hàm thực hiện format định dạng số ngay trong khi nhập liệu.
 * @param {Event} event 
 * Author: Đinh công Trứ (05/12/2022).
 -----------------------------------*/
    onNumberInput(event) {
      try {
        //lấy ra giá trị của ô input hiện tại
        let value = event.target.value;
        // console.log("ô input hiện tại: ", value);
        if (event.data == ",") {
          //nếu người dùng nhập dấu ',' và không có dấu ',' nào đã tồn tại => đang nhập số thập phân
          if ((value.match(/,/g) || []).length <= 1) {
            // console.log("chưa phải số thập phân");
            return; //cho nhập liệu tiếp
          }
        }
        //format số
        event.target.value = base.formatCurrency(value);
      } catch (error) {
        console.log(error);
      }
    },
    /**--------------------------------
     * Hàm dùng để focus vào input.
     * Author:Đinh công Trứ (05/12/2022).
    ----------------------------------*/
    focus: function () {
      this.$refs.input.focus();
    },
    /**------------------------------------
     * Hàm dùng để thêm css lỗi cho input.
     * Author: Đinh công Trứ (05/12/2022)
    --------------------------------------*/
    addError: function () {
      this.$refs.input.classList.add("error");
      this.$refs.input.setAttribute(
        "title",
        `${this.labelText} không được bỏ trống`
      );
    },
     /**--------------------------
     * Hàm thêm border đỏ cho input
     * Author: Đinh công Trứ (05/12/2022)
     -------------------------*/
     addRedBorder: function () {
      this.$refs.input.classList.add("error");
    },
    /**------------------------------------
     * Hàm dùng để bỏ css lỗi cho input.
     * Author: Đinh công Trứ (05/12/2022)
    --------------------------------------*/
    removeError: function () {
      this.$refs.input.classList.remove("error");
      this.$refs.input.setAttribute("title", "");
    },
 
},
}
</script>
<style>
.text-align-right {
    text-align: right;
}

.m-textarea {
    padding: 8px 12px 8px 10px!important;
    height: 55px;
    font-family: Notosans-Regular;
    box-sizing: border-box;
    line-height: 20px;
    overflow: hidden;
    outline: none;
    border: 1px solid #babec5;
    border-radius: 3px;
    font-size: 13px;
}
textarea.m-input.m-textarea:focus {
    border: 1px solid #2ca01c !important;
    outline: none;
}

textarea {
    resize: none;
}

input:disabled {
    color: #111111;
}
.m-label{
    font-weight: 700;
    margin-bottom: 4px;
}
@import url(@/css/main.css);
</style>
