<template>
  <div v-if="content">
    <h2>Profile</h2>
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
import { useStore } from "@/store/index";
import ApiClient from "@/services/ApiClient";
import { onMounted } from "vue";

const store = useStore();

onMounted(() => {
  loadData(onDataLoaded);
});

const onDataLoaded = async () => {
  return await ApiClient.request("account/users/get-by-id", { id: store.user.id });
};

const { content, loadData } = useDisplay(`Profile - ${store.user.username}`);
</script>
