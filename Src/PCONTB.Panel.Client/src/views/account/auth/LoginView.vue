<template>
  <div class="d-flex align-items-center justify-content-center vh-100">
    <b-card class="container">
      <auth-form-header :isLoading="isLoading" />
      <base-form v-if="form" :formData="form" @submit="submit">
        <base-form-input
          id="login"
          class="mt-2"
          v-model="form.login"
          label="Login"
          placeholder="Enter login"
        />
        <base-form-input
          id="password"
          class="mt-2"
          v-model="form.password"
          label="Password"
          type="password"
          placeholder="Enter password"
        />
        <div class="text-danger text-center" variant="danger">{{ errorMessage }}</div>
        <div class="d-flex w-100">
          <base-form-submit-button label="Login" class="mt-4 w-100" />
        </div>
      </base-form>
      <div class="text-center mt-3">
        <small>
          Don't have an account?
          <router-link to="/auth/register" class="text-primary">Register</router-link>
        </small>
      </div>
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
import { reactive, onMounted } from "vue";
import ApiClient from "@/services/ApiClient";
import { useAddUpdatePage } from "@/composables/useAddUpdatePage";
import { useStore } from "@/store/index";
import { useRouter } from "vue-router";

const router = useRouter();
const store = useStore();

onMounted(async () => {
  if (store.isAuthenticated) {
    router.push({ name: "home" });
  }
});

const form = reactive({
  login: null,
  password: null,
});

const submitInternal = async () => {
  return await ApiClient.request("account/auth/login", form);
};

const redirectAfterSucces = () => {
  router.push({ name: "home" });
};

const { submit, isLoading, errorMessage } = useAddUpdatePage(
  "Login",
  submitInternal,
  redirectAfterSucces
);
</script>
