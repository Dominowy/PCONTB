<template>
  <div>
    <b-card class="border-0">
      <base-form v-if="form" :formData="form" @submit="submit" @validate="validate">
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
        <base-form-input
          id="confirm"
          class="mt-2"
          v-model="form.confirmPassword"
          label="Confirm Password"
          property="ConfirmPassword"
          type="password"
          placeholder="Enter password"
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
import { useAddUpdatePage } from "@/composables/useAddUpdatePage";
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
  return await ApiClient.request("account/users/update-password/form", { id: store.user.id });
};

const submitInternal = async (onlyValidate) => {
  return await ApiClient.validate("account/users/update-password", onlyValidate, form);
};

const redirectAfterSucces = () => {
  router.go(0);
};

const { isLoading, submit, validate, errors, isAllTouched } = useAddUpdatePage(
  "Settings",
  submitInternal,
  redirectAfterSucces
);
</script>
