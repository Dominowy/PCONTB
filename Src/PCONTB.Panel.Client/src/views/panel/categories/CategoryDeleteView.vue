<template>
  <div>
    <base-header :isLoading="isLoading" :title="title" />
    <div>
      <b-card card>
        <base-form v-if="form" :formData="form" @submit="submit" @validate="validate">
          <div>Are you sure you want to delete category?</div>
          <div class="text-danger">{{ getFieldError("Id") }}</div>
          <base-form-submit-panel label="Delete" variant="danger" :isLoading="isLoading" />
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

const title = ref("Delete category");

onMounted(async () => {
  title.value = `Delete - ${route.query.name} `;

  setTitle(title.value);
});

const submitInternal = async (onlyValidate) => {
  return await ApiClient.validate("categories/delete", onlyValidate, {
    id: route.params.id,
  });
};

const redirectAfterSucces = () => {
  router.push({ name: "panel:admin" });
};

const getFieldError = (propertyName) => {
  const error = errors.value.find((e) => e.propertyName === propertyName);
  return error?.message || null;
};

const { isLoading, submit, validate, errors, setTitle } = useAddUpdate(
  submitInternal,
  redirectAfterSucces
);
</script>
