<template>
  <div v-if="!readonly">
    <h6>{{ index + ". Title" }}</h6>
    <base-form-input
      :id="'title' + index"
      :modelValue="content.data.title"
      placeholder="Enter title"
      @update:modelValue="updateField('title', $event)"
    />
  </div>
  <div v-else>
    <h2 :id="'title' + content.order">{{ content.data.title }}</h2>
  </div>
</template>

<script setup>
const props = defineProps({
  content: Object,
  index: Number,
  readonly: { type: Boolean, default: false },
});
const emit = defineEmits(["update"]);

function updateField(key, value) {
  emit("update", { ...props.content, data: { ...props.content.data, [key]: value } });
}
</script>
