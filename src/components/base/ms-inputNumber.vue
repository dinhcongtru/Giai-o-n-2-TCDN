<template lang="">
    <input
        class="form-input"
        ref="ref"
        :type="type"
        :placeholder="placeholder"
        :value="modelValue"
        @input="onInput"
        :tabindex="tabindex"
        :id="id"
        @keypress = "validateNumber"
    />

</template>
<script>
export default {
name: "msNumber",
props: {
    label: String,
    labelTooltip: String,
    tabindex: String,
    disable: Boolean,
    readOnly: Boolean,
    type: String,
    id: String,
    placeholder: String,
    class: String,
    required: Boolean,
    title: String,
    modelValue:String

},
// watch: {
//     value() {
//         this.$emit("value", this.value);
//     },
// },
emits:['update:modelValue'],
methods:{
    onInput(e){
        this.$emit('update:modelValue',e.target.value);
    },
    validateNumber(event){
        let keyCode = event.keyCode;
        if(keyCode < 48 || keyCode > 57){
            event.preventDefault();
        } 
    }
    
},

};
</script>
<style lang=""></style>
