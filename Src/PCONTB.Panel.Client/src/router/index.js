import { createRouter, createWebHistory } from "vue-router";

import DefaultLayout from "../layouts/DefaultLayout.vue";
import AuthLayout from "../layouts/AuthLayout.vue";

import Home from "../views/home/HomeView.vue";
import Login from "../views/account/auth/LoginView.vue";
import Register from "../views/account/auth/RegisterView.vue";
import Dashboard from "../views/projects/DashboardView.vue";

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
        path: "dashboard",
        name: "Dashboard",
        component: Dashboard,
      },
    ],
  },
  {
    path: "/auth",
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
