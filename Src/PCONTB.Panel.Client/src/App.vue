<template>
  <div>
    <LoadingLayout v-if="store.loading" />
    <router-view v-slot="{ Component }">
      <transition name="fade" mode="out-in">
        <component :is="Component" />
      </transition>
    </router-view>
  </div>
</template>

<script setup>
import { useStore } from "./store/index";
import LoadingLayout from "./layouts/LoadingLayout.vue";

const store = useStore();
</script>
