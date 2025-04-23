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
import { useAddUpdatePage } from "@/composables/useAddUpdatePage";
import { reactive, onMounted, ref } from "vue";
import ApiClient from "@/services/ApiClient";
import { useRoute, useRouter } from "vue-router";

const router = useRouter();
const route = useRoute();

const title = ref("Project Update");
const form = reactive({});

onMounted(async () => {
  const response = await getForm();
  Object.assign(form, response.form);
});

const getForm = async () => {
  return await ApiClient.request("projects/projects/update/form", { id: route.params.id });
};

const submitInternal = async (onlyValidate) => {
  return await ApiClient.validate("projects/projects/update", onlyValidate, form);
};

const redirectAfterSucces = (id) => {
  router.push({ name: "projects:update", params: { id: id } });
};

const { isLoading, submit, validate, errors, isAllTouched } = useAddUpdatePage(
  title.value,
  submitInternal,
  redirectAfterSucces
);
</script>
