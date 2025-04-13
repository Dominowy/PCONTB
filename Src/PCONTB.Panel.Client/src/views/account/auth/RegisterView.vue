<template>
  <div class="d-flex align-items-center justify-content-center vh-100">
    <b-card class="container">
      <auth-form-header :isLoading="isLoading" />
      <base-form v-if="form" :formData="form" @submit="submit" @validate="validate">
        <base-form-input
          id="username"
          v-model="form.username"
          label="Username"
          placeholder="Enter username"
          :errors="errors"
          :isAllTouched="isAllTouched"
        />
        <base-form-input
          id="email"
          v-model="form.email"
          label="Email"
          placeholder="Enter email"
          :errors="errors"
          :isAllTouched="isAllTouched"
        />
        <base-form-input
          id="password"
          v-model="form.password"
          label="Password"
          type="password"
          placeholder="Enter password"
          :errors="errors"
          :isAllTouched="isAllTouched"
        />
        <div class="d-flex w-100">
          <b-button
            class="mt-4 w-100"
            :disabled="isLoading"
            type="submit"
            variant="secondary"
            block
            v-on:click.prevent="submit"
          >
            Register
          </b-button>
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
import { useAddEditPage } from "@/composables/useAddEditPage";
import { reactive } from "vue";
import ApiClient from "@/services/ApiClient";

const form = reactive({
  username: null,
  email: null,
  password: null,
});

const submitInternal = async (onlyValidate) => {
  return await ApiClient.validate("account/auth/register", onlyValidate, form);
};

const redirectAfterSucces = () => {
  router.push({ name: "Home" });
};

const { router, isLoading, submit, validate, errors, isAllTouched } = useAddEditPage(
  "Register",
  submitInternal,
  redirectAfterSucces
);
</script>
