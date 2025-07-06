<template>
  <div>
    <base-header :title="title" />
    <b-row class="mb-2">
      <base-form v-if="form" :formData="form" @submit="submit" @validate="validate">
        <b-card card>
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
        </b-card>
        <b-card v-if="route.params.id" no-body class="mt-2">
          <b-tabs card>
            <b-tab title="Collaborators">
              <collaborators-list />
            </b-tab>
            <b-tab title="Images">
              <base-form-file
                id="file"
                class="mt-2"
                property="Image"
                v-model="form.image"
                uploadUrl="/api/multimedia/upload-file"
                placeholder="Select image"
                :errors="errors"
                :src="form.imageData"
                :isAllTouched="isAllTouched"
              />
            </b-tab>
          </b-tabs>
        </b-card>
      </base-form>
    </b-row>
  </div>
</template>

<script setup>
import { useAddUpdate } from "@/composables/useAddUpdate";
import { reactive, onMounted, ref, watch } from "vue";
import ApiClient from "@/services/ApiClient";
import { useRouter, useRoute } from "vue-router";

const title = ref("Add project");

const router = useRouter();
const route = useRoute();

const form = reactive({});

onMounted(async () => {
  if (route.params.id) {
    title.value = "Update project";
  }

  const response = await getForm();
  Object.assign(form, response.form);

  if (route.params.id) {
    title.value = `Update - ${form.name}`;
  }

  setTitle(title.value);
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
  router.push({ name: "projects:project:update", params: { id: id } });
};

watch(
  () => route.params.id,
  async (newId, oldId) => {
    if (newId !== oldId) {
      title.value = newId ? "Update project" : "Add project";
      const response = await getForm();
      Object.assign(form, response.form);
      if (newId) {
        title.value = `Update - ${form.name}`;
      }
      setTitle(title.value);
    }
  },
  { immediate: false } // nie wykonujemy od razu, bo onMounted już to robi
);

const { isLoading, submit, validate, errors, isAllTouched, setTitle } = useAddUpdate(
  submitInternal,
  redirectAfterSucces
);
</script>
