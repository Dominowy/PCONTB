<template>
  <base-form-field
    :id="id"
    :label="label"
    :errors="getFieldErrors(propertyName)"
    :isTouched="isTouched"
    :isAllTouched="isAllTouched"
  >
    <select
      :id="id"
      class="form-select"
      :value="modelValue"
      @input="onInput"
      @blur="onBlur"
      :disabled="disabled"
    >
      <option :disabled="required" value="">{{ placeholder }}</option>
      <option v-for="item in options" :key="item.id" :value="item.id">
        {{ item.name }}
      </option>
    </select>
  </base-form-field>
</template>

<script setup>
import { ref, onMounted, watch } from "vue";
import ApiClient from "@/services/ApiClient";

const props = defineProps({
  id: String,
  label: String,
  placeholder: String,
  modelValue: [String, Number],
  apiUrl: String,
  errors: {
    type: Array,
    default: () => [],
  },
  isAllTouched: {
    type: Boolean,
    default: false,
  },
  property: {
    type: String,
    default: null,
  },
  disabled: {
    type: Boolean,
    default: false,
  },
  searchOption: String,
  required: {
    type: Boolean,
    default: true,
  },
});

const emit = defineEmits(["update:modelValue"]);
const propertyName = ref(null);
const isTouched = ref(false);
const options = ref([]);

onMounted(async () => {
  propertyName.value = props.property;

  if (propertyName.value == null) {
    propertyName.value = props.label;
  }

  await fetchData();
});

watch(
  () => props.options,
  (newOptions) => {
    options.value = [...newOptions];
  }
);

watch(
  () => props.modelValue,
  async () => {
    await fetchData();
  }
);

const fetchData = async () => {
  if (props.apiUrl) {
    const res = await ApiClient.request(props.apiUrl, {
      id: props.searchOption,
      includedId: props.modelValue,
    });
    options.value = res.data;
  }
};

const onInput = (e) => {
  emit("update:modelValue", e.target.value);
};

const onBlur = () => {
  setTimeout(() => {
    isTouched.value = true;
  }, 1000);
};

const getFieldErrors = () => {
  const field = props.property || props.label;
  return props.errors.filter((m) => m.propertyName === field).map((m) => m.message);
};
</script>
