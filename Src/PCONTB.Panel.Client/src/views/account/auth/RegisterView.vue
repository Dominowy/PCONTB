<template>
  <div class="d-flex align-items-center justify-content-center vh-100">
    <b-card class="container">
      <auth-form-header :isLoading="isLoading" />
      <base-form v-if="form" :formData="form" @submit="submit" @validate="validate">
        <base-form-input
          v-model="form.username"
          label="Username"
          placeholder="Enter username"
          :errors="getFieldErrors('Username')"
        />
        <base-form-input
          v-model="form.email"
          label="Email"
          placeholder="Enter email"
          :errors="getFieldErrors('Email')"
        />
        <base-form-input
          v-model="form.password"
          label="Password"
          type="password"
          placeholder="Enter password"
          :errors="getFieldErrors('Password')"
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
import { ref, reactive } from "vue";
import ApiClient from "@/services/ApiClient";

const errors = ref([]);

const touchedFields = ref({
  Email: false,
  Username: false,
  Password: false,
});

const form = reactive({
  username: null,
  email: null,
  password: null,
});

const submitInternal = async (onlyValidate = false) => {
  isLoading.value = true;

  try {
    await ApiClient.validate("account/auth/register", onlyValidate, form);
    if (!onlyValidate) {
      redirectToHome();
    }
    errors.value = [];
  } catch (error) {
    errors.value = error.message.errors;
    if (!onlyValidate) {
      setAllFieldsTouched();
    }
  } finally {
    isLoading.value = false;
  }
};

const setAllFieldsTouched = () => {
  Object.keys(touchedFields.value).forEach((field) => {
    touchedFields.value[field] = true;
  });
};

const setFieldTouched = (field) => {
  touchedFields.value[field] = true;
};

const isFieldTouched = (field) => {
  return touchedFields.value[field];
};

const getFieldErrors = (propertyName) => {
  if (!errors.value) return [];

  return errors.value.filter((m) => m.propertyName === propertyName).map((m) => m.message);
};

const redirectToHome = () => {
  router.push({ name: "Home" });
};

const { router, isLoading, submit, validate } = useAddEditPage("Register", submitInternal);
</script>
