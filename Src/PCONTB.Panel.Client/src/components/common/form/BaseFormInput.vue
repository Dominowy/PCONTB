<template>
  <base-form-field
    :label="label"
    :errors="getFieldErrors(label)"
    :isTouched="isTouched"
    :isAllTouched="isAllTouched"
  >
    <input
      class="form-control"
      :type="type"
      :value="modelValue"
      :placeholder="placeholder"
      @input="onInput"
      @blur="onBlur"
      @change="onBlur"
    />
  </base-form-field>
</template>

<script setup>
import { ref } from "vue";
const props = defineProps({
  label: String,
  placeholder: String,
  type: {
    type: String,
    default: "text",
  },
  errors: {
    type: Array,
    default: () => [],
  },
  isAllTouched: {
    type: Boolean,
    default: false,
  },
  modelValue: [String, Number],
});

const isTouched = ref(false);

const emit = defineEmits(["update:modelValue"]);

const onBlur = () => {
  isTouched.value = true;
};

const getFieldErrors = (propertyName) => {
  if (!props.errors) return [];

  return props.errors.filter((m) => m.propertyName === propertyName).map((m) => m.message);
};

const onInput = (event) => {
  emit("update:modelValue", event.target.value);
};
</script>
