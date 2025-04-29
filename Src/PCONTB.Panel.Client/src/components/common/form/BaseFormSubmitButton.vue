<template>
  <button type="submit" class="btn" :class="variantClass">
    <slot>{{ label }}</slot>
  </button>
</template>

<script setup>
import { computed } from "vue";

const props = defineProps({
  label: {
    type: String,
    default: "Save",
  },
  variant: {
    type: String,
    default: "primary",
  },
});

const variantClass = computed(() => {
  return `btn-${props.variant}`;
});
</script>
