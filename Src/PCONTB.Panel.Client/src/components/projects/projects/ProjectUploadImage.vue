<template>
  <div>
    <base-form v-if="form" :formData="form" @submit="submit" @validate="validate">
      <base-form-file
        id="file"
        class="mt-2"
        property="files"
        v-model="form.images"
        placeholder="Select files"
        :errors="errors"
        :isAllTouched="isAllTouched"
        :multiple="true"
      />
      <base-form-submit-panel :isLoading="isLoading" />
    </base-form>
  </div>
</template>

<script setup>
import { useAddUpdate } from "@/composables/useAddUpdate";
import { reactive, onMounted } from "vue";
import ApiClient from "@/services/ApiClient";
import { useRouter, useRoute } from "vue-router";

const router = useRouter();
const route = useRoute();
const form = reactive({});

onMounted(async () => {
  const response = await getForm();
  Object.assign(form, response.form);
});

const getForm = async () => {
  return await ApiClient.request("projects/images/add/form", {});
};

const submitInternal = async (onlyValidate) => {
  try {
    const formData = new FormData();

    formData.append("ProjectId", route.params.id);
    form.images.forEach((f) => formData.append("Images", f));

    return await ApiClient.requestFormData("projects/images/add", formData);
  } catch (error) {
    console.log(error);
  }
};

const redirectAfterSucces = (id) => {
  router.push({ name: "projects:project:settings", params: { id: id } });
};

const { isLoading, submit, validate, errors, isAllTouched } = useAddUpdate(
  submitInternal,
  redirectAfterSucces
);
</script>
