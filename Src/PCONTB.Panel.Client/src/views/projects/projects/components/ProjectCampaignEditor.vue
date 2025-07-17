<template>
  <div>
    <div v-for="(content, index) in campaign.contents" :key="content.id" class="mb-4">
      <project-content
        :content="content"
        :index="index + 1"
        @update="(updatedContent) => updateContent(index, updatedContent)"
        @remove="() => removeContent(index)"
        @move-up="() => moveUp(index)"
        @move-down="() => moveDown(index)"
      />
    </div>

    <div class="mb-3" v-if="!readonly">
      <label for="newType" class="form-label">Add content:</label>
      <select id="newType" class="form-select" v-model="newType">
        <option disabled value="">Select type</option>
        <option v-for="type in types" :key="type" :value="type.value">
          {{ toPascalCase(type.value) }}
        </option>
      </select>
    </div>

    <button class="btn btn-primary" @click="addContent" :disabled="!newType">Add Content</button>
  </div>
</template>

<script setup>
import { ref, computed } from "vue";
import { v4 as uuidv4 } from "uuid";

const props = defineProps({
  modelValue: Object,
  types: Array,
  readonly: { type: Boolean, default: false },
});
const emit = defineEmits(["update:modelValue"]);

const campaign = computed({
  get: () => props.modelValue || { contents: [] },
  set: (val) => emit("update:modelValue", val),
});

const newType = ref("");

function addContent() {
  if (!newType.value) return;

  const contents = campaign.value.contents ? [...campaign.value.contents] : [];

  const nextOrder = contents.length ? Math.max(...contents.map((c) => c.order || 0)) + 1 : 1;

  const newContent = {
    id: uuidv4(),
    type: newType.value,
    order: nextOrder,
    data: defaultDataMap[newType.value]?.() || {},
  };

  campaign.value = {
    ...campaign.value,
    contents: [...contents, newContent],
  };

  newType.value = "";
}

function updateContent(index, updatedContent) {
  const updatedContents = [...campaign.value.contents];
  updatedContents[index] = { ...updatedContent };

  campaign.value = {
    ...campaign.value,
    contents: updatedContents,
  };
}

function removeContent(index) {
  const updatedContents = [...campaign.value.contents];
  updatedContents.splice(index, 1);
  updatedContents.forEach((item, idx) => (item.order = idx + 1));

  campaign.value = {
    ...campaign.value,
    contents: updatedContents,
  };
}

function moveUp(index) {
  if (index === 0) return;

  const contents = [...campaign.value.contents];
  [contents[index - 1], contents[index]] = [contents[index], contents[index - 1]];

  contents.forEach((item, idx) => (item.order = idx + 1));

  campaign.value = {
    ...campaign.value,
    contents,
  };
}
function moveDown(index) {
  if (index === campaign.value.contents.length - 1) return;

  const contents = [...campaign.value.contents];
  [contents[index], contents[index + 1]] = [contents[index + 1], contents[index]];

  contents.forEach((item, idx) => (item.order = idx + 1));

  campaign.value = {
    ...campaign.value,
    contents,
  };
}

function toPascalCase(str) {
  if (!str) return "";
  return str[0].toUpperCase() + str.slice(1);
}

const defaultDataMap = {
  title: () => ({ title: "" }),
  subtitle: () => ({ subtitle: "" }),
  paragraph: () => ({ paragraph: "" }),
  image: () => ({ image: {}, imageData: "" }),
  quote: () => ({ quote: "" }),
  video: () => ({ video: {}, videoData: "" }),
  button: () => ({ text: "", link: "" }),
  list: () => ({ items: [] }),
  divider: () => ({}),
};
</script>
