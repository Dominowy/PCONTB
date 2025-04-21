<template>
  <div class="loading-overlay">
    <base-loading-spinner />
  </div>
</template>

<style scoped>
.loading-overlay {
  position: fixed;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  background: rgba(255, 255, 255);
  display: flex;
  align-items: center;
  justify-content: center;
  z-index: 9999;
}
</style>
