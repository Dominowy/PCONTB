<template>
  <base-form-field
    :id="id"
    :errors="getFieldErrors(propertyName)"
    :isTouched="isTouched"
    :isAllTouched="isAllTouched"
  >
    <div class="custom-file-wrapper" @click="triggerFileInput">
      <template v-if="isUploading">
        <div class="progress-wrapper">
          <div class="progress-bar" :style="{ width: uploadProgress + '%' }"></div>
          <div class="progress-text">{{ uploadProgress }}%</div>
        </div>
      </template>
      <div v-if="props.modelValue?.previewUrl">
        <img
          v-if="props.modelValue.contentType?.startsWith('image/')"
          :src="props.modelValue.previewUrl"
          alt="Preview"
        />

        <video
          v-else-if="props.modelValue.contentType?.startsWith('video/')"
          :src="props.modelValue.previewUrl"
          controls
        />
      </div>
      <div v-else>
        {{ placeholder }}
      </div>

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

.progress-wrapper {
  position: relative;
  height: 20px;
  background-color: #eee;
  border-radius: 4px;
  overflow: hidden;
  margin-bottom: 12px;
}

.progress-bar {
  height: 100%;
  background-color: #42a5f5;
  transition: width 0.2s ease-in-out;
}

.progress-text {
  position: absolute;
  width: 100%;
  top: 0;
  left: 0;
  text-align: center;
  font-size: 12px;
  line-height: 20px;
  color: #fff;
}

video,
img {
  border-radius: 12px;
  outline: none;
  max-width: 100%;
  max-height: 50%;
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
const uploadProgress = ref(0);
const isUploading = ref(false);

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
  const preview = URL.createObjectURL(file);

  emit(
    "update:modelValue",
    { fileName: fileName, path: path, contentType: contentType, previewUrl: preview },
    file
  );
};

const uploadFile = (file) => {
  isUploading.value = true;
  uploadProgress.value = 0;

  const fileName = file.name;
  const contentType = file.type;

  var data = new FormData();
  data.append("file", file);

  axios
    .post(props.uploadUrl, data, {
      headers: { "Content-Type": "multipart/form-data" },
      onUploadProgress: (progressEvent) => {
        if (progressEvent.lengthComputable) {
          uploadProgress.value = Math.round((progressEvent.loaded * 100) / progressEvent.total);
        }
      },
    })
    .then((response) => {
      isUploading.value = false;
      console.log("Uploading complete: " + fileName);
      onFileChange(fileName, response.data, contentType, file);
    })
    .catch((error) => {
      isUploading.value = false;
      console.error("FAIL", error);
    });
};

watch(
  () => props.modelValue?.previewUrl,
  (newVal, oldVal) => {
    if (oldVal) {
      URL.revokeObjectURL(oldVal);
    }
  }
);
</script>
