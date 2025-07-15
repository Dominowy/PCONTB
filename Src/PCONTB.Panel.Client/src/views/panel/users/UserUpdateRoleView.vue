<template>
  <div>
    <base-header :isLoading="isLoading" :title="title" />
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
            :disabled="true"
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
import { useRouter, useRoute } from "vue-router";

const router = useRouter();
const route = useRoute();

const form = reactive({});

const title = ref("Update user");

const availableRoles = ref([]);

onMounted(async () => {
  const response = await getForm();
  Object.assign(form, response.form);
  availableRoles.value = response.roles;

  if (form != null) {
    title.value = `Update - ${form.username}`;
  } else {
    title.value = "Not found";
  }

  setTitle(title.value);
});

const getForm = async () => {
  return await ApiClient.request("account/users/update-role/form", { id: route.params.id });
};

const submitInternal = async (onlyValidate) => {
  return await ApiClient.validate("account/users/update-role", onlyValidate, form);
};

const redirectAfterSucces = (response) => {
  router.push({ name: "apnel:user:update", params: { id: response.id } });
};

const { isLoading, submit, validate, errors, isAllTouched, setTitle } = useAddUpdate(
  submitInternal,
  redirectAfterSucces
);
</script>
