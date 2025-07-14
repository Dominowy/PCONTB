<template>
  <div>
    <b-card>
      <b-card-title>Owner</b-card-title>
      <display-user :user="user" />
    </b-card>
    <b-card class="mt-2" v-if="collaborators.length > 0">
      <b-card-title>Collaborators</b-card-title>
      <template v-for="collaborator in collaborators" :key="collaborator.id">
        <div class="mt-2">
          <div class="d-flex justify-content-between align-items-center">
            {{ collaborator.user.username }}
            <button
              class="btn btn-link text-secondary p-0"
              @click="goToProfile(collaborator.user.id)"
            >
              <i-material-symbols-article-person style="font-size: 1.5rem" />
            </button>
          </div>
        </div>
      </template>
    </b-card>
  </div>
</template>

<script setup>
import { useRouter } from "vue-router";

const router = useRouter();

defineProps({
  user: Object,
  collaborators: Array,
});

const goToProfile = async (id) => {
  router.push({
    name: "account:users:profile",
    params: { id },
  });
};
</script>
