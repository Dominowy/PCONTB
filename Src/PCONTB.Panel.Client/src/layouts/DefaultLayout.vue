<template>
  <div class="page-content">
    <default-navbar-layout />
    <b-container class="mt-4">
      <router-view />
    </b-container>
  </div>
</template>

<style scoped>
.page-content {
  padding-bottom: 60px;
}
</style>

<script>
import { defineComponent } from "vue";
import DefaultNavbarLayout from "./DefaultNavbarLayout.vue";

export default defineComponent({
  components: { DefaultNavbarLayout },
});
</script>
