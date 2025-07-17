<template>
  <div v-if="!readonly">
    <h6>{{ index + ". Quote" }}</h6>
    <base-form-input
      :id="'quote' + index"
      :modelValue="content.data.quote"
      placeholder="Enter quote"
      @update:modelValue="updateField('quote', $event)"
    />
  </div>
  <div v-else>
    <blockquote class="blockquote text-muted">
      <p class="mb-0">{{ content.data.quote }}</p>
    </blockquote>
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
