<template>
  <div>
    <base-form v-if="form" :formData="form" @submit="submit" @validate="validate">
      <base-form-file
        id="file"
        class="mt-2"
        v-model="file"
        label="File"
        placeholder="Select file"
        :errors="errors"
        :isAllTouched="isAllTouched"
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
  if (route.params.id) {
    return await ApiClient.request("projects/projects/update/form", { id: route.params.id });
  }
  return await ApiClient.request("projects/projects/add/form", {});
};

const submitInternal = async (onlyValidate) => {
  if (route.params.id) {
    return await ApiClient.validate("projects/projects/update", onlyValidate, form);
  }

  return await ApiClient.validate("projects/projects/add", onlyValidate, form);
};

const redirectAfterSucces = (id) => {
  router.push({ name: "projects:project:settings", params: { id: id } });
};

const { isLoading, submit, validate, errors, isAllTouched } = useAddUpdate(
  submitInternal,
  redirectAfterSucces
);
</script>
