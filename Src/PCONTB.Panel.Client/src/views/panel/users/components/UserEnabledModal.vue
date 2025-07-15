<template>
  <base-modal :title="title" :isLoading="isLoading" @close="handleCloseModal">
    <template v-if="toLock" #body> Are you sure you want to lock {{ user.username }}? </template>
    <template v-else #body> Are you sure you want to unlock {{ user.username }}? </template>
    <template #footer>
      <div class="text-danger">{{ errorMessage }}</div>
      <button type="button" class="btn btn-secondary" @click="handleCloseModal">Close</button>
      <b-button variant="danger" @click="handleLockUnlock">Confirm</b-button>
    </template>
  </base-modal>
</template>

<script setup>
import { ref, onMounted } from "vue";
import ApiClient from "@/services/ApiClient";

const props = defineProps({
  user: Object,
  toLock: Boolean,
});

const emit = defineEmits(["close"]);

const errorMessage = ref("");
const isLoading = ref(false);
const title = ref("Lock collaborator");

onMounted(async () => {
  if (props.toLock) {
    title.value = `Lock - ${props.user.username}`;
  } else {
    title.value = `Unlock - ${props.user.username}`;
  }
});

const handleLockUnlock = async () => {
  isLoading.value = true;

  try {
    if (props.toLock) {
      await ApiClient.request("account/users/lock", {
        id: props.user.id,
      });
    } else {
      await ApiClient.request("account/users/unlock", {
        id: props.user.id,
      });
    }

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
