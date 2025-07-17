<template>
  <div v-if="!readonly">
    <h6>{{ index + ". Image" }}</h6>

    <base-form-file
      property="Image"
      :modelValue="content.data.image"
      uploadUrl="/api/multimedia/upload-file"
      placeholder="Select image"
      :src="content.data.imageData"
      @update:modelValue="updateField('image', $event)"
    />
  </div>
  <div v-else>
    <img
      v-if="content.data.image"
      class="w-100"
      :src="`data:${content.data.image.contentType};base64,${content.data.imageData}`"
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
