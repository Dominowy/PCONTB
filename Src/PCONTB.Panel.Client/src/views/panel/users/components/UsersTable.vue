<template>
  <div>
    <div class="d-flex justify-content-end">
      <b-button variant="primary" @click="goToAdd">Add</b-button>
    </div>
    <base-table
      :columns="columns"
      url="/api/account/users/table/get-data"
      :actions="[{ label: 'Display', action: () => router.push({ name: 'panel:user' }) }]"
    >
      <template #cell-userRoles="{ item }">
        {{ formatRoles(item.userRoles) }}
      </template>
    </base-table>
  </div>
</template>

<script setup>
import { useRouter } from "vue-router";

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
];

function formatRoles(userRoles) {
  if (!userRoles || !Array.isArray(userRoles)) return "";
  return userRoles.map((r) => r.name || r).join(", ");
}

const goToAdd = () => {
  router.push({ name: "panel:user:add" });
};
</script>
