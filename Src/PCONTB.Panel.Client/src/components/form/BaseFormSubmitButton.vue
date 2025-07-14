<template>
  <button type="submit" class="btn" :class="variantClass" :disabled="disabled">
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
  disabled: {
    type: Boolean,
    default: false,
  },
});

const variantClass = computed(() => {
  return `btn-${props.variant}`;
});
</script>
