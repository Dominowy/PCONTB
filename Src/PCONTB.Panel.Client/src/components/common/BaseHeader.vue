<template>
  <div class="d-flex justify-content-between">
    <h2>{{ title }}</h2>
    <button class="btn btn-link text-secondary p-0" @click="goBack">
      <IBiArrowLeft style="font-size: 1.2rem" />
    </button>
  </div>
</template>

<script setup>
defineProps({
  title: {
    type: String,
    default: null,
  },
});

const goBack = () => {
  window.history.back();
  setTimeout(() => {
    window.location.reload();
  }, 100);
};
</script>
