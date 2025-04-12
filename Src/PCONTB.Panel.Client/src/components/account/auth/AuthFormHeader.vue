<template>
  <button class="btn btn-link text-secondary p-0" @click="goBack">
    <IBiArrowLeft style="font-size: 1.2rem" />
  </button>
  <div v-if="!isLoading" class="text-center mb-4">
    <router-link to="/" class="btn text-secondary p-0">
      <h2 class="font-weight-bold text-secondary">PCONTB</h2>
    </router-link>
  </div>
  <div v-else>
    <loading-spinner />
  </div>
</template>

<script setup>
import { useRouter } from "vue-router";

const router = useRouter();

defineProps({
  isLoading: {
    type: Boolean,
    default: false,
  },
});

const goBack = () => {
  router.back();
};
</script>
