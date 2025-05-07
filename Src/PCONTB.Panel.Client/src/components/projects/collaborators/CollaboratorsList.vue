<template>
  <div>
    <b-list-group vertical="md">
      <template v-if="content">
        <template v-for="collaborator in content.collaborators" :key="collaborator.id">
          <b-list-group-item>
            <div class="d-flex justify-content-between">
              <div class="d-flex align-items-center">
                {{ collaborator.user.email }}
              </div>
              <div>
                <button
                  class="btn btn-link text-secondary p-0"
                  @click="handleShowUpdateModal(collaborator.id)"
                >
                  <i-material-symbols-settings-rounded style="font-size: 1.5rem" />
                </button>
                <button
                  class="btn btn-link text-secondary p-0"
                  @click="handleShowDeleteModal(collaborator.id)"
                >
                  <i-material-symbols-person-remove style="font-size: 1.5rem" />
                </button>
                <button
                  class="btn btn-link text-secondary p-0"
                  @click="goToProfile(collaborator.user.id, collaborator.user.username)"
                >
                  <i-material-symbols-article-person style="font-size: 1.5rem" />
                </button>
              </div>
            </div>
          </b-list-group-item>
        </template>
      </template>
      <b-list-group-item class="d-flex justify-content-end">
        <button class="btn btn-link text-secondary p-0" @click="handleShowAddModal">
          <i-material-symbols-person-add style="font-size: 1.5rem" />
        </button>
      </b-list-group-item>
    </b-list-group>
  </div>
  <collaborator-add-update-modal
    v-if="showModal"
    :collaboratorId="collaboratorId"
    @close="handleCloseModal"
    @refresh="refresh"
  />
  <collaborator-delete-modal
    v-if="showDeleteModal"
    :collaboratorId="collaboratorId"
    @close="handleCloseModal"
    @refresh="refresh"
  />
</template>

<script setup>
import { useDisplay } from "@/composables/useDisplay";
import ApiClient from "@/services/ApiClient";
import { onMounted, ref } from "vue";
import { useRoute, useRouter } from "vue-router";

const showModal = ref(false);
const showDeleteModal = ref(false);
const collaboratorId = ref(null);
const router = useRouter();
const route = useRoute();

onMounted(() => {
  loadData(onDataLoaded);
});

const onDataLoaded = async () => {
  return await ApiClient.request("projects/collaborators/get-all", { id: route.params.id });
};

const refresh = async () => {
  loadData(onDataLoaded);
};

const handleShowUpdateModal = async (id) => {
  collaboratorId.value = id;
  showModal.value = true;
};

const handleShowAddModal = async () => {
  collaboratorId.value = null;
  showModal.value = true;
};

const handleShowDeleteModal = async (id) => {
  collaboratorId.value = id;
  showDeleteModal.value = true;
};

const handleCloseModal = async () => {
  collaboratorId.value = null;
  showModal.value = false;
  showDeleteModal.value = false;
};

const goToProfile = async (id, username) => {
  router.push({
    name: "account:users:profile",
    params: { id },
    query: { username },
  });
};

const { content, loadData } = useDisplay();
</script>
