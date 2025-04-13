<template>
  <form>
    <slot></slot>
  </form>
</template>

<script setup>
import { watch } from "vue";
import { debounce } from "lodash";

const props = defineProps({
  formData: {
    default: null,
    required: true,
  },
});

const emit = defineEmits(["validate"]);

const debouncedValidate = debounce(async () => {
  emit("validate");
}, 500);

watch(() => props.formData, debouncedValidate, { deep: true });
</script>
