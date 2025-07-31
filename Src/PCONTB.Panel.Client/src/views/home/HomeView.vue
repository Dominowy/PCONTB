<template>
  <div>
    <b-row v-if="content" class="mb-2">
      <h2>Most viewed</h2>
      <home-slider :content="content.mostViewed" />
      <h2 class="mt-2">Last created</h2>
      <home-slider :content="content.lastCreated" />
    </b-row>
  </div>
</template>

<script setup>
import HomeSlider from "./components/HomeSlider.vue";
import { useDisplay } from "@/composables/useDisplay";
import ApiClient from "@/services/ApiClient";
import { onMounted } from "vue";

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

const { content, loadData } = useDisplay("Projects");
</script>
