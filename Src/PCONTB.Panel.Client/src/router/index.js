import { createRouter, createWebHistory } from "vue-router";
import { useStore } from "@/store/index";

import routes from "./routes";

const router = createRouter({
  history: createWebHistory(),
  linkExactActiveClass: "active",
  base: "/",
  routes,
});

router.beforeEach(async (to, from, next) => {
  const store = useStore();
  store.startTransition();

  await store.fetchSession();

  if (to.meta.requiresAuth && !store.user) {
    return next("/");
  }

  if (to.meta.role && !store.user?.roles.includes(to.meta.role)) {
    return next("/");
  }

  next();
});

router.afterEach(() => {
  const store = useStore();

  setTimeout(() => {
    store.stopTransition();
  }, 300);
});

export default router;
