<template>
    <div class="header">
                <div class="h-left">
                    <span class="icon-h"></span>
                    <div class="title-pro">{{Resource.HEADER.nameCopany}}</div>
                    <span class="icon-r"></span>
                </div>
                <div class="h-right">
                    <span class="mi-category marginRight16"></span> 
                    <span class="mi-setting__nav marginRight16"></span> 
                    <span class="mi-chat__nav marginRight16"></span>    
                    <span class="mi-help__nav marginRight16"></span>
                    <span class="bell marginRight16"></span>
                    <div class="user marginRight16">
                        <span class="logo-us"></span>
                        <div class="us-login">{{Resource.HEADER.userName}}</div>
                        <span class="us-r"></span>
                    </div>
                </div>
            </div>
</template>
<script>
import Resource from '@/Resource/Resource';
export default {
    data(){
        return {
        Resource : Resource
        }
    }
}
</script>
