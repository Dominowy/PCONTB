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
import { ref } from "vue";

const props = defineProps({
  collaborator: Object,
});

const emit = defineEmits(["close", "delete"]);

const isLoading = ref(false);
const title = ref("Delete collaborator");

const handleDelete = async () => {
  isLoading.value = true;
  emit("delete", props.collaborator.id);
  isLoading.value = false;
  emit("close");
};

const handleCloseModal = () => {
  emit("close");
};
</script>
