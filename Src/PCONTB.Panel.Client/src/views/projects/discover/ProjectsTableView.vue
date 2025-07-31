<template>
  <div>
    <base-header title="Discover" />
    <b-row v-if="content" class="mb-2">
      <b-col v-for="project in content.projects" :key="project.id" md="4" class="mt-2">
        <b-card>
          <project-card-display :project="project" />
        </b-card>
      </b-col>
    </b-row>
  </div>
</template>

<script setup>
import { useDisplay } from "@/composables/useDisplay";
import ApiClient from "@/services/ApiClient";
import { onMounted } from "vue";

onMounted(() => {
  loadData(onDataLoaded);
});

const onDataLoaded = async () => {
  return await ApiClient.request("projects/projects/get-all", {});
};

const { content, loadData } = useDisplay("Discover");
</script>
