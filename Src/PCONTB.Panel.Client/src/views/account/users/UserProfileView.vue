<template>
  <div v-if="content">
    <base-header :title="`Profile-${content.user.username}`" />
    <b-row>
      <b-col md="12" class="mb-2">
        <b-card class="text-center">
          <b-card-title>
            <IClarityAvatarLine class="mb-2 fs-1" />
          </b-card-title>
          <b-card-text>{{ content.user.username }}</b-card-text>
          <b-card-text>{{ content.user.email }}</b-card-text>
        </b-card>
      </b-col>
      <b-col md="12" class="mb-2">
        <b-card>
          <b-tabs>
            <b-tab title="Created" class="border"> </b-tab>
          </b-tabs>
        </b-card>
      </b-col>
    </b-row>
  </div>
</template>

<script setup>
import { useDisplay } from "@/composables/useDisplay";
import { useRoute } from "vue-router";
import ApiClient from "@/services/ApiClient";
import { onMounted } from "vue";

const route = useRoute();

onMounted(async () => {
  await loadData(onDataLoaded);
  setTitle(`Profile - ${content.value.user.username}`);
});

const onDataLoaded = async () => {
  return await ApiClient.request("account/users/get-by-id", { id: route.params.id });
};

const { content, loadData, setTitle } = useDisplay("Profile");
</script>
