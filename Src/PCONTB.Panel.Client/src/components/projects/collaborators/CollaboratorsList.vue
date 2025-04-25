<template>
  <div>
    <b-card class="border-0">
      <b-list-group vertical="md">
        <template v-if="content">
          <template v-for="collaborator in content.collaborators" :key="collaborator.id">
            <b-list-group-item>
              {{ collaborator.user.email }}
              <button
                class="btn btn-link text-secondary p-0"
                @click="showUpdateModal(collaborator.id)"
              >
                <IMaterialSymbolsSettingsRounded style="font-size: 1.5rem" />
              </button>
            </b-list-group-item>
          </template>
        </template>
        <b-list-group-item class="d-flex justify-content-center">
          <b-button @click="showModal = true"> Add collaborator </b-button>
        </b-list-group-item>
      </b-list-group>
    </b-card>
  </div>
  <collaborator-add-update-modal
    v-if="showModal"
    :collaboratorId="collaboratorId"
    @close="handleCloseModal"
  />
</template>

<script setup>
import { useDisplay } from "@/composables/useDisplay";
import ApiClient from "@/services/ApiClient";
import { onMounted, ref } from "vue";
import { useRoute } from "vue-router";

const showModal = ref(false);
const collaboratorId = ref(null);
const route = useRoute();

onMounted(() => {
  loadData(onDataLoaded);
});

const onDataLoaded = async () => {
  return await ApiClient.request("projects/collaborators/get-all", { id: route.params.id });
};

const showUpdateModal = async (id) => {
  collaboratorId.value = id;
  showModal.value = true;
};

const handleCloseModal = async () => {
  collaboratorId.value = null;
  showModal.value = false;
};

const { content, loadData } = useDisplay();
</script>
