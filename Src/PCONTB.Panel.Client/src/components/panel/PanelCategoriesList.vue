<template>
  <div>
    <div class="d-flex justify-content-end">
      <b-button variant="primary" @click="goToAdd">Add</b-button>
    </div>
    <base-table :columns="columns" url="/api/categories/table/get-data" />
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
  router.push({ name: "projects:project:add" });
};
</script>
