<template>
  <div>
    <base-header title="Project - Settings" />
    <b-tabs>
      <b-tab title="Update" class="border">
        <project-add-update />
      </b-tab>
      <b-tab title="Collaborators" class="border">
        <collaborators-list />
      </b-tab>
    </b-tabs>
  </div>
</template>

<script setup>
import { useView } from "@/composables/useView";

useView("Project - Settings");
</script>
