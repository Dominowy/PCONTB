<template>
  <div>
    <div class="d-flex justify-content-between align-items-center">
      {{ user.username }}
      <button class="btn btn-link text-secondary p-0" @click="goToProfile(user.id, user.username)">
        <i-material-symbols-article-person style="font-size: 1.5rem" />
      </button>
    </div>
  </div>
</template>

<script setup>
import { useRouter } from "vue-router";

const router = useRouter();

defineProps({
  user: Object,
});

const goToProfile = async (id, username) => {
  router.push({
    name: "account:users:profile",
    params: { id },
    query: { username },
  });
};
</script>
