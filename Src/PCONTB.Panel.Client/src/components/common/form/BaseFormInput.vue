<template>
  <template v-if="type == 'checkbox'">
    <div class="form-check form-switch mt-2">
      <label class="form-check-label" :for="id">
        {{ label }}
      </label>
      <input
        :id="id"
        class="form-check-input"
        type="checkbox"
        role="switch"
        :checked="modelValue === true || modelValue === 'true'"
        @change="onInput($event.target.checked)"
      />
    </div>
  </template>
  <template v-else>
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
        @input="onInput($event.target.value)"
        @blur="onBlur"
        @change="onBlur"
      />
    </base-form-field>
  </template>
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
  modelValue: [String, Number, Boolean],
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
  setTimeout(() => {
    isTouched.value = true;
  }, 1000);
};

const getFieldErrors = (propertyName) => {
  if (!props.errors) return [];

  return props.errors.filter((m) => m.propertyName === propertyName).map((m) => m.message);
};

const onInput = (event) => {
  emit("update:modelValue", event);
};
</script>
