<template>
  <div>
    <div class="d-flex justify-content-end">
      <b-button variant="primary" @click="goToAdd">Add</b-button>
    </div>
    <base-table
      :data="users"
      :totalItems="totalCount"
      :columns="columns"
      :pageSize="pageSize"
      :initialPage="page"
      :initialSort="initialSort"
      @update:page="onPageChange"
      @update:sort="onSortChange"
      @update:filters="onFiltersChange"
    >
      <template #cell-userRoles="{ item }">
        {{ formatRoles(item.userRoles) }}
      </template>
    </base-table>
  </div>
</template>

<script setup>
import { ref, reactive, watch } from "vue";
import { debounce } from "lodash";
import axios from "axios";
import { useRouter } from "vue-router";

const router = useRouter();

const debouncedFetchUsers = debounce(fetchUsers, 400);

const columns = [
  {
    key: "username",
    accessor: "Username",
    label: "Username",
    filterable: true,
    sortable: true,
  },
  { key: "email", accessor: "Email", label: "Email", filterable: true, sortable: true },
  {
    key: "userRoles",
    accessor: "UserRoles.Role",
    label: "Roles",
    filterable: true,
    sortable: true,
  },
  { key: "action", label: "", filterable: false, sortable: false },
];

const users = ref([]);
const totalCount = ref(0);
const page = ref(1);
const pageSize = 10;
const initialSort = ref([]);

const filters = reactive({
  global: "",
  columns: {},
});

function formatRoles(userRoles) {
  if (!userRoles || !Array.isArray(userRoles)) return "";
  return userRoles.map((r) => r.name || r).join(", ");
}

async function fetchUsers() {
  const params = {
    search: filters.global || undefined,
    page: page.value,
    pageSize,
    filters: {},
  };

  for (const key in filters.columns) {
    if (filters.columns[key]) {
      const column = columns.find((col) => col.key === key);
      if (column) {
        // Użyj accessor zamiast key
        params.filters[column.accessor] = filters.columns[key];
      }
    }
  }

  if (initialSort.value.length > 0) {
    params.sorts = initialSort.value.map((s) => {
      const column = columns.find((col) => col.key === s.key);
      return {
        field: column?.accessor ?? s.key, // fallback dla bezpieczeństwa
        descending: s.desc,
      };
    });
  }

  try {
    console.log("Wysyłane dane:", params);
    const response = await axios.post("/api/account/users/table/get-data", params);
    users.value = response.data.items;
    totalCount.value = response.data.totalCount;
  } catch (err) {
    console.error("Błąd pobierania użytkowników:", err);
  }
}

function onPageChange(newPage) {
  page.value = newPage;
}

function onSortChange(newSort) {
  initialSort.value = newSort;
}

function onFiltersChange(newFilters) {
  filters.global = newFilters.global;
  filters.columns = { ...newFilters.columns };
  page.value = 1;
}

const goToAdd = async () => {
  router.push({ name: "projects:project:add" });
};

watch(
  [page, initialSort, () => filters.global, () => JSON.stringify(filters.columns)],
  () => {
    debouncedFetchUsers();
  },
  { immediate: true }
);
</script>
