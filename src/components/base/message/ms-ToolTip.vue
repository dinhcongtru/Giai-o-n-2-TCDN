<template>
    <div class="tooltip">
      <span
        class="tooltiptext"
        :class="{ top: isTop, bottom: !isTop, rectangle: isRectangle }"
        >{{ text }}</span
      >
    </div>
</template>
<script>
  export default {
    name:"msTooltip",
    props: {
      text: {
        type: String,
        default: "tool-tip-text",
      },
      //vị trí của mũi tên (tên/dưới)
      isTop: {
        type: Boolean,
        default: false, //mặc định bên dưới
      },
      isRectangle: {
        //có phải hình chữ nhật không
        type: Boolean,
        default: false, //mặc định có mũi tên
      },
    },
  };
</script>
  
<style>
@import url("@/css/toolTip.css");
</style>