<template>
  <div v-if="content">
    <base-header :title="content.project.name" />
    <b-card> </b-card>
    <b-card no-body>
      <b-tabs card>
        <b-tab title="Campaign"> </b-tab>
        <b-tab title="Creator">
          <discover-display-creator
            :user="content.project.user"
            :collaborators="content.project.collaborators"
          />
        </b-tab>
      </b-tabs>
    </b-card>
  </div>
</template>

<script setup>
import { useDisplay } from "@/composables/useDisplay";
import ApiClient from "@/services/ApiClient";
import { onMounted } from "vue";
import { useRoute } from "vue-router";

const route = useRoute();

onMounted(() => {
  loadData(onDataLoaded);
});

const onDataLoaded = async () => {
  return await ApiClient.request("projects/projects/get-by-id", { id: route.params.id });
};

const { content, loadData } = useDisplay(`Projects`);
</script>
