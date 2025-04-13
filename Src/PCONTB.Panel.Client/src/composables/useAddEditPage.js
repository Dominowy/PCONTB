import { onMounted, ref } from "vue";
import { useRouter } from "vue-router";

export function useAddEditPage(title, submitInternal, redirectAfterSuccess) {
  const router = useRouter();

  const isLoading = ref(false);
  const isAllTouched = ref(false);
  const errors = ref([]);

  onMounted(() => {
    document.title = title;
  });

  const submit = async () => {
    isLoading.value = true;
    try {
      await submitInternal(false);
      errors.value = [];
      redirectAfterSuccess();
    } catch (error) {
      errors.value = error.message.errors;
      isAllTouched.value = true;
    } finally {
      isLoading.value = false;
    }
  };
  const validate = async () => {
    try {
      await submitInternal(true);
      errors.value = [];
    } catch (error) {
      errors.value = error.message.errors;
    } finally {
      isLoading.value = false;
    }
  };
  return { router, isLoading, submit, validate, errors, isAllTouched };
}
