<template>
  <div>
    <base-header title="Settings" />
    <b-card no-body>
      <b-tabs card>
        <b-tab title="Account">
          <user-account />
        </b-tab>
        <b-tab title="Password">
          <user-password />
        </b-tab>
        <b-tab title="Lock">
          <user-lock />
        </b-tab>
      </b-tabs>
    </b-card>
  </div>
</template>

<script setup>
import UserAccount from "./components/UserAccount.vue";
import UserPassword from "./components/UserPassword.vue";
import UserLock from "./components/UserLock.vue";

import { useView } from "@/composables/useView";

useView("Settings");
</script>
