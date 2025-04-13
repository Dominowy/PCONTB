<template>
  <b-navbar v-b-color-mode="'dark'" toggleable="lg" variant="dark">
    <b-navbar-brand to="/">PCONTB</b-navbar-brand>
    <b-navbar-toggle v-b-toggle.navbar-toggle-collapse />
    <b-collapse id="navbar-toggle-collapse" is-nav>
      <b-navbar-nav>
        <b-nav-item to="/">Home</b-nav-item>
        <b-nav-item to="/projects">Projects</b-nav-item>
      </b-navbar-nav>
      <b-navbar-nav class="ms-auto mb-2 mb-lg-0">
        <b-nav-item v-if="!store.isAuthenticated" to="/login">Login</b-nav-item>
        <b-nav-item-dropdown v-if="store.isAuthenticated" right>
          <template #button-content>
            <em>{{ store.user.username }}</em>
          </template>
          <b-dropdown-item to="/profile">Profile</b-dropdown-item>
          <b-dropdown-item @click="store.logout">Sign Out</b-dropdown-item>
        </b-nav-item-dropdown>
      </b-navbar-nav>
    </b-collapse>
  </b-navbar>
</template>

<style scoped>
.b-navbar-nav .b-nav-item:hover,
.b-navbar-nav .b-nav-item:focus {
  background-color: #6c757d !important;
  color: white !important;
}
</style>

<script setup>
import { useStore } from "@/store/index";

const store = useStore();
</script>
