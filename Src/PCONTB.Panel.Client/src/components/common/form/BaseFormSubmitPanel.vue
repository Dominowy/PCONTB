<template>
  <div class="d-flex justify-content-between mt-4">
    <div class="left-container">
      <slot name="left"> </slot>
    </div>
    <div class="right-container">
      <slot name="right">
        <base-loading-spinner v-if="isLoading" />
        <div class="submit-actions-group">
          <slot name="submit-actions">
            <base-form-submit-button :label="label" :variant="variant" />
          </slot>
        </div>
      </slot>
    </div>
  </div>
</template>

<style scoped>
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
