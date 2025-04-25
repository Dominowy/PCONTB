<template>
  <div>
    <b-card class="border-0">
      <base-form v-if="form" :formData="form" @submit="submit" @validate="validate">
        <base-form-input
          id="email"
          class="mt-2"
          v-model="form.email"
          label="Email"
          placeholder="Enter email"
          :errors="errors"
          :isAllTouched="isAllTouched"
        />
        <base-form-input
          id="manageProjectPermission"
          class="mt-2"
          v-model="form.manageProjectPermission"
          label="Manage Project Permission"
          type="checkbox"
          :errors="errors"
          :isAllTouched="isAllTouched"
        />
        <base-form-input
          id="manageCommunityPermission"
          class="mt-2"
          v-model="form.manageCommunityPermission"
          label="Manage Community Permission"
          type="checkbox"
          :errors="errors"
          :isAllTouched="isAllTouched"
        />
        <base-form-input
          id="manageFulfillmentPermission"
          class="mt-2"
          v-model="form.manageFulfillmentPermission"
          label="Manage Fulfillment Permission"
          placeholder="Enter email"
          type="checkbox"
          :errors="errors"
          :isAllTouched="isAllTouched"
        />
        <div class="d-flex w-100">
          <base-form-submit-button label="Save" class="mt-4" />
          <base-loading-spinner v-if="isLoading" />
        </div>
      </base-form>
    </b-card>
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
  let response = await getForm();

  response.form.projectId = route.params.id;
  Object.assign(form, response.form);
});

const getForm = async () => {
  if (route.params.id) {
    return await ApiClient.request("projects/collaborators/add/form", {});
  }
  return await ApiClient.request("projects/collaborators/add/form", {});
};

const submitInternal = async (onlyValidate) => {
  if (route.params.id) {
    return await ApiClient.validate("projects/collaborators/add", onlyValidate, form);
  }

  return await ApiClient.validate("projects/collaborators/add", onlyValidate, form);
};

const { isLoading, submit, validate, errors, isAllTouched } = useAddUpdate(submitInternal);
</script>
