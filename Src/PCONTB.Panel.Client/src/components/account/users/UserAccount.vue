<template>
  <div>
    <b-card class="border-0">
      <base-form v-if="form" :formData="form" @submit="submit" @validate="validate">
        <base-form-input
          id="username"
          class="mt-2"
          v-model="form.username"
          label="Username"
          placeholder="Enter username"
          :errors="errors"
          :isAllTouched="isAllTouched"
        />
        <base-form-input
          id="email"
          class="mt-2"
          v-model="form.email"
          label="Email"
          placeholder="Enter email"
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
import { useAddEditPage } from "@/composables/useAddEditPage";
import { reactive, onMounted } from "vue";
import ApiClient from "@/services/ApiClient";
import { useStore } from "@/store/index";
import { useRouter } from "vue-router";

const router = useRouter();
const store = useStore();

const form = reactive({});

onMounted(async () => {
  const response = await getForm();
  Object.assign(form, response.form);
});

const getForm = async () => {
  return await ApiClient.request("account/users/update/form", { id: store.user.id });
};

const submitInternal = async (onlyValidate) => {
  return await ApiClient.validate("account/users/update", onlyValidate, form);
};

const redirectAfterSucces = () => {
  router.go(0);
};

const { isLoading, submit, validate, errors, isAllTouched } = useAddEditPage(
  "Settings",
  submitInternal,
  redirectAfterSucces
);
</script>
