<template lang="">
    <div :class="className"><slot></slot></div>
  </template>
  <script>
  export default {
    props: {
      className: {
        type: String,
      },
    },
  };
  </script>
  <style lang="css" scoped>
  .text_center {
    text-align: center;
    justify-content: center;
  }
  </style>