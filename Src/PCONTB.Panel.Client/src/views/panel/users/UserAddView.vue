<template>
  <div>
    <base-header :isLoading="isLoading" title="User - Add" />
    <div>
      <b-card card>
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
          <base-form-picker-select
            id="roles"
            class="mt-2"
            v-model="form.roles"
            property="Roles"
            placeholder="Roles"
            :errors="errors"
            :isAllTouched="isAllTouched"
            :options="availableRoles"
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
import { useRouter } from "vue-router";

const router = useRouter();
const form = reactive({});
const availableRoles = ref([]);

onMounted(async () => {
  const response = await getForm();
  Object.assign(form, response.form);

  setTitle("User - add");
});

const getForm = async () => {
  return await ApiClient.request("account/users/add/form", {});
};

const submitInternal = async (onlyValidate) => {
  return await ApiClient.validate("account/users/add", onlyValidate, form);
};

const redirectAfterSucces = (response) => {
  router.push({ name: "panel:user:update", params: { id: response.id } });
};

const { isLoading, submit, validate, errors, isAllTouched, setTitle } = useAddUpdate(
  submitInternal,
  redirectAfterSucces
);
</script>
