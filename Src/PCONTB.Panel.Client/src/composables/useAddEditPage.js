import { onMounted, ref } from "vue";
import { useRouter } from "vue-router";

export function useAddEditPage(title, submitInternal) {
  const router = useRouter();

  const isLoading = ref(false);

  onMounted(() => {
    document.title = title;
  });

  const submit = async () => {
    submitInternal(false);
  };
  const validate = async () => {
    submitInternal(true);
  };
  return { router, isLoading, submit, validate };
}
