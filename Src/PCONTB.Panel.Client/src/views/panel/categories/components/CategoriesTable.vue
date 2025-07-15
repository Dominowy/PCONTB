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
];

const goToAdd = async () => {
  router.push({ name: "panel:category:add" });
};
</script>
