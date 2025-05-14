<template>
  <b-navbar v-b-color-mode="'dark'" toggleable="lg" variant="dark">
    <router-link to="/" class="navbar-brand" active-class="active">PCONTB</router-link>
    <b-navbar-toggle v-b-toggle.navbar-toggle-collapse />
    <b-collapse id="navbar-toggle-collapse" is-nav>
      <b-navbar-nav>
        <router-link to="/projects/discover" class="nav-link" active-class="active">
          Discover
        </router-link>
      </b-navbar-nav>
      <b-navbar-nav class="ms-auto mb-2 mb-lg-0">
        <b-nav-item v-if="!store.isAuthenticated" to="/auth/login">Login</b-nav-item>
        <b-nav-item-dropdown v-if="store.isAuthenticated" right>
          <template #button-content>
            <em>{{ store.user.username }}</em>
          </template>
          <b-dropdown-item @click="goToProfile">Profile</b-dropdown-item>
          <b-dropdown-item to="/account/user/settings">Settings</b-dropdown-item>
          <b-dropdown-item v-if="store.hasRole('user')" to="/projects"> Projects </b-dropdown-item>
          <b-dropdown-item v-if="store.hasRole('moderator')" to="/panel/moderation">
            Moderation
          </b-dropdown-item>
          <b-dropdown-item v-if="store.hasRole('admin')" to="/panel/admin">
            Admin Panel
          </b-dropdown-item>
          <b-dropdown-item @click="handleLogout">Sign Out</b-dropdown-item>
        </b-nav-item-dropdown>
      </b-navbar-nav>
    </b-collapse>
  </b-navbar>
</template>

<script setup>
import { useStore } from "@/store/index";
import { useRouter } from "vue-router";

const store = useStore();
const router = useRouter();

const handleLogout = async () => {
  await store.logout();

  router.push({ name: "home" });
};

const goToProfile = async () => {
  router.push({
    name: "account:users:profile",
    params: { id: store.user.id },
    query: { username: store.user.username },
  });
};
</script>
