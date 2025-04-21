<template>
  <form @submit.prevent="handleSubmit">
    <slot></slot>
  </form>
</template>

<script setup>
import { watch, ref } from "vue";
import { debounce } from "lodash";

const props = defineProps({
  formData: {
    default: null,
    required: true,
  },
});

const emit = defineEmits(["submit", "validate"]);

const isFirstRender = ref(true);

const debouncedValidate = debounce(async () => {
  if (!isFirstRender.value) {
    emit("validate");
  }
}, 500);

watch(
  () => props.formData,
  () => {
    if (isFirstRender.value) {
      isFirstRender.value = false;
    } else {
      debouncedValidate();
    }
  },
  { deep: true }
);

const handleSubmit = async () => {
  emit("submit");
};
</script>
