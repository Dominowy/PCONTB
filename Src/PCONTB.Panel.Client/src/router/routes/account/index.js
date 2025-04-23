import DefaultLayout from "@/layouts/DefaultLayout.vue";
import Users from "./users";
import Auth from "./auth";

let routes = [];
routes.push(...Auth);

routes.push({
  path: "/account",
  component: DefaultLayout,
  children: [...Users],
});

export default routes;
