<template>
  <div>
    <div>Are you sure you want to lock your account?</div>
    <div class="text-danger">{{ errorMessage }}</div>
    <b-button variant="danger" @click="handleLockUnlock">Confirm</b-button>
  </div>
</template>

<script setup>
import { ref, onMounted } from "vue";
import ApiClient from "@/services/ApiClient";
import { useStore } from "@/store/index";
import { useRouter } from "vue-router";

const router = useRouter();
const store = useStore();

const errorMessage = ref("");
const isLoading = ref(false);

onMounted(async () => {});

const handleLockUnlock = async () => {
  isLoading.value = true;

  try {
    await ApiClient.request("account/users/lock", {
      id: store.user.id,
    });

    router.go(0);
  } catch (error) {
    errorMessage.value = error.message;
  } finally {
    isLoading.value = false;
  }
};
</script>
