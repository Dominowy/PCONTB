<template>
  <div>
    <div class="d-flex justify-content-end">
      <b-button variant="primary" @click="goToAdd">Add</b-button>
    </div>
    <base-table
      :columns="columns"
      url="/api/categories/table/get-data"
      :actions="[
        {
          label: 'Update',
          action: (row) => router.push({ name: 'panel:category:update', params: { id: row.id } }),
        },
        {
          label: 'Delete',
          action: (row) =>
            router.push({
              name: 'panel:category:delete',
              params: { id: row.id },
              query: { name: row.name },
            }),
        },
      ]"
    />
  </div>
</template>

<script setup>
import { useRouter } from "vue-router";

const router = useRouter();

const columns = [
  {
    key: "name",
    accessor: "Name",
    label: "Name",
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

const goToAdd = async () => {
  router.push({ name: "panel:category:add" });
};
</script>
