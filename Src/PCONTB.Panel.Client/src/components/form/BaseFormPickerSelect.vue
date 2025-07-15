<template>
  <base-form-field
    :id="id"
    :label="label"
    :errors="getFieldErrors(propertyName)"
    :isTouched="isTouched"
    :isAllTouched="isAllTouched"
  >
    <div class="flex gap-6">
      <div class="w-1/2">
        Available {{ placeholder }}:
        <ul class="border p-2 h-48 overflow-y-auto">
          <li
            v-for="item in availableItems"
            :key="item.id"
            @dblclick="assignItem(item)"
            class="cursor-pointer hover:bg-gray-100 p-1"
          >
            {{ toPascalCase(item.value) }}
          </li>
        </ul>
      </div>

      <div class="w-1/2">
        Assigned {{ placeholder }}:
        <ul class="border p-2 h-48 overflow-y-auto">
          <li
            v-for="item in selectedItems"
            :key="item.id"
            @dblclick="unassignItem(item)"
            class="cursor-pointer hover:bg-gray-100 p-1"
          >
            {{ toPascalCase(item.value) }}
          </li>
        </ul>
      </div>
    </div>
  </base-form-field>
</template>

<script setup>
import { ref, computed, watch } from "vue";

const props = defineProps({
  id: String,
  label: String,
  placeholder: String,
  modelValue: Array,
  errors: {
    type: Array,
    default: () => [],
  },
  isAllTouched: Boolean,
  property: String,
  disabled: Boolean,
  required: Boolean,
  options: {
    type: Array,
    default: () => [],
  },
});

const emit = defineEmits(["update:modelValue"]);

const propertyName = ref(props.property || props.label);
const isTouched = ref(false);

const selectedItems = ref([]);

const availableItems = computed(() =>
  props.options.filter((item) => !selectedItems.value.some((r) => r.id === item.id))
);

const assignItem = (item) => {
  if (!selectedItems.value.find((r) => r.id === item.id)) {
    selectedItems.value = [...selectedItems.value, item];
    emit(
      "update:modelValue",
      selectedItems.value.map((i) => i.value)
    );
  }
};

const unassignItem = (item) => {
  selectedItems.value = selectedItems.value.filter((r) => r.id !== item.id);
  emit(
    "update:modelValue",
    selectedItems.value.map((i) => i.value)
  );
};

const getFieldErrors = () => {
  const field = props.property || props.label;
  return props.errors.filter((m) => m.propertyName === field).map((m) => m.message);
};

function toPascalCase(str) {
  if (!str) return "";
  return str[0].toUpperCase() + str.slice(1);
}

watch(
  () => [props.modelValue, props.options],
  ([modelValue, options]) => {
    selectedItems.value = options.filter((opt) => modelValue.includes(opt.value));
  },
  { immediate: true }
);
</script>
