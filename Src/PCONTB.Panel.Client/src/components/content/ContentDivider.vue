<template>
  <h6 v-if="!readonly">{{ index + ". Divider" }}</h6>
  <hr class="my-4" />
</template>

<script setup>
defineProps({ content: Object, index: Number, readonly: { type: Boolean, default: false } });
defineEmits(["update"]);
</script>
