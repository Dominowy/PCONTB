<template>
  <base-form-field
    :id="id"
    :label="label"
    :errors="getFieldErrors(propertyName)"
    :isTouched="isTouched"
    :isAllTouched="isAllTouched"
  >
    <input
      :id="id"
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
import { ref, onMounted } from "vue";
const props = defineProps({
  id: String,
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
  property: { type: String, default: null },
  modelValue: [String, Number],
});

const propertyName = ref(null);

const isTouched = ref(false);

const emit = defineEmits(["update:modelValue"]);

onMounted(async () => {
  propertyName.value = props.property;

  if (propertyName.value == null) {
    propertyName.value = props.label;
  }
});

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
