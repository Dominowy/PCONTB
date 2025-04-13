import { createRouter, createWebHistory } from "vue-router";
import { useSessionStore } from "@/store/index";

import DefaultLayout from "../layouts/DefaultLayout.vue";
import AuthLayout from "../layouts/AuthLayout.vue";

import Home from "../views/home/HomeView.vue";
import Login from "../views/account/auth/LoginView.vue";
import Register from "../views/account/auth/RegisterView.vue";
import Projects from "../views/projects/ProjectsView.vue";
import UserProfile from "@/views/account/users/UserProfileView.vue";

const routes = [
  {
    path: "/",
    component: DefaultLayout,
    children: [
      {
        path: "",
        name: "Home",
        component: Home,
      },
      {
        path: "projects",
        name: "Projects",
        component: Projects,
      },
      {
        path: "profile",
        name: "UserProfile",
        component: UserProfile,
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
];

const router = createRouter({
  history: createWebHistory(),
  routes,
});

router.beforeEach(async (to, from, next) => {
  const session = useSessionStore();

  if (!session.user && !session.loading) {
    await session.fetchSession();
  }

  if (to.meta.requiresAuth && !session.user) {
    return next("/");
  }

  next();
});

export default router;
