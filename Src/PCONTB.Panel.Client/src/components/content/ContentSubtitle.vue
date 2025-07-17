<template>
  <div v-if="!readonly">
    <h6>{{ index + ". Subtitle" }}</h6>
    <base-form-input
      :id="'subtitle' + index"
      :modelValue="content.data.subtitle"
      placeholder="Enter subtitle"
      @update:modelValue="updateField('subtitle', $event)"
    />
  </div>
  <div v-else>
    <h5>{{ content.data.subtitle }}</h5>
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
