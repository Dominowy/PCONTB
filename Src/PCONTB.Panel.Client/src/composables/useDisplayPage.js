import { onMounted, ref } from "vue";
import { useStore } from "@/store/index";

export function useDisplayPage(title) {
  const store = useStore();

  const errorMessage = ref("");
  const content = ref(null);

  onMounted(() => {
    document.title = title;
  });

  const loadData = async (onDataLoaded) => {
    store.startLoading();
    try {
      let response = await onDataLoaded();
      content.value = response;
    } catch (error) {
      errorMessage.value = error.message;
    } finally {
      store.stopLoading();
    }
  };

  return { content, loadData };
}
