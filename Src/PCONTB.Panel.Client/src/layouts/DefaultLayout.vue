<template>
  <div>
    <Navbar />

    <b-container class="mt-4">
      <router-view />
    </b-container>
  </div>
</template>

<script>
import { defineComponent } from "vue";
import Navbar from "./NavbarLayout.vue";

export default defineComponent({
  components: { Navbar },
});
</script>
