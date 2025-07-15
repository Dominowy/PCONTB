<template>
  <div>
    <base-header title="Discover" />
    <b-row v-if="content" class="mb-2">
      <b-col v-for="project in content.projects" :key="project.id" md="4" class="mt-2">
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
  return await ApiClient.request("projects/projects/get-all", {});
};

const goToProject = (id) => {
  router.push({ name: "projects:discover:display", params: { id: id } });
};

const { content, loadData } = useDisplay("Discover");
</script>
