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
  validate: Function,
});

const emit = defineEmits(["validate"]);

const debouncedValidate = debounce(async () => {
  emit("validate");
}, 1000);

watch(() => props.formData, debouncedValidate, { deep: true });
</script>
