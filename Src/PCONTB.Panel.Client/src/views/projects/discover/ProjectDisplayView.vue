<template>
  <div v-if="content">
    <base-header :title="content.project.name" />
    <b-card>
      <div class="d-flex flex-row">
        <div class="d-flex w-75 position-relative me-3">
          <div
            v-if="showThumbnail && content.project.image"
            class="position-relative w-100"
            @click="playVideo"
          >
            <img
              class="w-100"
              :src="`data:${content.project.image.contentType};base64,${content.project.imageData}`"
            />
            <div
              class="position-absolute top-50 start-50 translate-middle"
              style="font-size: 64px; color: white; text-shadow: 0 0 8px black"
            >
              <i-material-symbols-play-arrow-rounded style="font-size: 64px; color: white" />
            </div>
          </div>
          <video
            v-if="content.project.video && !showThumbnail"
            class="w-100"
            :src="`data:${content.project.video.contentType};base64,${content.project.videoData}`"
            controls
            autoplay
          />
        </div>
        <div class="flex-grow-1">
          <project-campaign-goal :projectId="route.params.id" />
        </div>
      </div>
    </b-card>
    <b-card no-body>
      <b-tabs card>
        <b-tab title="Campaign">
          <div class="d-flex">
            <div class="nav flex-column me-3 w-25">
              <div v-for="content in content.project.campaign.contents" :key="content.id">
                <a
                  v-if="content.type == 'title'"
                  href="javascript:void(0);"
                  class="nav-link text-center"
                  @click="scrollTo(content)"
                >
                  {{ content.data.title }}
                </a>
              </div>
            </div>
            <div class="flex-grow-1 overflow-auto" style="height: 100vh">
              <div v-for="(content, index) in content.project.campaign.contents" :key="content.id">
                <project-content :content="content" :index="index + 1" :readonly="true" />
              </div>
            </div>
          </div>
        </b-tab>
        <b-tab title="Creator">
          <project-creator-display
            :user="content.project.user"
            :collaborators="content.project.collaborators"
          />
        </b-tab>
        <b-tab title="Transactions" style="height: 50vh">
          <project-campaing-transactions :projectId="route.params.id" />
        </b-tab>
        <b-tab title="Community" style="height: 50vh">
          <discover-community />
        </b-tab>
      </b-tabs>
    </b-card>
  </div>
</template>

<script setup>
import ProjectCreatorDisplay from "./components/ProjectCreatorDisplay.vue";
import ProjectCampaingTransactions from "./components/ProjectCampaingTransactions.vue";
import ProjectCampaignGoal from "./components/ProjectCampaignGoal.vue";
import DiscoverCommunity from "./components/DiscoverCommunity.vue";
import { useDisplay } from "@/composables/useDisplay";
import ApiClient from "@/services/ApiClient";
import { onMounted, ref } from "vue";
import { useRoute } from "vue-router";

const route = useRoute();

const showThumbnail = ref(true);

onMounted(() => {
  loadData(onDataLoaded);
});

const onDataLoaded = async () => {
  return await ApiClient.request("projects/projects/get-by-id", { id: route.params.id });
};

function scrollTo(content) {
  const id = `title${content.order}`;
  const el = document.getElementById(id);
  if (el) {
    el.scrollIntoView({ behavior: "smooth", block: "start" });
  }
}

function playVideo() {
  showThumbnail.value = false;
}

const { content, loadData } = useDisplay(`Projects`);
</script>
