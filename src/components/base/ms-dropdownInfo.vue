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

            <ul class="dropdown-listInfo" v-if="isOpenDropdown">
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
name: "msDropdown",
props: {
    label: String,
    tabindex: String,
    disable: Boolean,
    type: String,
    id: String,
    placeholder: String,
    class: String,
    itemData: Array,
    departmentError: Boolean,
    autoFocus: Boolean,
    idStyle: String,
    refName: String,
    modelValue: String,
    isReadonly:Boolean,
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
        this.$refs.input.focus()
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