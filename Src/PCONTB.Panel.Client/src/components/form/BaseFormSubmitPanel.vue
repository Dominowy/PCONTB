<template>
  <div class="d-flex justify-content-between mt-4">
    <div class="submit-actions-group">
      <div class="left-container">
        <slot name="left"> </slot>
      </div>
      <div class="right-container">
        <slot name="right">
          <base-loading-spinner v-if="isLoading" />
          <slot name="submit-actions">
            <base-form-submit-button :label="label" :variant="variant" :disabled="isLoading" />
          </slot>
        </slot>
      </div>
    </div>
  </div>
</template>

<style scoped>
.submit-actions-group {
  position: fixed;
  bottom: 0;
  left: 0;
  width: 100%;
  height: 60px;
  background: white;
  border-top: 1px solid #ddd;
  box-shadow: 0 -2px 8px rgba(0, 0, 0, 0.1);
  z-index: 1000;
  display: flex;
  align-items: center;
  justify-content: end;
  padding: 1rem;
}

.submit-actions-group .btn {
  float: right;
  margin-left: 25px;
}

.left-container,
.right-container {
  display: flex;
  align-items: center;
}
</style>

<script setup>
defineProps({
  label: {
    type: String,
    default: "Save",
  },
  variant: {
    type: String,
    default: "primary",
  },
  isLoading: {
    type: Boolean,
    default: false,
  },
});
</script>
