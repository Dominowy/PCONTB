<template>
  <base-form-field
    :id="id"
    :errors="getFieldErrors(propertyName)"
    :isTouched="isTouched"
    :isAllTouched="isAllTouched"
  >
    <div class="custom-file-wrapper" @click="triggerFileInput">
      {{ fileLabel || placeholder }}
      <input
        ref="fileInput"
        :id="id"
        type="file"
        class="hidden-input"
        :multiple="multiple"
        @change="onFileChange"
        @blur="onBlur"
      />
    </div>
  </base-form-field>
</template>

<style scoped>
.custom-file-wrapper {
  border: 2px dashed #90caf9;
  padding: 24px;
  border-radius: 6px;
  text-align: center;
  cursor: pointer;
  position: relative;
}

.file-placeholder {
  margin: 0 0 8px;
  color: #666;
  font-size: 14px;
  word-break: break-word;
}

.hidden-input {
  display: none;
}
</style>

<script setup>
import { ref, onMounted, watch } from "vue";

const props = defineProps({
  id: String,
  placeholder: String,
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
  modelValue: [File, Array],
  multiple: {
    type: Boolean,
    default: false,
  },
});

const emit = defineEmits(["update:modelValue"]);

const propertyName = ref(null);
const isTouched = ref(false);
const fileInput = ref(null);
const fileLabel = ref("");

onMounted(() => {
  propertyName.value = props.property || props.label;
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

const triggerFileInput = () => {
  fileInput.value?.click();
};

const onFileChange = (event) => {
  const files = Array.from(event.target.files);
  if (!files.length) return;

  emit("update:modelValue", props.multiple ? files : files[0]);

  fileLabel.value = props.multiple ? files.map((f) => f.name).join(", ") : files[0].name;

  onBlur();
};

watch(
  () => props.modelValue,
  (val) => {
    if (!val) fileLabel.value = "";
    else if (Array.isArray(val)) {
      fileLabel.value = val.map((f) => f.name).join(", ");
    } else if (val.name) {
      fileLabel.value = val.name;
    }
  }
);
</script>
