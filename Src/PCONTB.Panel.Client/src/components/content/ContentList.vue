<template>
  <div v-if="!readonly">
    <h6>{{ index + ". List" }}</h6>
    <b-list-group>
      <b-list-group-item
        v-for="(item, idx) in content.data.items"
        :key="item.order"
        class="d-flex align-items-center"
      >
        <input
          class="form-control me-2"
          :value="item.text"
          :placeholder="`Item ${idx + 1}`"
          @input="updateItem(idx, $event.target.value)"
        />
        <button class="btn btn-sm btn-outline-danger" type="button" @click="removeItem(idx)">
          <i-material-symbols-delete />
        </button>
      </b-list-group-item>

      <b-list-group-item class="d-flex justify-content-end">
        <button type="button" class="btn btn-link text-secondary p-0" @click="addItem">
          <i-material-symbols-add style="font-size: 1.5rem" />
        </button>
      </b-list-group-item>
    </b-list-group>
  </div>
  <div v-else>
    <div>
      <b-list-group>
        <b-list-group-item
          class="border-0 px-0 py-1"
          v-for="(item, idx) in content.data.items"
          :key="idx"
        >
          {{ idx + 1 }}. {{ item.text }}
        </b-list-group-item>
      </b-list-group>
    </div>
  </div>
</template>

<script setup>
const props = defineProps({
  content: Object,
  index: Number,
  readonly: { type: Boolean, default: false },
});
const emit = defineEmits(["update"]);

function updateOrders(items) {
  return items.map((item, idx) => ({
    ...item,
    order: idx + 1,
  }));
}

function addItem() {
  const items = [...(props.content.data.items || [])];
  items.push({ order: items.length + 1, text: "" });
  emit("update", { ...props.content, data: { ...props.content.data, items } });
}

function removeItem(index) {
  let items = [...(props.content.data.items || [])];
  items.splice(index, 1);
  items = updateOrders(items);
  emit("update", { ...props.content, data: { ...props.content.data, items } });
}

function updateItem(index, text) {
  const items = [...(props.content.data.items || [])];
  items[index] = { ...items[index], text };
  emit("update", { ...props.content, data: { ...props.content.data, items } });
}
</script>
