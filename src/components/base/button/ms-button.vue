<template lang="">
    <button class="form-btn" ref="button" :class="style" :id="id" :isDisabled="disable" :tabindex="tabindex" :type="type">{{ title }}</button>
</template>
<script>
export default {
    name: "msButton",
    props: {
        //class button
        style: {
            type: String,
            default:"form-btn"
        },
        //có disable hay không
        disable: {
            type: Boolean,
            default: false,
            },
        // Nội dung button
        title: {
            type: String,
            default: "btn-text",
        },
        //id button
        id:{ 
            type:String,
            default:"",
        },
        //tabindex button
        tabindex: {
            type:String,
            default: ""
        },
        //loại button
        type:{ 
            type:String,
            default:"text",
        }

    },
    methods:{
            focus:function(){
        this.$refs.button.focus();
        }
    }
};
</script>
<style lang=""></style>