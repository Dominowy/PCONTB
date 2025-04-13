<template>
  <fieldset class="form-group">
    <label :for="id" class="form-label d-block"> {{ label }}: </label>
    <slot></slot>
    <ul class="list-unstyled text-danger form-message my-0" v-if="hasErrors">
      <li v-for="error in errors" :key="error">
        {{ error }}
      </li>
    </ul>
  </fieldset>
</template>

<script setup>
import { computed } from "vue";

const props = defineProps({
  id: String,
  label: null,
  errors: {
    type: Array,
    default: () => [],
  },
  isTouched: {
    type: Boolean,
    default: false,
  },
  isAllTouched: {
    type: Boolean,
    default: false,
  },
});

const hasErrors = computed(() => {
  if (props.isAllTouched) return true;
  if (!props.isTouched) return false;
  return props.errors != null && props.errors.length > 0;
});
</script>
