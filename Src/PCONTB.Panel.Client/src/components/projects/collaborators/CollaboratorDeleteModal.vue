<template>
  <base-modal :title="title" :isLoading="isLoading" @close="handleCloseModal">
    <template #body> Are you sure you want to delete collaborator? </template>
    <template #footer>
      <button type="button" class="btn btn-secondary" @click="handleCloseModal">Close</button>
      <b-button variant="danger" @click="handleDelete">Delete</b-button>
    </template>
  </base-modal>
</template>

<script setup>
import { useAddUpdate } from "@/composables/useAddUpdate";
import { onMounted, ref } from "vue";
import ApiClient from "@/services/ApiClient";
import { useRoute } from "vue-router";

const route = useRoute();

const props = defineProps({
  collaboratorId: String,
});

const emit = defineEmits(["close", "refresh"]);

const title = ref("Delete collaborator");

onMounted(async () => {
  isLoading.value = true;

  isLoading.value = false;
});

const handleDelete = async () => {
  isLoading.value = true;
  try {
    await ApiClient.request("projects/collaborators/delete", {
      id: props.collaboratorId,
      projectId: route.params.id,
    });
  } finally {
    isLoading.value = false;
    emit("refresh");
    emit("close");
  }
};

const handleCloseModal = () => {
  emit("close");
};

const { isLoading } = useAddUpdate();
</script>
