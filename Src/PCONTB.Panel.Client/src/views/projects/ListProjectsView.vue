<template>
  <div>
    <div class="d-flex justify-content-between">
      <h2>Projects</h2>
      <b-button @click="goToAdd">Add</b-button>
    </div>
    <b-row v-if="content">
      <b-col v-for="project in content.projects" :key="project.id" md="4" class="mt-2">
        <b-card>
          <b-card-title>
            <div class="d-flex justify-content-between align-items-center">
              <div>{{ project.name }}</div>
              <button class="btn btn-link text-secondary p-0" @click="goToSetting(project.id)">
                <IMaterialSymbolsSettingsRounded style="font-size: 1.5rem" />
              </button>
            </div>
          </b-card-title>
          <div class="d-flex mt-2">
            {{ project.category.name }}
            <div v-if="project.subcategory" class="ms-2">{{ "  " + project.subcategory.name }}</div>
          </div>
        </b-card>
      </b-col>
    </b-row>
  </div>
</template>

<script setup>
import { useDisplayPage } from "@/composables/useDisplayPage";
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

const goToSetting = (id) => {
  router.push({ name: "projects:update", params: { id: id } });
};

const { content, loadData } = useDisplayPage(`Projects - ${store.user.username}`);
</script>
