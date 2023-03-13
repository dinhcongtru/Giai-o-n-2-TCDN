<template lang="">
    <div :id="idStyle" class="combobox inf-chucvu">
        <input 
            :type="type" 
            autocomplete = "off"
            ref="input"
            :disabled="disable"
            :readonly="isReadonly"
            required 
            class="input-cbx inf-cv"
            :placeholder="label"
            :value="modelValue"
            :id="id"
            :tabindex="tabindex"
            @input="updateSearch"
            >

                <div class="absolute-dropdownInfo"  :class="[{ dropup: isOpenDropdown }]" @click="ToggleDropdown">
                    <div :class="[{ tranform: isOpenDropdown }]"  class="icon-cbx"></div>    
                </div>

            <ul class="dropdown-list" v-if="isOpenDropdown">
                <li
                    v-for="(item, index) in itemData"
                    :key="index"
                    class="dropdown-list-item"
                    :class="[
                        item.name == modelValue && active,
                    ]"
                    id=""
                    @click="
                        onDepartmentValue(
                            item.name
                            
                        )">
                    {{ item.name }}
                </li>
            </ul>
    </div>
</template>
<script>
export default {
name: "msCheckCombobox",
props: {
    // placeholder
    label:{ 
        type: String,
        default: "",
    },
    //tabindex
    tabindex: {
        type:String,
        default:""
    },
    // có disable hay không
    disable:{
        type: Boolean,
        default:false
    },
    //kiểu combo
    type: {
        type:String,
        default:"text"
    },
    //id của combo
    id:{ 
        type:String,
        default:""
    },
    //class
    class:{ 
        type:String,
        default:""
    },
    //đối tượng truyền vào
    itemData:{ 
        type:Array,
        default: () => [
        { name: "option 1", value: 1 },
        { name: "option 2", value: 2 },
        { name: "option 3", value: 3 },
      ],
    },
    //có lỗi hay không
    departmentError:{ 
        type:Boolean,
        default:false
    },
    //có focus hay không
    autoFocus:{ 
        type:Boolean,
        default:false
    },
    // id của cha
    idStyle:{ 
        type:String,
        default:""
    },
    //tên ref
    refName:{ 
        type:String,
        default:""
    },
    // modelValue để ăn v-model
    modelValue:{ 
        type:String,
        default:""
    },
    //chỉ đọc hay không
    isReadonly:{
        type:Boolean,
        default:false
    },
},
emits:["update:modelValue"],
watch: {
    /**xử lý sự kiện clickOutSide
     * Author: Đinh Công Trứ(30/10/2022)
     */
    isOpenDropdown(newvalue){
        const me = this;
        if(newvalue == true){
            document.addEventListener('click' , me.clickOutSide);
        } else{
            document.removeEventListener('click', me.clickOutSide)
        }
    },

    //chuyền thông báo lỗi khi validate
    departmentError() {
        this.error = !this.error;
    },
},

mounted(){
    /**xử lý sự kiện autoFocus
     * Author: Đinh Công Trứ(30/10/2022)
     */
    if(this.autoFocus)
    {
        this.$refs.input.focus();
    }
   
    
}, 

methods: {
    /**xử lý gán giá trị để active giá trị được chọn
     * Author: Đinh Công Trứ(30/10/2022)
     */
     onDepartmentValue(itemName) {
        this.$emit("update:modelValue",itemName );
        this.error = false;
        this.isOpenDropdown = !this.isOpenDropdown;
    },
    /**xử lý clickOutSide
     * Author: Đinh Công Trứ(30/10/2022)
     */
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
   /**xử lý hàm tìm kiếm
     * Author: Đinh Công Trứ(30/10/2022)
     */
    updateSearch(e){
        this.currentDepartment=e.target.value;
    }
    
},
data() {
    return{
        isOpenDropdown: false,
        active: "active",
        error: false,
};

}
}
</script>