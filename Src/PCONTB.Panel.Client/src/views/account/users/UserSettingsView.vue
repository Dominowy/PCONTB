<template>
  <div>
    <h2>Settings</h2>
    <b-card no-body>
      <b-tabs card>
        <b-tab title="Account">
          <user-account />
        </b-tab>
        <b-tab title="Password">
          <user-password />
        </b-tab>
      </b-tabs>
    </b-card>
  </div>
</template>

<script setup>
import { useView } from "@/composables/useView";

useView("Settings");
</script>
