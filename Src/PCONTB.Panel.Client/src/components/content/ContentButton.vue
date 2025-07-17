<template>
  <div v-if="!readonly">
    <h6>{{ index + ". Button" }}</h6>
    <base-form-input
      label="Text"
      :id="'text' + index"
      :modelValue="content.data.text"
      placeholder="Enter button text"
      @update:modelValue="updateField('text', $event)"
    />
    <base-form-input
      label="Link"
      :id="'link' + index"
      :modelValue="content.data.link"
      placeholder="Enter button link"
      @update:modelValue="updateField('link', $event)"
    />
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
