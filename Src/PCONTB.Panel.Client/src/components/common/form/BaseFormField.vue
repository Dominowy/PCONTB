<template>
  <fieldset class="form-group">
    <label class="form-label d-block"> {{ label }}: </label>
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
  label: null,
  errors: {
    type: Array,
    default: () => [],
  },
});

const hasErrors = computed(() => props.errors != null && props.errors.length > 0);
</script>
