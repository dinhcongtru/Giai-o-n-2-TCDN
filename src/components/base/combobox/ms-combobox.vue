<template lang="">
    <div class="info-donvi" :id="idStyle">
        <div class="name-dv" :class="labelClass" v-show="isHasLabel">
            <div class="font700" v-show="isHasLabel">{{ labelText }}
                <span style="color: red;" v-show="isRequire">*</span>
            </div>
        </div>
        <div class="combobox inf-chucvu">
            <input 
                :readonly="!searchable"
                type="text" 
                autocomplete = "off"
                ref="input"
                :disabled="disabled"
                class="input-cbx inf-cv"
                :class="styleInput"
                :placeholder="placeholder"
                :value="currentValue"
                :id="id"
                :tabindex="tabInput"
                @blur="validateDropdown()"
                
                
                >
                <div class="dropdown-merge">
                    <div class="absolute-dropdown-plus"  :class="[{ dropup: isOpenDropdown }]" v-show="isPlus">
                        <div class="btn-plus" v-show="isPlus"></div>    
                    </div>   
                    <div class="absolute-dropdown"  :class="[{ dropup: isOpenDropdown }]" @click="ToggleDropdown">
                        <div :class="[{ tranform: isOpenDropdown }]"  class="icon-cbx"></div>    
                    </div>
                </div>
            
                <div 
                     class="dropdown-list" v-show="isOpenDropdown"
                     ref="dropDownContent"
                     :class="[styleDrop,{ 'append-body': appendToBody }]"
                     :style="[showHeader ? 'max-height:200px' : 'max-height:130px']"
                      @scroll="lazyLoad()"
                >
                    <ms-grid
                        ref="mGrid"
                        :isHasHeader="showHeader"
                        :listHeader="listHeader"
                        :listData="optionLocal"
                        :isHasFunctionColumn="false"
                        :isCheckable="false"
                        @eClick="setOption"
                    />
                </div>
        </div>
    </div>
