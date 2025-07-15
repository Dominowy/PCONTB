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
            <b-row>
              <b-col v-for="project in content.user.projects" :key="project.id" md="4" class="mt-2">
                <b-card>
                  <b-card-title>
                    <div class="d-flex justify-content-between align-items-center">
                      <div>{{ project.name }}</div>
                      <button
                        class="btn btn-link text-secondary p-0"
                        @click="goToProject(project.id)"
                      >
                        <i-bi-arrow-right style="font-size: 1.5rem" />
                      </button>
                    </div>
                  </b-card-title>
                  <div class="d-flex mt-2">
                    {{ project.category.name }}
                  </div>
                </b-card>
              </b-col>
            </b-row>
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
import { useRouter } from "vue-router";

const router = useRouter();
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

const goToProject = (id) => {
  router.push({ name: "projects:discover:display", params: { id: id } });
};

const { content, loadData, setTitle } = useDisplay(title.value);
</script>
