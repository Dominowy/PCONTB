<template>
  <div class="modal fade show" tabindex="-1" style="display: block">
    <div class="modal-dialog">
      <div class="modal-content">
        <div class="modal-header">
          <h5 class="modal-title">{{ title }}</h5>
          <button type="button" class="btn-close" @click="handleCloseModal" aria-label="Close" />
        </div>

        <div class="modal-body">
          <template v-if="isLoading">
            <div class="d-flex justify-content-center align-items-center">
              <base-loading-spinner />
            </div>
          </template>
          <slot v-else name="body"></slot>
        </div>
        <div class="modal-footer">
          <template v-if="isLoading">
            <button type="button" class="btn btn-secondary" @click="handleCloseModal">Close</button>
          </template>
          <slot v-else name="footer"></slot>
        </div>
      </div>
    </div>
  </div>
  <div class="modal-backdrop fade show"></div>
</template>

<script setup>
defineProps({
  title: String,
  isLoading: Boolean,
});

const emit = defineEmits(["close"]);

const handleCloseModal = () => {
  emit("close");
};
</script>
