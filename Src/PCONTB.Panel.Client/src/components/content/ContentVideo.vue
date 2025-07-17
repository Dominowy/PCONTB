<template>
  <div v-if="!readonly">
    <h6>{{ index + ". Video" }}</h6>
    <base-form-file
      property="Video"
      :modelValue="content.data.video"
      uploadUrl="/api/multimedia/upload-file"
      placeholder="Select video"
      :src="content.data.videoData"
      @update:modelValue="updateField('video', $event)"
    />
  </div>
  <div v-else>
    <video
      v-if="content.data.video"
      class="w-100"
      :src="`data:${content.data.video.contentType};base64,${content.data.videoData}`"
      controls
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
