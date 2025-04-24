<template>
  <div class="d-flex align-items-center justify-content-center vh-100">
    <b-card class="container">
      <auth-form-header :isLoading="isLoading" />
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
        <base-form-input
          id="password"
          class="mt-2"
          v-model="form.password"
          label="Password"
          type="password"
          placeholder="Enter password"
          :errors="errors"
          :isAllTouched="isAllTouched"
        />
        <div class="d-flex w-100">
          <base-form-submit-button label="Register" class="mt-4 w-100" />
        </div>
      </base-form>
    </b-card>
  </div>
</template>

<style scoped>
.container {
  max-width: 400px;
  border: 1px solid #ccc;
  border-radius: 10px;
  padding: 2rem;
  background: white;
}
</style>

<script setup>
import { useAddUpdatePage } from "@/composables/useAddUpdatePage";
import { useView } from "@/composables/useView";
import { reactive, onMounted } from "vue";
import ApiClient from "@/services/ApiClient";
import { useStore } from "@/store/index";
import { useRouter } from "vue-router";

const router = useRouter();
const store = useStore();

const form = reactive({});

onMounted(async () => {
  if (store.isAuthenticated) {
    router.push({ name: "home" });
  }

  const response = await getForm();
  Object.assign(form, response.form);
});

const getForm = async () => {
  return await ApiClient.request("account/auth/register/form", {});
};

const submitInternal = async (onlyValidate) => {
  return await ApiClient.validate("account/auth/register", onlyValidate, form);
};

const redirectAfterSucces = () => {
  router.push({ name: "home" });
};

const { isLoading, submit, validate, errors, isAllTouched } = useAddUpdatePage(
  submitInternal,
  redirectAfterSucces
);
useView("Register");
</script>
