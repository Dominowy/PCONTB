<template>
  <div>
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
      <base-form-select
        id="category"
        class="mt-2"
        v-model="form.categoryId"
        label="Category"
        placeholder="Select category"
        property="CategoryId"
        :errors="errors"
        :isAllTouched="isAllTouched"
        api-url="/projects/categories/select-categories"
      />
      <base-form-select
        :key="form.categoryId"
        id="subcategory"
        class="mt-2"
        v-model="form.subcategoryId"
        label="Subcategory"
        property="SubcategoryId"
        placeholder="No subcategory"
        :errors="errors"
        :isAllTouched="isAllTouched"
        api-url="/projects/categories/select-subcategories"
        :searchOption="form.categoryId"
        :disabled="form.categoryId == null"
        :required="false"
      />
      <base-form-select
        id="country"
        class="mt-2"
        v-model="form.countryId"
        label="Country"
        property="CountryId"
        placeholder="Select country"
        :errors="errors"
        :isAllTouched="isAllTouched"
        api-url="/locations/countries/select-countries"
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
