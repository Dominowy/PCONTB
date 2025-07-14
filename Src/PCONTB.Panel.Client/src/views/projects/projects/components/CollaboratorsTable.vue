<template>
  <div>
    <b-list-group vertical="md">
      <template v-if="items">
        <template v-for="(collaborator, index) in items" :key="collaborator.id">
          <b-list-group-item>
            <div class="d-flex justify-content-between">
              <div class="d-flex align-items-center">
                {{ collaborator.email }}
              </div>
              <div>
                <button
                  type="button"
                  class="btn btn-link text-secondary p-0"
                  @click="handleShowUpdateModal(collaborator)"
                >
                  <i-material-symbols-settings-rounded style="font-size: 1.5rem" />
                </button>
                <button
                  type="button"
                  class="btn btn-link text-secondary p-0"
                  @click="handleShowDeleteModal(collaborator)"
                >
                  <i-material-symbols-person-remove style="font-size: 1.5rem" />
                </button>
                <button
                  type="button"
                  class="btn btn-link text-secondary p-0"
                  @click="goToProfile(collaborator.user.id)"
                >
                  <i-material-symbols-article-person style="font-size: 1.5rem" />
                </button>
              </div>
            </div>
            <div>
              <small v-if="getErrorForIndex(index)" class="text-danger">
                {{ getErrorForIndex(index) }}
              </small>
            </div>
          </b-list-group-item>
        </template>
      </template>
      <b-list-group-item class="d-flex justify-content-end">
        <button type="button" class="btn btn-link text-secondary p-0" @click="handleShowAddModal">
          <i-material-symbols-person-add style="font-size: 1.5rem" />
        </button>
      </b-list-group-item>
    </b-list-group>
  </div>
  <collaborator-add-update-modal
    v-if="showModal"
    :collaborator="collaborator"
    @close="handleCloseModal"
    @addUpdate="handleAddUpdate"
  />
  <collaborator-delete-modal
    v-if="showDeleteModal"
    :collaborator="collaborator"
    @close="handleCloseModal"
    @delete="handleDelete"
  />
</template>

<script setup>
import { ref } from "vue";
import { useRouter } from "vue-router";

const props = defineProps({
  items: Array,
  errors: {
    type: Array,
    default: () => [],
  },
  isAllTouched: {
    type: Boolean,
    default: false,
  },
});

const showModal = ref(false);
const showDeleteModal = ref(false);
const collaborator = ref(null);
const router = useRouter();

const emit = defineEmits(["addUpdate", "delete"]);

const handleShowAddModal = async () => {
  collaborator.value = null;
  showModal.value = true;
};

const handleShowUpdateModal = async (item) => {
  collaborator.value = item;
  showModal.value = true;
};

const handleAddUpdate = (collaborator) => {
  emit("addUpdate", collaborator);
};

const handleShowDeleteModal = async (id) => {
  collaborator.value = id;
  showDeleteModal.value = true;
};

const handleDelete = (collaboratorId) => {
  emit("delete", collaboratorId);
};

const handleCloseModal = async () => {
  collaborator.value = null;
  showModal.value = false;
  showDeleteModal.value = false;
};

const getErrorForIndex = (index) => {
  if (!props.errors || props.errors.length === 0) return null;

  const error = props.errors.find((e) => {
    return e.propertyName === `Collaborators[${index}].Email`;
  });

  return error ? error.message : null;
};

const goToProfile = async (id) => {
  router.push({
    name: "account:users:profile",
    params: { id },
  });
};
</script>
