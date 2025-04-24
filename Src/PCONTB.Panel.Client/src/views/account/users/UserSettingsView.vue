<template>
  <div>
    <h2>Settings</h2>
    <b-tabs>
      <b-tab title="Account" class="border">
        <user-account />
      </b-tab>
      <b-tab title="Password" class="border">
        <user-password />
      </b-tab>
    </b-tabs>
  </div>
</template>

<script setup>
import { useView } from "@/composables/useView";

useView("Settings");
</script>
