<template>
  <button type="submit" class="btn btn-md" :class="variantClass">
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
    default: "secondary",
  },
});

const variantClass = computed(() => {
  return `btn-${props.variant}`;
});
</script>
