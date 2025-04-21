import DefaultLayout from "@/layouts/DefaultLayout.vue";
import Users from "./users";
import Auth from "./auth";

let routes = [];
routes.push(...Auth);

routes.push({
  path: "/users",
  component: DefaultLayout,
  children: [...Users],
  meta: { requiresAuth: true },
});

export default routes;
