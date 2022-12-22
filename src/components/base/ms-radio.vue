<template>
    <div>
        <label for="" class="input__label b">{{labelName}}</label>
        <div class="base-radio-container">
            <div class="base-radio" v-for="(radioItem,index) in radioData" :key="index">
    
                <input type="radio" class="ridio-gt" :name="radioName"  :checked="(radioItem==modelValue)" :id="radioItem" :value="radioItem" @input="onInput" />
                <label :for="radioItem">{{radioItem}}</label>
            </div>
        </div>
    
    </div>
    </template>
    
    <script>
    export default {
        name: "msRadio",
        props: {
            labelName: String,
            labelTooltip: String,
            radioName: String,
            radioData: Object,
            modelValue:String,
           
        },
        emits: ['update:modelValue'],
        methods: {
            onInput(e) {
             
                this.$emit('update:modelValue', e.target.value);
            },
        }
    }
    </script>
    
    <style scoped>
    .base-radio-container {
        display: flex;
        width: auto;
    }
    
    .base-radio {
        margin-right: 15px;
        padding: 0;
        height: 36px;
        display: flex;
        align-items: center;
    }
    </style>
    