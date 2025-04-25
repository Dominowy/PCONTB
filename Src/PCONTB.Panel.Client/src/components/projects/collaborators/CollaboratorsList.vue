<template>
  <div>
    <b-card class="border-0">
      <b-list-group vertical="md">
        <template v-if="content">
          <template v-for="collaborator in content.users" :key="collaborator.id">
            <b-list-group-item>
              {{ collaborator.email }}
              <button class="btn btn-link text-secondary p-0" @click="showModal(collaborator.id)">
                <IMaterialSymbolsSettingsRounded style="font-size: 1.5rem" />
              </button>
            </b-list-group-item>
          </template>
        </template>
        <b-list-group-item class="d-flex justify-content-center">
          <BButton @click="showModal = true"> Add collaborator </BButton>
        </b-list-group-item>
      </b-list-group>
    </b-card>
    <div>
      <div v-if="showModal" class="modal fade show" tabindex="-1" style="display: block">
        <div class="modal-dialog">
          <div class="modal-content">
            <div class="modal-header">
              <h5 class="modal-title">Add collaborator</h5>
              <button
                type="button"
                class="btn-close"
                @click="showModal = false"
                aria-label="Close"
              />
            </div>
            <div class="modal-body">
              <collaborator-add-update />
            </div>
          </div>
        </div>
      </div>
      <div v-if="showModal" class="modal-backdrop fade show"></div>
    </div>
  </div>
</template>

<script setup>
import { useDisplay } from "@/composables/useDisplay";
import ApiClient from "@/services/ApiClient";
import { onMounted, ref } from "vue";
import { useRoute } from "vue-router";

const showModal = ref(false);
const route = useRoute();

onMounted(() => {
  loadData(onDataLoaded);
});

const onDataLoaded = async () => {
  return await ApiClient.request("projects/collaborators/get-all", { id: route.params.id });
};

const { content, loadData } = useDisplay();
</script>
