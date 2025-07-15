<template>
  <div>
    <base-header :isLoading="isLoading" :title="title" />
    <div>
      <b-card card>
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
          <base-form-submit-panel :isLoading="isLoading" />
        </base-form>
      </b-card>
    </div>
  </div>
</template>

<script setup>
import { useAddUpdate } from "@/composables/useAddUpdate";
import ApiClient from "@/services/ApiClient";
import { reactive, onMounted, ref } from "vue";
import { useRouter, useRoute } from "vue-router";

const router = useRouter();
const route = useRoute();
const form = reactive({});

const title = ref("Add category");

onMounted(async () => {
  const response = await getForm();
  Object.assign(form, response.form);

  if (route.params.id) {
    title.value = `Update - ${form.name}`;
  }

  setTitle(title.value);
});

const getForm = async () => {
  if (route.params.id) {
    return await ApiClient.request("categories/update/form", { id: route.params.id });
  }
  return await ApiClient.request("categories/add/form", {});
};

const submitInternal = async (onlyValidate) => {
  if (route.params.id) {
    return await ApiClient.validate("categories/update", onlyValidate, form);
  }
  return await ApiClient.validate("categories/add", onlyValidate, form);
};

const redirectAfterSucces = (response) => {
  router.push({ name: "panel:category:update", params: { id: response.id } });
};

const { isLoading, submit, validate, errors, isAllTouched, setTitle } = useAddUpdate(
  submitInternal,
  redirectAfterSucces
);
</script>
