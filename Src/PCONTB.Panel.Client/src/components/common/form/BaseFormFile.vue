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
        @input="onInput"
        @change="onBlur"
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
import axios from "axios";

const props = defineProps({
  id: String,
  placeholder: String,
  errors: {
    type: Array,
    default: () => [],
  },
  uploadUrl: {
    type: String,
    default: null,
  },
  isAllTouched: {
    type: Boolean,
    default: false,
  },
  property: {
    type: String,
    default: null,
  },
  modelValue: [Object],
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

  const messages = props.errors
    .filter((m) => m.propertyName?.startsWith(propertyName))
    .map((m) => m.message);

  return [...new Set(messages)];
};

const triggerFileInput = () => {
  fileInput.value?.click();
};

const onInput = (event) => {
  const file = event.target.files[0];
  uploadFile(file);
};

const onFileChange = (fileName, path, contentType, file) => {
  emit("update:modelValue", { fileName: fileName, path: path, contentType: contentType }, file);
};

const uploadFile = (file) => {
  const fileName = file.name;
  const contentType = file.type;

  var data = new FormData();
  data.append("file", file);

  axios
    .post(props.uploadUrl, data, { headers: { "Content-Type": "multipart/form-data" } })
    .then((response) => {
      console.log("Uploading complete: " + fileName);
      onFileChange(fileName, response.data, contentType, file);
    })
    .catch((error) => {
      console.error("FAIL", error);
    });
};

watch(
  () => props.modelValue,
  (val) => {
    console.log(val);
    if (!val) fileLabel.value = "";
    else if (val.fileName) {
      fileLabel.value = val.fileName;
    }
  }
);
</script>
