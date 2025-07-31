<template>
  <b-card-title>
    <div class="d-flex justify-content-between align-items-center">
      <div>{{ project.name }}</div>
      <button class="btn btn-link text-secondary p-0" @click="goToProject(project.id)">
        <i-bi-arrow-right style="font-size: 1.5rem" />
      </button>
    </div>
    <img
      v-if="project.image != null"
      class="w-100"
      :src="`data:${project.image.contentType};base64,${project.imageData}`"
      style="height: 160px; object-fit: cover"
    />
  </b-card-title>
  <div class="d-flex mt-2">
    {{ project.category.name }}
  </div>
</template>

<script setup>
import { useRouter } from "vue-router";

const router = useRouter();
defineProps({
  project: Object,
});

const goToProject = (id) => {
  router.push({ name: "projects:discover:display", params: { id: id } });
};
</script>
