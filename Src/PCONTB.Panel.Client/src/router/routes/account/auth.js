import AuthLayout from "@/layouts/AuthLayout.vue";
import LoginView from "@/views/account/auth/LoginView.vue";
import RegisterView from "@/views/account/auth/RegisterView.vue";

let routes = [];

routes.push({
  path: "/",
  component: AuthLayout,
  children: [
    {
      path: "login",
      name: "Login",
      component: LoginView,
    },
    {
      path: "register",
      name: "Register",
      component: RegisterView,
    },
  ],
});

export default routes;
