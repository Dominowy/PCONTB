<template>
  <div>
    <default-navbar-layout />
    <b-container class="mt-4">
      <router-view />
    </b-container>
  </div>
</template>

<script>
import { defineComponent } from "vue";
import DefaultNavbarLayout from "./DefaultNavbarLayout.vue";

export default defineComponent({
  components: { DefaultNavbarLayout },
});
</script>
