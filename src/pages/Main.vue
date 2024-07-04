<script setup lang="ts">

import { onMounted, ref } from "vue";




onMounted(async () => {
  // menuList.value = await GetLeftMenuAsync();
  localStorage.setItem("listNum", "1");
});
</script>

<template>
  <Toast />
  <MainHeader />
  <div class="tw-flex">
    <div class="tw-w-[15%]">
      <LeftMenu />
    </div>
    <div class="tw-w-[85%] tw-m-[10px]">
<!--      <Documents ref="documentsComp" />-->
    </div>
  </div>
</template>

<style scoped></style>
