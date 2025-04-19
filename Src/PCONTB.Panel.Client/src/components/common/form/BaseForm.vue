<template>
  <form @submit.prevent="handleSubmit">
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

const emit = defineEmits(["submit", "validate"]);

const debouncedValidate = debounce(async () => {
  emit("validate");
}, 1000);

watch(() => props.formData, debouncedValidate, { deep: true });

const handleSubmit = async () => {
  emit("submit");
};
</script>
