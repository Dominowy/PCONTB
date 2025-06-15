import { onMounted, ref } from "vue";
import { useStore } from "@/store/index";

export function useDisplay(title) {
  const store = useStore();

  const errorMessage = ref("");
  const content = ref(null);

  onMounted(() => {
    if (title) document.title = title;
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

  const setTitle = (title) => {
    document.title = title;
  };

  return { content, loadData, setTitle };
}
