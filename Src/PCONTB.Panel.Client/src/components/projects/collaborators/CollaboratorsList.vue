<template>
  <div>
    <b-row v-if="content"></b-row>
  </div>
</template>

<script setup>
import { useDisplayPage } from "@/composables/useDisplayPage";
import ApiClient from "@/services/ApiClient";
import { onMounted } from "vue";
import { useRoute } from "vue-router";

const route = useRoute();

onMounted(() => {
  loadData(onDataLoaded);
});

const onDataLoaded = async () => {
  return await ApiClient.request("projects/collaborators/get-all", { id: route.params.id });
};

const { content, loadData } = useDisplayPage();
</script>
