<template>
  <div class="container mt-4">
    <div v-if="showGlobalFilter" class="mb-3">
      <input
        v-model="globalFilter"
        @input="onFilterChange"
        placeholder="Szukaj..."
        class="form-control"
      />
    </div>

    <div class="table-responsive overflow-auto m-2">
      <table class="table table-bordered table-hover">
        <thead class="thead-light">
          <tr>
            <th
              v-for="col in columns"
              :key="col.key"
              @click="sortColumn(col.key, col.sortable)"
              style="cursor: pointer; user-select: none"
            >
              {{ col.label }}
              <span v-if="col.sortable && isSorted(col.key)" @click="sortColumn(col.key)">
                {{ getSortDirection(col.key) === "desc" ? "↓" : "↑" }}
              </span>
              <div v-if="col.filterable" class="mt-1">
                <input
                  type="text"
                  v-model="columnFilters[col.key]"
                  @input="onFilterChange"
                  placeholder="Filtruj..."
                  class="form-control form-control-sm"
                />
              </div>
            </th>
            <th v-if="actions.length > 0"></th>
          </tr>
        </thead>

        <tbody>
          <tr v-for="item in data" :key="item[idKey]">
            <td v-for="col in columns" :key="col.key">
              <slot :name="`cell-${col.key}`" :item="item">{{ item[col.key] }}</slot>
            </td>
            <td v-if="actions.length > 0">
              <div class="dropdown">
                <button
                  class="btn btn-secondary dropdown-toggle"
                  type="button"
                  id="globalActionDropdown"
                  data-bs-toggle="dropdown"
                  aria-expanded="false"
                ></button>
                <ul class="dropdown-menu dropdown-menu-end" aria-labelledby="globalActionDropdown">
                  <li v-for="(action, index) in actions" :key="index">
                    <a class="dropdown-item" href="#" @click.prevent="action.action()">
                      {{ action.label }}
                    </a>
                  </li>
                </ul>
              </div>
            </td>
          </tr>
        </tbody>
      </table>
    </div>
    <nav class="d-flex justify-content-between align-items-center">
      <button class="btn btn-primary" :disabled="page === 1" @click="changePage(page - 1)">
        Previous
      </button>
      <span>Page {{ page }} of {{ totalPages }}</span>
      <button class="btn btn-primary" :disabled="page === totalPages" @click="changePage(page + 1)">
        Next
      </button>
    </nav>
  </div>
</template>

<script setup>
import { ref, reactive, watch, computed } from "vue";
import axios from "axios";
import { debounce } from "lodash";

const props = defineProps({
  columns: { type: Array, required: true },
  url: { type: String, required: true },
  idKey: { type: String, default: "id" },
  pageSize: { type: Number, default: 10 },
  initialPage: { type: Number, default: 1 },
  initialSort: { type: Array, default: () => [] },
  showGlobalFilter: { type: Boolean, default: true },
  actions: {
    type: Array,
    default: () => [],
  },
});

const page = ref(props.initialPage);
const totalItems = ref(0);
const data = ref([]);
const sortings = ref([...props.initialSort]);

const globalFilter = ref("");
const columnFilters = reactive({});
props.columns.forEach((col) => {
  if (col.filterable) columnFilters[col.key] = "";
});

const totalPages = computed(() => Math.ceil(totalItems.value / props.pageSize));

watch(
  [page, sortings, globalFilter, () => JSON.stringify(columnFilters)],
  debounce(fetchData, 400),
  { immediate: true }
);

async function fetchData() {
  const params = {
    search: globalFilter.value || undefined,
    page: page.value,
    pageSize: props.pageSize,
    filters: {},
    sorts: [],
  };

  for (const key in columnFilters) {
    if (columnFilters[key]) {
      const col = props.columns.find((c) => c.key === key);
      if (col?.accessor) {
        params.filters[col.accessor] = columnFilters[key];
      }
    }
  }

  if (sortings.value.length > 0) {
    params.sorts = sortings.value.map((s) => {
      const col = props.columns.find((c) => c.key === s.key);
      return {
        field: col?.accessor ?? s.key,
        descending: s.desc,
      };
    });
  }

  try {
    const res = await axios.post(props.url, params);
    data.value = res.data.items;
    totalItems.value = res.data.totalCount;
  } catch (err) {
    console.error("Błąd pobierania danych:", err);
  }
}

function changePage(newPage) {
  if (newPage < 1 || newPage > totalPages.value) return;
  page.value = newPage;
}

function sortColumn(key, sortable) {
  if (!sortable) return;
  const idx = sortings.value.findIndex((s) => s.key === key);
  if (idx >= 0) {
    sortings.value[idx].desc = !sortings.value[idx].desc;
  } else {
    sortings.value.unshift({ key, desc: false });
  }
  sortings.value = sortings.value.slice(0, 3);
}

function isSorted(key) {
  return sortings.value.some((s) => s.key === key);
}

function getSortDirection(key) {
  const s = sortings.value.find((s) => s.key === key);
  return s ? (s.desc ? "desc" : "asc") : null;
}

let filterTimeout = null;
function onFilterChange() {
  clearTimeout(filterTimeout);
  filterTimeout = setTimeout(() => {
    page.value = 1;
  }, 300);
}
</script>
