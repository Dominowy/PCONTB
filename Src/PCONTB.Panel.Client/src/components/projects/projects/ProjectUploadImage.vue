<template>
  <div>
    <base-form v-if="form" :formData="form" @submit="autoSubmit" @validate="autoSubmit">
      <base-form-file
        id="file"
        class="mt-2"
        property="Image"
        v-model="form.image"
        uploadUrl="/api/multimedia/upload-file"
        placeholder="Select image"
        :errors="errors"
        :src="form.data"
        :isAllTouched="isAllTouched"
      />
    </base-form>
  </div>
</template>

<script setup>
import { useAddUpdate } from "@/composables/useAddUpdate";
import { reactive, onMounted, watch } from "vue";
import ApiClient from "@/services/ApiClient";
import { useRouter, useRoute } from "vue-router";

const props = defineProps({
  modelValue: String,
});

const emit = defineEmits(["update:modelValue", "onImageChange"]);

const router = useRouter();
const route = useRoute();
const form = reactive({});

onMounted(async () => {
  const response = await getForm();
  Object.assign(form, response.form);
});

const getForm = async () => {
  if (props.modelValue) {
    return await ApiClient.request("projects/images/update/form", { id: props.modelValue });
  }

  return await ApiClient.request("projects/images/add/form", {});
};

const submitInternal = async (onlyValidate) => {
  if (props.modelValue) {
    return await ApiClient.validate("projects/images/update", onlyValidate, form);
  }

  return await ApiClient.validate("projects/images/add", onlyValidate, form);
};

const redirectAfterSucces = (id) => {
  updateValue(id);
  emit("onImageChange");
  router.push({ name: "projects:project:settings", params: { id: route.params.id } });
};

function updateValue(newVal) {
  emit("update:modelValue", newVal);
}

const autoSubmit = async () => {
  validate();
  submit();
};

watch(
  () => props.modelValue,
  async (newVal) => {
    if (newVal) {
      const response = await getForm();
      Object.assign(form, response.form);
    }
  },
  () => props.modelValue?.path,
  async (newVal) => {
    if (newVal) {
      autoSubmit();
    }
  },
  { immediate: true }
);

const { submit, validate, errors, isAllTouched } = useAddUpdate(
  submitInternal,
  redirectAfterSucces
);
</script>
