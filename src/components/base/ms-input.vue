<template lang="">
       
        <input
            class="form-input"
            ref="input"
            :class="[style, {'form-input-error': isError}]"
            :type="type"           
            :placeholder="placeholder"
            :value="modelValue"
            @input="onInput" 
            @blur="blur"      
            :tabindex="tabindex"
            :id="id"
           

        />

</template>
<script>
export default {
  name: "msInput",
  props: {
    label: String,
    tabindex: String,
    disable: Boolean,
    readOnly: Boolean,
    type: String,
    id: String,
    placeholder: String,
    style: String,  
    required: Boolean,
    title: String,
    blur: Function,
    modelValue: String,
    autoFocus: Boolean,
    error: Boolean,
   
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
    if(this.autoFocus)
    {
      this.$refs.input.focus();
    }
  
    
    
    
    
  },
  
  methods: {
   
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
  
  },
}
</script>
<style lang=""></style>
