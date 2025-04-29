<template>
  <div>
    <base-header :title="title" />
    <b-row>
      <b-card>
        <project-add-update />
      </b-card>
    </b-row>
  </div>
</template>

<script setup>
import { useView } from "@/composables/useView";
import { ref } from "vue";

const title = ref("Project Add");

useView(title.value);
</script>
