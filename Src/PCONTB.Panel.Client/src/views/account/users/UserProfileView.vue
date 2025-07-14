<template>
  <div>
    <base-header :title="title" />
    <div v-if="content">
      <b-row class="mb-2">
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
  </div>
</template>

<script setup>
import { useDisplay } from "@/composables/useDisplay";
import { useRoute } from "vue-router";
import ApiClient from "@/services/ApiClient";
import { onMounted, ref } from "vue";

const route = useRoute();

const title = ref("Profile");

onMounted(async () => {
  await loadData(onDataLoaded);

  if (content != null) {
    title.value = `Profile - ${content.value.user.username}`;
  } else {
    title.value = "Not found";
  }

  setTitle(title.value);
});

const onDataLoaded = async () => {
  return await ApiClient.request("account/users/get-by-id", { id: route.params.id });
};

const { content, loadData, setTitle } = useDisplay(title.value);
</script>
