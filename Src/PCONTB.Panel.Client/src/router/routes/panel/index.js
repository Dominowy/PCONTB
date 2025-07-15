import DefaultLayout from "@/layouts/DefaultLayout.vue";
import Moderation from "./moderation";
import Admin from "./admin";
import User from "./user";
import Country from "./country";
import Category from "./category";

let routes = [];

routes.push({
  path: "/panel",
  component: DefaultLayout,
  children: [...Moderation, ...Admin, ...User, ...Country, ...Category],
});

export default routes;
