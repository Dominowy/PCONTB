<template>
  <div class="page-content">
    <LoadingLayout v-if="store.loading" />
    <TransitionLayout v-if="store.transition" />
    <router-view v-slot="{ Component }">
      <transition name="fade" mode="out-in">
        <component :is="Component" />
      </transition>
    </router-view>
  </div>
</template>

<style scoped>
.page-content {
  padding-bottom: 60px;
}
</style>

<script setup>
import { useStore } from "./store/index";
import LoadingLayout from "./layouts/LoadingLayout.vue";
import TransitionLayout from "./layouts/TransitionLayout.vue";

const store = useStore();
</script>
