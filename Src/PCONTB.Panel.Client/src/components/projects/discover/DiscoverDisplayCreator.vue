<template>
  <div>
    <b-card>
      <b-card-title>Owner</b-card-title>
      <display-user :user="user" />
    </b-card>
    <b-card class="mt-2" v-if="collaborators.length > 0">
      <b-card-title>Collaborators</b-card-title>
      <template v-for="collaborator in collaborators" :key="collaborator.id">
        <display-user :user="collaborator.user" />
      </template>
    </b-card>
  </div>
</template>

<script setup>
defineProps({
  user: Object,
  collaborators: Array,
});
</script>
