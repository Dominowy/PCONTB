<template>
  <div v-if="content">
    <base-header :title="content.project.name" />
    <b-card>
      <div class="d-flex w-75">
        <img
          v-if="content.project.image"
          class="w-100"
          :src="`data:${content.project.image.contentType};base64,${content.project.imageData}`"
        />
        <video
          v-if="content.project.video"
          class="w-100"
          :src="`data:${content.project.video.contentType};base64,${content.project.videoData}`"
          controls
        ></video>
      </div>
      <div class="flex-grow-1"></div>
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
        <b-tab title="Creator" style="height: 100vh">
          <project-creator-display
            :user="content.project.user"
            :collaborators="content.project.collaborators"
          />
        </b-tab>
      </b-tabs>
    </b-card>
  </div>
</template>

<script setup>
import ProjectCreatorDisplay from "./components/ProjectCreatorDisplay.vue";
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

function scrollTo(content) {
  const id = `title${content.order}`;
  const el = document.getElementById(id);
  if (el) {
    el.scrollIntoView({ behavior: "smooth", block: "start" });
  }
}

const { content, loadData } = useDisplay(`Projects`);
</script>
