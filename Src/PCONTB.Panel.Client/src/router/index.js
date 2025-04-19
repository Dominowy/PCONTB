import { createRouter, createWebHistory } from "vue-router";
import { useStore } from "@/store/index";

import DefaultLayout from "../layouts/DefaultLayout.vue";
import AuthLayout from "../layouts/AuthLayout.vue";

import Home from "../views/home/HomeView.vue";
import Login from "../views/account/auth/LoginView.vue";
import Register from "../views/account/auth/RegisterView.vue";
import Discover from "@/views/discover/DiscoverView.vue";
import Projects from "@/views/projects/ProjectsView.vue";
import Profile from "@/views/account/users/ProfileView.vue";
import ModerationPanel from "@/views/panel/ModerationPanelView.vue";
import AdminPanel from "@/views/panel/AdminPanelView.vue";
import LoadingLayout from "@/layouts/LoadingLayout.vue";

const routes = [
  {
    path: "/",
    component: DefaultLayout,
    children: [
      {
        path: "home",
        name: "home",
        component: Home,
      },
      {
        path: "discover",
        name: "discover",
        component: Discover,
      },
      {
        path: "projects",
        name: "projects",
        component: Projects,
      },
      {
        path: "profile",
        name: "profile",
        component: Profile,
        meta: { requiresAuth: true },
      },
      {
        path: "moderation",
        name: "ModerationPanel",
        component: ModerationPanel,
        meta: { requiresAuth: true, roles: ["moderator", "admin"] },
      },
      {
        path: "admin",
        name: "AdminPanel",
        component: AdminPanel,
        meta: { requiresAuth: true, roles: ["admin"] },
      },
    ],
  },
  {
    path: "/",
    component: AuthLayout,
    children: [
      {
        path: "login",
        name: "Login",
        component: Login,
      },
      {
        path: "register",
        name: "Register",
        component: Register,
      },
    ],
  },
  {
    path: "/loading",
    component: LoadingLayout,
  },
];

const router = createRouter({
  history: createWebHistory(),
  routes,
});

router.beforeEach(async (to, from, next) => {
  const store = useStore();
  store.startLoading();

  await store.fetchSession();

  if (to.meta.requiresAuth && !store.user) {
    return next("/home");
  }

  if (to.meta.roles && !to.meta.roles.includes(store.user?.role)) {
    return next("/home");
  }

  next();
});

router.afterEach(() => {
  const store = useStore();

  setTimeout(() => {
    store.stopLoading();
  }, 300);
});

export default router;
