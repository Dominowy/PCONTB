import DefaultLayout from "@/layouts/DefaultLayout.vue";
import Moderation from "./moderation";
import Admin from "./admin";

let routes = [];

routes.push({
  path: "/",
  component: DefaultLayout,
  children: [...Moderation, ...Admin],
});

export default routes;
