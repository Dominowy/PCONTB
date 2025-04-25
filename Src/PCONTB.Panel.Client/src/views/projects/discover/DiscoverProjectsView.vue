<template>
  <div>
    <div class="d-flex justify-content-between">
      <h2>Discover</h2>
    </div>
    <b-row v-if="content">
      <b-col v-for="project in content.projects" :key="project.id" md="4" class="mt-2">
        <b-card>
          <b-card-title>
            <div class="d-flex justify-content-between align-items-center">
              <div>{{ project.name }}</div>
              <button class="btn btn-link text-secondary p-0" @click="goToProject(project.id)">
                <IBiArrowRight style="font-size: 1.5rem" />
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
  router.push({ name: "discover:display", params: { id: id } });
};

const { content, loadData } = useDisplay("Discover");
</script>
