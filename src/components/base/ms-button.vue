<template lang="">
    <button class="form-btn" :class="style" :id="id" :disable="disable" :tabindex="tabindex" :type="type">{{ title }}</button>
</template>
<script>
export default {
    name: "msButton",
    props: {
        style: String,
        disable: Boolean,
        title: String,
        id: String,
        tabindex: String,
        type: String,

    },
};
</script>
<style lang=""></style>