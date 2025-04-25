<template>
  <div>
    <div>
      <h2>Projects</h2>
      <b-button @click="goToAdd">Add</b-button>
    </div>

    <b-row>
      <b-col md="12" class="mb-2">
        <b-card class="text-center">
          <b-card-title>
            <IClarityAvatarLine class="mb-2 fs-1" />
          </b-card-title>
        </b-card>
      </b-col>
    </b-row>
  </div>
</template>

<script setup>
import { useDisplay } from "@/composables/useDisplay";
import { useStore } from "@/store/index";
import ApiClient from "@/services/ApiClient";
import { onMounted } from "vue";
import { useRouter } from "vue-router";

const store = useStore();
const router = useRouter();

onMounted(() => {
  loadData(onDataLoaded);
});

const goToAdd = async () => {
  router.push({ name: "projects:add" });
};

const onDataLoaded = async () => {
  return await ApiClient.request("projects/projects/get-all-by-user-id", { id: store.user.id });
};

const { content, loadData } = useDisplay(`Projects - ${store.user.username}`);
</script>
