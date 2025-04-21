<template>
  <div>
    <base-header :title="title" />
    <b-row>
      <b-card class="border">
        <base-form v-if="form" :formData="form" @submit="submit" @validate="validate">
          <base-form-input
            id="name"
            class="mt-2"
            v-model="form.name"
            label="Name"
            placeholder="Enter name"
            :errors="errors"
            :isAllTouched="isAllTouched"
          />
          <div class="d-flex w-100">
            <base-form-submit-button label="Save" class="mt-4" />
            <base-loading-spinner v-if="isLoading" />
          </div>
        </base-form>
      </b-card>
    </b-row>
  </div>
</template>

<script setup>
import { useAddEditPage } from "@/composables/useAddEditPage";
import { reactive, onMounted, ref } from "vue";
import ApiClient from "@/services/ApiClient";
import { useRoute, useRouter } from "vue-router";

const router = useRouter();
const route = useRoute();

const title = ref(route.params.id ? "Project Update" : "Project Add");
const form = reactive({});

onMounted(async () => {
  const response = await getForm();
  Object.assign(form, response.form);

  await getCategories();
  await getCountries();
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

const redirectAfterSucces = () => {
  router.push({ name: "projects:update", params: { id: form.id } });
};

const getCategories = async () => {
  let response = await ApiClient.request("projects/categories/get-all", {});
};

const getCountries = async () => {
  let response = await ApiClient.request("locations/countries/get-all", {});
};

const { isLoading, submit, validate, errors, isAllTouched } = useAddEditPage(
  title.value,
  submitInternal,
  redirectAfterSucces
);
</script>
