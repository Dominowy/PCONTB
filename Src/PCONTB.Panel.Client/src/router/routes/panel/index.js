import DefaultLayout from "@/layouts/DefaultLayout.vue";
import Moderation from "./moderation";
import Admin from "./admin";
import User from "./user";

let routes = [];

routes.push({
  path: "/panel",
  component: DefaultLayout,
  children: [...Moderation, ...Admin, ...User],
});

export default routes;
