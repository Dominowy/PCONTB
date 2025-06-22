<template>
  <div>
    <base-header title="Admin - Panel" />
    <b-row>
      <div>
        <b-card no-body>
          <b-tabs card>
            <b-tab title="Users">
              <panel-users-list />
            </b-tab>
            <b-tab title="Categories"> </b-tab>
            <b-tab title="Countries"> </b-tab>
          </b-tabs>
        </b-card>
      </div>
    </b-row>
  </div>
</template>

<script setup>
import { useView } from "@/composables/useView";

useView("Admin - Panel");
</script>
