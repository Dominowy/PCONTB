<template>
  <component
    :is="componentMap[content.type]"
    :content="content"
    :index="index"
    :readonly="readonly"
    @update="emitUpdate"
  />

  <div class="text-end mt-2" v-if="!readonly">
    <div>
      <button type="button" class="btn btn-outline-primary btn-sm me-2" @click="$emit('move-up')">
        <i-material-symbols-arrow-upward />
      </button>
      <button
        type="button"
        variant="primary"
        class="btn btn-outline-primary btn-sm me-2"
        @click="$emit('move-down')"
      >
        <i-material-symbols-arrow-downward />
      </button>
      <button type="button" class="btn btn-sm btn-outline-danger" @click="$emit('remove')">
        <i-material-symbols-delete />
      </button>
    </div>
  </div>
</template>

<script setup>
import ContentTitle from "./ContentTitle.vue";
import ContentSubtitle from "./ContentSubtitle.vue";
import ContentParagraph from "./ContentParagraph.vue";
import ContentImage from "./ContentImage.vue";
import ContentQuote from "./ContentQuote.vue";
import ContentVideo from "./ContentVideo.vue";
import ContentButton from "./ContentButton.vue";
import ContentDivider from "./ContentDivider.vue";
import ContentList from "./ContentList.vue";

defineProps({
  content: Object,
  index: Number,
  readonly: { type: Boolean, default: false },
});

const emit = defineEmits(["update", "remove", "move-up", "move-down"]);

const componentMap = {
  title: ContentTitle,
  subtitle: ContentSubtitle,
  paragraph: ContentParagraph,
  image: ContentImage,
  quote: ContentQuote,
  video: ContentVideo,
  button: ContentButton,
  divider: ContentDivider,
  list: ContentList,
};

function emitUpdate(updatedContent) {
  emit("update", updatedContent);
}
</script>