</template>
<script>
import msGrid from '../grid/ms-grid.vue';
export default {
name: "msCombobox",
components:{msGrid},
emits: [
    "onSelected",
    "update:modelValue",
    "eQuickAdd",
    "eLoadNext",
    "eSearching",
  ],
props: {
      //modelValue để ăn v-model
      modelValue: {
      type: [String, Number],
      default: "",
    },
    //dropdown có label hay không?
    isHasLabel: {
      type: Boolean,
      default: true, //mặc định có label
    },
    //label của dropdown là gì
    labelText: {
      type: String,
      default: "label-text",
    },
    //có dấu * thể hiện trường bắt buộc không?
    isRequire: {
      type: Boolean,
      default: false,
    },
    //place holder của combobox là gì?
    placeholder: {
      type: String,
      default: "",
    },
    //input này có được nhập vào không?
    searchable: {
      type: Boolean,
      default: true, //mặc định cho nhập vào
    },
    //Có show header của bảng dropdown bên dưới không?
    showHeader: {
      type: Boolean,
      default: true,
    },
    //giá trị muốn hiển thị lên ô input khi chọn vào options.
    label: {
      type: String,
      default: "label",
    },
    //xác định cột muốn là value của combobox.
    value: {
      type: String,
      default: "value",
    },
    //prop chứa các tiêu đề của bảng trong combobox.
    listHeader: {
      type: Array,
      default: () => [
        // {
        //   text: "Mã nhân viên",
        //   fieldName: "value", //fieldName = value sẽ là giá trị của option
        // },
        {
          text: "",
          fieldName: "label", // fieldName = "label" sẽ là giá trị hiển thị sau khi chọn.
        },
      ],
      required: true, //bắt buộc có header để map mới cột trong table
    },
    autoFocus:{
      type:Boolean,
      default:false
    },
    //list option
    options: {
      type: Array,
      default: () => [
        {
          value: "code 1",
          label: "name 1",
          
        },
        {
          value: "code 2",
          label: "name 2",
        },
        {
          value: "code 3",
          label: "name 3",
        },
      ],
    },
    //dropdown có append thẳng vào body hay không?
    appendToBody: {
      type: Boolean,
      default: false,
    },
    //có đóng content sau khi chọn không?
    closeOnSelect: {
      type: Boolean,
      default: true,
    },
  
    //content có lazy loading hay khong?
    lazyLoading: {
      type: Boolean,
      default: false,
    },
    //input có bị disabled hay không?
    disabled: {
      type: Boolean,
      default: false,
    },
    //tabindex input
    tabInput: {
      type:String,
      default:""
    },
    
    // id combobox
    id: {
      type:String,
      default:""
    },
    //id của thẻ cha
    idStyle: {
      type:String,
      default:"idStyle"
    },
    //tên ref
    refName: {
      type:String,
      default:""
    },
    isPlus:{
      type:Boolean,
      default:true
    },
    labelClass:{
      type:String,
      default:""
    },
    styleInput:{
      type:String,
      default:""
    },
    styleDrop:{
      type:String,
      default:""
    }

},
watch: {
  options(){
    this.optionLocal = this.options
  },
    isOpenDropdown(newvalue){
        const me = this;
        if(newvalue == true){
            document.addEventListener('click' , me.clickOutSide);
        } else{
            document.removeEventListener('click', me.clickOutSide)
        }
    },
     // /**------------------------------------------
    // *Mỗi khi modelValue có thay đổi thì cập nhật lại giá trị.
    // *Author:   Đinh Công Trứ(30/10/2022)
    // --------------------------------------------*/
    modelValue: {
      // immediate: true,
      async handler() {
        if (this.modelValue) {
          try {
      
            //tìm kiếm trong danh sách option
            this.options.forEach((element, index) => {
              if (element[this.value] == this.modelValue) {
                if(this.isPlus){
                  if (this.$refs.mGrid) {
                    //thêm css được chọn
                    this.$refs.mGrid.addSelectedRow(index);
                    //bind giá trị hiển thị lên
                    this.currentValue = element[this.label];
                  }
                }else{
                    if (this.$refs.mGrid) {
                      //thêm css được chọn
                      this.$refs.mGrid.addSelectedRow(index);
                      //bind giá trị hiển thị lên
                      this.currentValue = element[this.value];
                    }
                }
              }
              }
            );
          } catch (error) {
            //log ra lỗi
            console.log(error);
          }
        } else {
          this.currentValue = "";
        }
      },
    },
},

 mounted(){
   if(this.autoFocus){
    this.$refs.input.focus();
   }
   //khi khởi tạo mà model value có giá trị
   if (this.modelValue) {
      try {
       
        //tìm kiếm trong danh sách option
        this.options.forEach((element, index) => {
          if (element[this.value] == this.modelValue) {
             if(this.isPlus){
              //thêm css được chọn
              this.$refs.mGrid.addSelectedRow(index);
              //bind giá trị hiển thị lên
              this.currentValue = element[this.label];
             }else{
               //thêm css được chọn
               this.$refs.mGrid.addSelectedRow(index);
              //bind giá trị hiển thị lên
              this.currentValue = element[this.value];
             }
             }
          }
        );
      } catch (error) {
        //log ra lỗi
        console.log(error);
      }
    }
}, 

computed:{
    // // currentDepartment() {
    // //     ret
    // // },
    // filters(){
    //     const self = this;
    //     return self.options.filter((item) => {
  
    //       return item.label.toLowerCase().includes(self.currentValue.toLowerCase())

    //       });
        
    // }
}, 
methods: {
  /**------------------------------------------
    *lazyloading cho combobox
    *Author: Đinh Công Trứ(30/10/2022)
    --------------------------------------------*/
    lazyLoad() {
      //nếu lazyLoading bằng true thì mới loading.
      if (this.lazyLoading) {
        //độ cao dropdown
        let dropdownHeight = this.$refs.dropDownContent.clientHeight;
        //phần đã bị tràn khỏi top.
        let scrollTop = this.$refs.dropDownContent.scrollTop;
        //toàn bộ độ cao có thể scroll
        let scrollHeight = this.$refs.dropDownContent.scrollHeight;

        //vị trí hiện tại của con lăn
        var total = dropdownHeight + Math.floor(scrollTop);
        //nếu bằng scrollHeight thì là đang ở bottom
        if (total == scrollHeight) {
          // console.log("đang ở bottom");
          total = 0;
          //emit tới cha load tiếp thông tin, truyền vào dữ liệu filter nếu có để lazy load tiếp thông tin đó.
          this.$emit("eLoadNext", this.currentValue);
        }
      }
    },
    /**------------------------------------------
    *Hàm lấy object được chọn từ dropdown dạng bảng.
    *@param {Object} object - 1 hàng của dropdown dạng bảng(nhận được từ ms-grid)
    *Author: Đinh Công Trứ(30/10/2022)dropdownValue
    --------------------------------------------*/
    setOption({ object, index }) {
      if(this.isPlus){
        //gán giá trị mong muốn vào value, label
        this.currentValue = object[this.value]; //giá trị vào ô input (VD:DepartmentName)
        this.dropdownValue = object[this.value]; //giá trị của dropdown (VD:id của đối tượng: DepartmentID)
        //emit tới cha value của option
        this.$emit("update:modelValue", this.dropdownValue);
        if (this.dropdownValue) {
          //emit tới grid add css chọn
          this.$refs.mGrid.addSelectedRow(index);
        }

        if (this.closeOnSelect) {
          //ẩn dropdown sau khi select
          this.isOpenDropdown = false;
        }

        //focus vào input sau khi select
        this.$refs.input.focus();

        //bỏ css đỏ nếu có
        this.error = false;

        //emit tới cha sự kiện sau khi selected
        this.$emit("onSelected", object);
      }if(!this.isPlus){
        // console.log(`hàng ${index} nhận được không plus :`, object);
      //gán giá trị mong muốn vào value, label
      this.currentValue = object[this.label]; //giá trị vào ô input (VD:DepartmentName)
      this.dropdownValue = object[this.value]; //giá trị của dropdown (VD:id của đối tượng: DepartmentID)
      // console.log(`hàng nhận được không plus :`,  this.dropdownValue);
      this.$emit("value", this.dropdownValue);

      //emit tới cha value của option
      this.$emit("update:modelValue", this.dropdownValue);
      if (this.dropdownValue) {
        //emit tới grid add css chọn
        this.$refs.mGrid.addSelectedRow(index);
      }

      if (this.closeOnSelect) {
        //ẩn dropdown sau khi select
        this.isOpenDropdown = false;
      }

      //focus vào input sau khi select
      this.$refs.input.focus();

      //bỏ css đỏ nếu có
      this.error = false;

      //emit tới cha sự kiện sau khi selected
      this.$emit("onSelected", object);
      }
    },
 
    clickOutSide(event){
        // console.log("jmaddk");
       if(event.target.closest(`#${this.idStyle}`)) return
        // console.log("sakdjasjd");
       this.isOpenDropdown = false;
    },
    /**Thực hiện ẩn TheDropdown
     * Author: Đinh Công Trứ(30/10/2022)
     */
     ToggleDropdown() {
        this.isOpenDropdown = !this.isOpenDropdown;
    },
    /** validate TheDropdown đơn vị
     * Author: Đinh Công Trứ(30/10/2022)
     */
     validateDropdown() {
        if (this.currentValue) {
            this.error = false;
        } else {
            this.error = true;
        }
    },
    
},
data() {
    return{
      // dropdown đã được show hay chưa?
        isOpenDropdown: false,
        //giá trị để update cho v-model
        dropdownValue: "",
        //giá trị được chọn hiện lên input
        currentValue:"",
        //lưu lại option để xử lý tìm kiếm
        optionLocal: this.options, 
        //css khi option được adtive
        active: "active",
        //dropdown có lỗi hay không?
        error: false,
};

}
}
</script>
<style scoped>
  .append-body{
    
    z-index: 9696;
  }
</style>