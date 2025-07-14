<template>
  <base-modal :title="title" :isLoading="isLoading" @close="handleCloseModal">
    <template #body>
      <base-form-input
        id="email"
        class="mt-2"
        v-model="form.email"
        label="Email"
        placeholder="Enter email"
        :disabled="props.collaborator != null"
      />
      <base-form-input
        id="manageProjectPermission"
        class="mt-2"
        v-model="form.manageProjectPermission"
        label="Manage Project Permission"
        type="checkbox"
      />
      <base-form-input
        id="manageCommunityPermission"
        class="mt-2"
        v-model="form.manageCommunityPermission"
        label="Manage Community Permission"
        type="checkbox"
      />
      <base-form-input
        id="manageFulfillmentPermission"
        class="mt-2"
        v-model="form.manageFulfillmentPermission"
        label="Manage Fulfillment Permission"
        placeholder="Enter email"
        type="checkbox"
      />
    </template>
    <template #footer>
      <div class="text-danger">{{ errorMessage }}</div>
      <button type="button" class="btn btn-secondary" @click="handleCloseModal">Close</button>
      <b-button variant="primary" @click="handleAddUpdate">Add</b-button>
    </template>
  </base-modal>
</template>

<script setup>
import { reactive, onMounted, ref } from "vue";
import ApiClient from "@/services/ApiClient";
import { useRoute } from "vue-router";
import { v4 as uuidv4 } from "uuid";

const route = useRoute();

const props = defineProps({
  collaborator: Object,
});

const emit = defineEmits(["addUpdate", "close"]);
const title = ref("Add collaborator");
const isLoading = ref(false);
const errorMessage = ref("");

const form = reactive({
  email: null,
  id: null,
  manageCommunityPermission: false,
  manageFulfillmentPermission: false,
  manageProjectPermission: false,
  projectId: route.params.id ?? null,
  userId: null,
});

onMounted(async () => {
  if (props.collaborator) {
    title.value = "Update collaborator";
    Object.assign(form, props.collaborator);
  }
});

const handleAddUpdate = async () => {
  isLoading.value = true;
  try {
    if (props.collaborator == null) {
      let response = await ApiClient.request("projects/projects/get-collaborator-user", {
        email: form.email,
      });

      if (response != null) {
        form.userId = response.user.id;
        form.user = response.user;
        form.id = uuidv4();
      }
    }

    emit("addUpdate", form);
    emit("close");
  } catch (error) {
    errorMessage.value = error.message;
  } finally {
    isLoading.value = false;
  }
};

const handleCloseModal = () => {
  emit("close");
};
</script>
