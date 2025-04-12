<template>
  <div class="d-flex align-items-center justify-content-center vh-100">
    <b-card class="container">
      <auth-form-header :isLoading="loading" />
      <b-form @submit.prevent="handleSubmit">
        <b-form-group class="mt-2" label="Username:" label-for="username">
          <b-form-input id="username" v-model="form.username" placeholder="Enter username" />
        </b-form-group>
        <b-form-group class="mt-2" label="Email:" label-for="username">
          <b-form-input id="email" v-model="form.email" placeholder="Enter email" />
        </b-form-group>
        <b-form-group class="mt-2" label="Password:" label-for="password">
          <b-form-input
            id="password"
            v-model="form.password"
            required
            type="password"
            placeholder="Enter password"
          />
        </b-form-group>
        <div class="d-flex w-100">
          <b-button class="mt-4 w-100" :disabled="loading" type="submit" variant="secondary" block
            >Login</b-button
          >
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
import { useRouter } from "vue-router";

const router = useRouter();

const loading = ref(false);
const errors = ref("");
const form = reactive({
  username: null,
  email: null,
  password: null,
});

const handleSubmit = async () => {
  loading.value = true;

  try {
    await ApiClient.validate("account/auth/register", true, form);
    redirectToHome();
  } catch (error) {
    console.log(error);
  } finally {
    loading.value = false;
  }
};

const redirectToHome = () => {
  router.push({ name: "Home" });
};
</script>
