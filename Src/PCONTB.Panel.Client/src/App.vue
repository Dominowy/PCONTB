<template>
  <div>
    <LoadingLayout v-if="store.loading" />
    <TransitionLayout v-if="store.transition" />
    <router-view :key="$route.fullPath" v-slot="{ Component }">
      <transition name="fade" mode="out-in">
        <component :is="Component" />
      </transition>
    </router-view>
  </div>
</template>

<script setup>
import { useStore } from "./store/index";
import LoadingLayout from "./layouts/LoadingLayout.vue";
import TransitionLayout from "./layouts/TransitionLayout.vue";

const store = useStore();
</script>
