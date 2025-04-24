import { onMounted } from "vue";

export function useView(title) {
  onMounted(() => {
    document.title = title;
  });

  return {};
}
