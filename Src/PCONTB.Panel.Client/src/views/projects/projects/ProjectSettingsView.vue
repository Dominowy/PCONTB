<template>
  <div>
    <base-header title="Project - Settings" />
    <b-card no-body>
      <b-tabs card>
        <b-tab title="Update">
          <project-add-update />
        </b-tab>
        <b-tab title="Collaborators">
          <collaborators-list />
        </b-tab>
      </b-tabs>
    </b-card>
  </div>
</template>

<script setup>
import { useView } from "@/composables/useView";

useView("Project - Settings");
</script>
