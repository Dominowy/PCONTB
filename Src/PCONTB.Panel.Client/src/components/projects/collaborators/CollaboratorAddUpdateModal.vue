<template>
  <base-form v-if="form" :formData="form" @submit="submit" @validate="validate">
    <base-modal :title="title" :isLoading="isLoading" @close="handleCloseModal">
      <template #body>
        <base-form-input
          id="email"
          class="mt-2"
          v-model="form.email"
          label="Email"
          placeholder="Enter email"
          :errors="errors"
          :isAllTouched="isAllTouched"
          :disabled="props.collaboratorId != null"
        />
        <base-form-input
          id="manageProjectPermission"
          class="mt-2"
          v-model="form.manageProjectPermission"
          label="Manage Project Permission"
          type="checkbox"
          :errors="errors"
          :isAllTouched="isAllTouched"
        />
        <base-form-input
          id="manageCommunityPermission"
          class="mt-2"
          v-model="form.manageCommunityPermission"
          label="Manage Community Permission"
          type="checkbox"
          :errors="errors"
          :isAllTouched="isAllTouched"
        />
        <base-form-input
          id="manageFulfillmentPermission"
          class="mt-2"
          v-model="form.manageFulfillmentPermission"
          label="Manage Fulfillment Permission"
          placeholder="Enter email"
          type="checkbox"
          :errors="errors"
          :isAllTouched="isAllTouched"
        />
      </template>
      <template #footer>
        <button type="button" class="btn btn-secondary" @click="handleCloseModal">Close</button>
        <base-form-submit-button variant="danger" label="Save" />
      </template>
    </base-modal>
  </base-form>
</template>

<script setup>
import { useAddUpdate } from "@/composables/useAddUpdate";
import { reactive, onMounted, ref } from "vue";
import ApiClient from "@/services/ApiClient";
import { useRoute } from "vue-router";

const props = defineProps({
  collaboratorId: String,
});

const emit = defineEmits(["close", "refresh"]);

const route = useRoute();

const title = ref("Add collaborator");

const form = reactive({});

onMounted(async () => {
  if (props.collaboratorId) {
    title.value = "Update collaborator";
  }

  let response = await getForm();

  response.form.projectId = route.params.id;
  Object.assign(form, response.form);

  isLoading.value = false;
});

const getForm = async () => {
  isLoading.value = true;
  if (props.collaboratorId) {
    return await ApiClient.request("projects/collaborators/update/form", {
      id: props.collaboratorId,
    });
  }
  return await ApiClient.request("projects/collaborators/add/form", {});
};

const submitInternal = async (onlyValidate) => {
  if (props.collaboratorId) {
    return await ApiClient.validate("projects/collaborators/update", onlyValidate, form);
  }

  return await ApiClient.validate("projects/collaborators/add", onlyValidate, form);
};

const handleCloseModal = () => {
  emit("close");
};

const redirectAfterSucces = () => {
  emit("refresh");
  emit("close");
};

const { isLoading, submit, validate, errors, isAllTouched } = useAddUpdate(
  submitInternal,
  redirectAfterSucces
);
</script>
