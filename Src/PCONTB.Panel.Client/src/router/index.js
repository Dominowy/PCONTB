import { createRouter, createWebHistory } from "vue-router";

import DefaultLayout from "../layouts/DefaultLayout.vue";
import AuthLayout from "../layouts/AuthLayout.vue";

import Home from "../views/home/HomeView.vue";
import Login from "../views/account/auth/LoginView.vue";
import Register from "../views/account/auth/RegisterView.vue";
import Projects from "../views/projects/ProjectsView.vue";
import UserProfile from "@/views/account/user/UserProfileView.vue";

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

export default router;
