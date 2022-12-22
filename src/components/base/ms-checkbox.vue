<template lang="">
    <div class="ms__checkbox">
      <input
        type="button"
        id="name"
        class="checkbox__btn"
        :class="{ 'checkbox__btn--active': active }"
        @click="toggleCheckbox"
        ref="checkbox"
        :disabled="readOnly"
      />
      <label :for="name" class="checkbox__label">{{ label }}</label>
    </div>
  </template>
  <script>
  export default {
    props: {
      name: {
        type: String,
        default: "checkbox",
      },
      label: {
        type: String,
        default: "",
      },
      readOnly: {
        type: Boolean,
        default: false,
      },
      active: {
        type: Boolean,
        default: false,
      },
    },
    methods: {
      toggleCheckbox() {
        this.$refs.checkbox.classList.toggle("checkbox__btn--active");
        this.$refs.checkbox.classList.toggle("checkbox__focus");
      },
    },
  };
  </script>
  <style lang="css">
  @import url("../../../style/component/checkbox.css");
  </style>
  