<template lang="">
    <div class="combobox">
        <span class="pagination-count-title">{{Resource.TOOLBAR.totalRecord}}</span>
         
        <input 
             autocomplete = "off"
            :type="type" 
            readonly
            class="input-cbx input-filter"
            :value="currentPage" 
            >
            <div class="absolute-dropdown"  :class="[{ dropup: isPaginationActive }]" @click="onTogglePagination">
                <div :class="[{ tranform: isPaginationActive }]"  class="icon-cbx"></div>    
            </div>

            <ul class="dropdown-list-page" v-if="isPaginationActive">
                <li
                v-for="(page, index) in pageSize"
                    :key="index"
                    class="dropdown-list-item"
                    :class="[page == currentPage && active]"
                    id=""
                    @click="onPageActive(page)">
                    {{ page }}
                </li>
             </ul>
            <!-- <div
                class="overlay-feature"
                v-if="isPaginationActive"
                @click="onTogglePagination()">
            </div> -->
    </div>
</template>
<script>
import Resource from '@/Resource/Resource';
export default {
name: "msDropdownPage",
watch: {
    isPaginationActive(newvalue){
        const me = this;
        if(newvalue == true){
            document.addEventListener('click' , me.clickOutSide);
        } else{
            document.removeEventListener('click', me.clickOutSide)
        }
    },
    // Chuyền pageSize hiện tại lên thằng cha
    currentPage() {
        this.$emit("pageSize", this.currentPage);
    },
},
methods: {
    /**
     * Thực hiện xử lý ẩn hiện dropdown department
     **  Author: Đinh Công Trứ(26/10/2022)
     */
    onTogglePagination() {
        this.isPaginationActive = !this.isPaginationActive;
        
    },
    clickOutSide(event){
        // console.log("jmaddk");
       if(event.target.closest(`.combobox`)) return
        // console.log("sakdjasjd");
       this.isPaginationActive = false;
    },
    /**
     * Thực hiện active giá trị được chọn
     **  Author: Đinh Công Trứ(26/10/2022)
     */
    onPageActive(page) {
        this.currentPage = page;
        this.onTogglePagination();
    },
},
data() {
    return {
        isPaginationActive: false,
        pageSize: ["10 ", "25 ", "50 ", "100 "],
        currentPage: 25,
        active: "active",
        Resource :Resource
    };
},
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
},
};
</script>
<style lang=""></style>