<template>
    <div
      class="loading-icon"
      :style="{ width: size + 'px', height: size + 'px' }"
    ></div>
  </template>
  <script>
  export default {
    props: {
      //kích thước của loading là bao nhiêu?
      size: {
        type: Number,
        default: 40,
      },
    },
  };
  </script>
  <style>
  .loading-icon {
    position: absolute;
    top: 50%;
    left: 50%;
    transform: (-50%, -50%);
    background-image: url("@/assets/loading.svg");
    background-size: 100%;
    width: 40px;
    height: 40px;
    animation: spin 1s linear infinite;
    z-index: 10000;
  }
  
  @keyframes spin {
    0% {
      transform: rotate(0deg);
    }
    100% {
      transform: rotate(360deg);
    }
  }
  </style>