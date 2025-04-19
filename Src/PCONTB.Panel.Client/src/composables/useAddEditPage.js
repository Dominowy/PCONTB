import { onMounted, ref } from "vue";
import { useRouter } from "vue-router";
import { useStore } from "@/store/index";

export function useAddEditPage(title, submitInternal, redirectAfterSuccess) {
  const router = useRouter();
  const store = useStore();

  const isLoading = ref(false);
  const isAllTouched = ref(false);
  const errors = ref([]);
  const errorMessage = ref("");

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
      errorMessage.value = error.message;
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
  return { router, isLoading, submit, validate, errors, isAllTouched, errorMessage, store };
}
