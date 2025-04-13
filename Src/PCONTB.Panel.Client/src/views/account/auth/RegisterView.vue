<template>
  <div class="d-flex align-items-center justify-content-center vh-100">
    <b-card class="container">
      <auth-form-header :isLoading="isLoading" />
      <form @submit.prevent="handleSubmit(false)">
        <b-form-group class="mt-2" label="Username:" label-for="username">
          <b-form-input
            id="Username"
            v-model="form.username"
            placeholder="Enter username"
            @blur="setFieldTouched('Username')"
          />
          <div v-if="isFieldTouched('Username') && getFieldErrors('Username')" class="text-danger">
            {{ getFieldErrors("Username") }}
          </div>
        </b-form-group>
        <b-form-group class="mt-2" label="Email:" label-for="username">
          <b-form-input
            id="Email"
            v-model="form.email"
            placeholder="Enter email"
            @blur="setFieldTouched('Email')"
          />
          <div v-if="isFieldTouched('Email') && getFieldErrors('Email')" class="text-danger">
            {{ getFieldErrors("Email") }}
          </div>
        </b-form-group>
        <b-form-group class="mt-2" label="Password:" label-for="password">
          <b-form-input
            id="Password"
            v-model="form.password"
            type="password"
            placeholder="Enter password"
            @blur="setFieldTouched('Password')"
          />

          <div v-if="isFieldTouched('Password') && getFieldErrors('Password')" class="text-danger">
            {{ getFieldErrors("Password") }}
          </div>
        </b-form-group>
        <div class="d-flex w-100">
          <b-button
            class="mt-4 w-100"
            :disabled="isLoading"
            type="submit"
            variant="secondary"
            block
          >
            Register
          </b-button>
        </div>
      </form>
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
import { ref, reactive, watch } from "vue";
import ApiClient from "@/services/ApiClient";
import { debounce } from "lodash";

const { router, isLoading } = useAddEditPage("Register");

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

const debouncedValidate = debounce(async () => {
  await handleSubmit(true);
}, 500);

watch(() => form, debouncedValidate, { deep: true });

const handleSubmit = async (onlyValidate = false) => {
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
  if (!errors.value) return null;
  const error = errors.value.find((m) => m.propertyName == propertyName);
  return error ? error.message : null;
};

const redirectToHome = () => {
  router.push({ name: "Home" });
};
</script>
