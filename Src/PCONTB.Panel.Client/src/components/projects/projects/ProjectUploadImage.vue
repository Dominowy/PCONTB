<template>
  <div>
    <base-form v-if="form" :formData="form" @submit="submit" @validate="validate">
      <base-form-file
        id="file"
        class="mt-2"
        v-model="form.image"
        label="File"
        placeholder="Select files"
        :errors="errors"
        :isAllTouched="isAllTouched"
        :multiple="false"
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
  return await ApiClient.validate("projects/images/add", onlyValidate, form);
};

const redirectAfterSucces = (id) => {
  router.push({ name: "projects:project:settings", params: { id: id } });
};

const { isLoading, submit, validate, errors, isAllTouched } = useAddUpdate(
  submitInternal,
  redirectAfterSucces
);
</script>
