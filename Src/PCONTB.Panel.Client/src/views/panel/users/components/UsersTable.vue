<template>
  <div>
    <div class="d-flex justify-content-end">
      <b-button variant="primary" @click="goToAdd">Add</b-button>
    </div>
    <base-table
      :columns="columns"
      url="/api/account/users/table/get-data"
      :actions="[
        {
          label: 'Update',
          action: (row) => router.push({ name: 'panel:user:update', params: { id: row.id } }),
        },
        {
          label: 'Lock',
          action: (row) => handleShowModal(row, true),
          show: (row) => row.enabled,
        },
        {
          label: 'Unlock',
          action: (row) => handleShowModal(row, false),
          show: (row) => !row.enabled,
        },
      ]"
    >
      <template #cell-userRoles="{ item }">
        {{ formatRoles(item.userRoles) }}
      </template>
    </base-table>
    <user-enabled-modal v-if="modalVisible" :user="user" :toLock="lock" @close="handleCloseModal" />
  </div>
</template>

<script setup>
import UserEnabledModal from "./UserEnabledModal.vue";
import { useRouter } from "vue-router";
import { ref } from "vue";

const router = useRouter();

const columns = [
  {
    key: "username",
    accessor: "Username",
    label: "Username",
    filterable: true,
    sortable: true,
  },
  {
    key: "email",
    accessor: "Email",
    label: "Email",
    filterable: true,
    sortable: true,
  },
  {
    key: "userRoles",
    accessor: "UserRoles.Role",
    label: "Roles",
    filterable: true,
    sortable: true,
  },
  {
    key: "enabled",
    accessor: "Enabled",
    label: "Enabled",
    filterable: true,
    sortable: true,
  },
];

const lock = ref();
const user = ref();
const modalVisible = ref(false);

function formatRoles(userRoles) {
  if (!userRoles || !Array.isArray(userRoles)) return "";
  return userRoles.map((r) => r.name || r).join(", ");
}

const handleShowModal = async (item, toLock) => {
  lock.value = toLock;
  user.value = item;
  modalVisible.value = true;
};

const handleCloseModal = async () => {
  modalVisible.value = false;
  user.value = null;

  router.go(0);
};

const goToAdd = () => {
  router.push({ name: "panel:user:add" });
};
</script>
