<template>
  <b-spinner label="Loading..." :class="spinnerClass" />
</template>

<script setup>
defineProps({
  spinnerClass: {
    type: String,
    default: null,
  },
});
</script>
