<template>
  <div v-if="!readonly">
    <h6>{{ index + ". Paragraph" }}</h6>
    <textarea
      class="form-control"
      rows="4"
      :value="content.data.paragraph"
      placeholder="Enter paragraph"
      @input="updateField('paragraph', $event.target.value)"
    ></textarea>
  </div>
  <div v-else>
    <p>
      {{ content.data.paragraph }}
    </p>
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
