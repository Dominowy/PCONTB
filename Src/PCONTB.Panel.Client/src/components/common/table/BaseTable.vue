<template>
  <div>
    <input
      v-if="showGlobalFilter"
      v-model="globalFilter"
      @input="onFilterChange"
      placeholder="Szukaj..."
    />

    <table>
      <thead>
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
            <div v-if="col.filterable">
              <input
                type="text"
                v-model="columnFilters[col.key]"
                @input="onFilterChange"
                placeholder="Filtruj..."
              />
            </div>
          </th>
        </tr>
      </thead>

      <tbody>
        <tr v-for="item in data" :key="item[idKey]">
          <td v-for="col in columns" :key="col.key">
            <slot :name="`cell-${col.key}`" :item="item">{{ item[col.key] }}</slot>
          </td>
        </tr>
      </tbody>
    </table>

    <div class="pagination">
      <button :disabled="page === 1" @click="changePage(page - 1)">Poprzednia</button>
      <span>Strona {{ page }} z {{ totalPages }}</span>
      <button :disabled="page === totalPages" @click="changePage(page + 1)">Następna</button>
    </div>
  </div>
</template>

<script setup>
import { ref, reactive, watch, toRefs, computed } from "vue";

const props = defineProps({
  data: { type: Array, default: () => [] },
  totalItems: { type: Number, default: 0 },
  columns: { type: Array, required: true },
  idKey: { type: String, default: "id" },
  pageSize: { type: Number, default: 10 },
  initialPage: { type: Number, default: 1 },
  showGlobalFilter: { type: Boolean, default: true },
  initialSort: { type: Array, default: () => [] },
});

const emit = defineEmits(["update:page", "update:sort", "update:filters"]);

const { totalItems, pageSize, initialPage, initialSort, columns } = toRefs(props);

const page = ref(initialPage.value);
const sortings = ref([...initialSort.value]);

const globalFilter = ref("");
const columnFilters = reactive({});

columns.value.forEach((col) => {
  if (col.filterable) columnFilters[col.key] = "";
});

function changePage(newPage) {
  if (newPage < 1 || newPage > totalPages.value) return;
  page.value = newPage;
  emit("update:page", page.value);
}

function sortColumn(key, sortable) {
  if (!sortable) return;
  const index = sortings.value.findIndex((s) => s.key === key);
  if (index >= 0) {
    sortings.value[index].desc = !sortings.value[index].desc;
  } else {
    sortings.value.unshift({ key, desc: false });
  }
  sortings.value = sortings.value.slice(0, 3);

  emit("update:sort", sortings.value);
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
    emit("update:filters", {
      global: globalFilter.value,
      columns: { ...columnFilters },
    });
    page.value = 1;
    emit("update:page", page.value);
  }, 300);
}

const totalPages = computed(() => Math.ceil(totalItems.value / pageSize.value));

watch(columns, () => {
  for (const key in columnFilters) {
    columnFilters[key] = "";
  }
  globalFilter.value = "";
  sortings.value = [...initialSort.value];
  page.value = initialPage.value;
});
</script>

<style scoped>
table {
  width: 100%;
  border-collapse: collapse;
}
th,
td {
  border: 1px solid #ccc;
  padding: 6px 12px;
}
th {
  background: #fafafa;
}
th > div {
  margin-top: 4px;
}
.pagination {
  margin-top: 12px;
}
</style>
