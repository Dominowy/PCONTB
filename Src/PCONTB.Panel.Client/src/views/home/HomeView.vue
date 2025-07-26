<template>
  <div>
    <b-row v-if="content" class="mb-2">
      <h2>Most viewed</h2>
      <b-col v-for="project in content.mostViewed.projects" :key="project.id" md="4" class="mt-2">
        <b-card>
          <b-card-title>
            <div class="d-flex justify-content-between align-items-center">
              <div>{{ project.name }}</div>
              <button class="btn btn-link text-secondary p-0" @click="goToProject(project.id)">
                <i-bi-arrow-right style="font-size: 1.5rem" />
              </button>
            </div>
          </b-card-title>
          <div class="d-flex mt-2">
            {{ project.category.name }}
          </div>
        </b-card>
      </b-col>
      <h2 class="mt-2">Last created</h2>
      <b-col v-for="project in content.lastCreated.projects" :key="project.id" md="4" class="mt-2">
        <b-card>
          <b-card-title>
            <div class="d-flex justify-content-between align-items-center">
              <div>{{ project.name }}</div>
              <button class="btn btn-link text-secondary p-0" @click="goToProject(project.id)">
                <i-bi-arrow-right style="font-size: 1.5rem" />
              </button>
            </div>
          </b-card-title>
          <div class="d-flex mt-2">
            {{ project.category.name }}
          </div>
        </b-card>
      </b-col>
    </b-row>
  </div>
</template>

<script setup>
import { useDisplay } from "@/composables/useDisplay";
import ApiClient from "@/services/ApiClient";
import { onMounted } from "vue";
import { useRouter } from "vue-router";

const router = useRouter();

onMounted(() => {
  loadData(onDataLoaded);
});

const onDataLoaded = async () => {
  const data = {
    mostViewed: await ApiClient.request("projects/projects/get-most-viewed", {}),
    lastCreated: await ApiClient.request("projects/projects/get-last-created", {}),
  };
  return data;
};

const goToProject = (id) => {
  router.push({ name: "projects:discover:display", params: { id: id } });
};

const { content, loadData } = useDisplay("Projects");
</script>
