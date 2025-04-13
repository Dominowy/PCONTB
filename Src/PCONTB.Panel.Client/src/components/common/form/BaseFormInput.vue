<template>
  <base-form-field :label="label" :errors="errors">
    <input
      class="form-control"
      :type="type"
      :value="modelValue"
      :placeholder="placeholder"
      @input="onInput"
    />
  </base-form-field>
</template>

<script setup>
defineProps({
  label: String,
  placeholder: String,
  type: {
    type: String,
    default: "text",
  },
  errors: {
    type: Array,
    default: () => [],
  },
  modelValue: [String, Number],
});

const emit = defineEmits(["update:modelValue"]);

const onInput = (event) => {
  emit("update:modelValue", event.target.value);
};
</script>
