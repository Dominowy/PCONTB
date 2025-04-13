import { onMounted, ref } from "vue";
import { useRouter } from "vue-router";

export function useAddEditPage(title) {
  const router = useRouter();

  const isLoading = ref(false);

  onMounted(() => {
    document.title = title;
  });
  return { router, isLoading };
}
