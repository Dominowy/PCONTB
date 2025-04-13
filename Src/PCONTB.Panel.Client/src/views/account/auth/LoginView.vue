<template>
  <div class="d-flex align-items-center justify-content-center vh-100">
    <b-card class="container">
      <auth-form-header :isLoading="isLoading" />
      <b-form @submit.prevent="handleSubmit">
        <b-form-group class="mt-2" label="UserName:" label-for="username">
          <b-form-input id="username" v-model="form.login" placeholder="Enter username" />
        </b-form-group>
        <b-form-group class="mt-2" label="Password:" label-for="password">
          <b-form-input
            id="password"
            v-model="form.password"
            type="password"
            placeholder="Enter password"
          />
        </b-form-group>
        <div class="d-flex w-100">
          <b-button class="mt-4 w-100" :disabled="isLoading" type="submit" variant="secondary" block
            >Login</b-button
          >
        </div>
        <div class="text-danger text-center" variant="danger">{{ errorMessage }}</div>
        <div class="text-center mt-3">
          <small>
            Don't have an account?
            <router-link to="/register" class="text-primary">Register</router-link>
          </small>
        </div>
      </b-form>
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
import { ref, reactive } from "vue";
import ApiClient from "@/services/ApiClient";
import { useAddEditPage } from "@/composables/useAddEditPage";

const { router, isLoading } = useAddEditPage("Login");

const errorMessage = ref("");
const form = reactive({
  login: null,
  password: null,
});

const handleSubmit = async () => {
  isLoading.value = true;
  errorMessage.value = null;

  try {
    await ApiClient.request("account/auth/login", form);
    redirectToHome();
  } catch (error) {
    errorMessage.value = error.message;
  } finally {
    isLoading.value = false;
  }
};

const redirectToHome = () => {
  router.push({ name: "Home" });
};
</script>
