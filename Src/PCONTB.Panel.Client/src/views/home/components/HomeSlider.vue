<template>
  <div class="d-flex flex-nowrap overflow-auto gap-3 py-2" ref="scroller">
    <b-card
      v-for="project in content.projects"
      :key="project.id"
      class="flex-shrink-0"
      style="width: 20rem"
    >
      <project-card-display :project="project" />
    </b-card>
  </div>
</template>

<script setup>
defineProps({
  content: Object,
});
</script>
