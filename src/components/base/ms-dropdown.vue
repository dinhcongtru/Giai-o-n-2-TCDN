<template lang="">
    <div :id="idStyle" class="combobox inf-chucvu">
        <input 
            
            :type="type" 
            autocomplete = "off"
            ref="input"
            required 
            class="input-cbx inf-cv"
            :placeholder="label"
            :value="currentDepartment"
            :id="id"
            :tabindex="tabindex"
            @input="updateSearch"
            @blur="validateDropdown()"
            >
            
             <!-- <span
                v-if="error"
                class="error-text"
                title="Đơn vị không được để trống"
                >Đơn vị không được để trống</span> -->
            <div class="dropdown-merge">
                <div class="absolute-dropdown-plus"  :class="[{ dropup: isOpenDropdown }]">
                    <div class="btn-plus"></div>    
                </div>   
                <div class="absolute-dropdown"  :class="[{ dropup: isOpenDropdown }]" @click="ToggleDropdown">
                    <div :class="[{ tranform: isOpenDropdown }]"  class="icon-cbx"></div>    
                </div>
            </div>
           
            <ul class="dropdown-list" v-if="isOpenDropdown">
                <li
                    v-for="(department, index) in filters"
                    :key="index"
                    class="dropdown-list-item"
                    :class="[
                        department.departmentName == currentDepartment && active,
                    ]"
                    id=""
                    @click="
                        onDepartmentValue(
                            department.departmentName,
                            department.departmentID
                        )">
                    {{ department.departmentName }}
                </li>
            </ul>
    <!-- <div
        class="overlay-feature"
        v-if="isOpenDropdown"
        @click="ToggleDropdown"
    ></div> -->
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
    departmentList: Object,
    departmentId: String,
    departmentName: String,
    departmentError: Boolean,
    autoFocus: Boolean,
    idStyle: String,
    refName: String,
},
watch: {
    isOpenDropdown(newvalue){
        const me = this;
        if(newvalue == true){
            document.addEventListener('click' , me.clickOutSide);
        } else{
            document.removeEventListener('click', me.clickOutSide)
        }
    },
    //Nhận dữ liệu từ props và chuyền vào combobox
    departmentName() {
        this.getDepartmentEdit();
    },
    //chuyền thông báo lỗi khi validate
    departmentError() {
        this.error = !this.error;
    },
},

mounted(){
    if(this.autoFocus)
    {
        this.$refs.input.focus();
    }
   
    
}, 

computed:{
    // currentDepartment() {
    //     ret
    // },
    filters(){
        const self = this;
        return self.departmentList.filter((item) => {
  
          return item.departmentName.toLowerCase().includes(self.currentDepartment.toLowerCase())

          });
        
    }
}, 
methods: {
    /**xử lý gán giá trị để active giá trị được chọn
     * Author: Đinh Công Trứ(30/10/2022)
     */
     onDepartmentValue(departmentName, departmentId) {
        this.currentDepartment = departmentName;
        this.currentId = departmentId;
        this.$emit("departmentId", this.currentId);
        this.$emit("departmentName", this.currentDepartment);
        this.error = false;
        this.isOpenDropdown = !this.isOpenDropdown;
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
        if (this.currentDepartment) {
            this.error = false;
        } else {
            this.error = true;
        }
    },
    /**Thực hiện gán giá trị vào combobox
     * Author: Đinh Công Trứ(30/10/2022)
     */
     getDepartmentEdit() {
        (this.currentDepartment = this.departmentName),
            (this.currentId = this.departmentId);
            // console.log(this.department);
    },
    updateSearch(e){
        this.currentDepartment=e.target.value;
    }
    
},
data() {
    return{
        isOpenDropdown: false,
        currentDepartment:"",
        currentId: null,
        active: "active",
        error: false,
};

}
}
</script>