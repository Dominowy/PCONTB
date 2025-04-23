import DefaultLayout from "@/layouts/DefaultLayout.vue";
import Projects from "./projects";
import Discover from "./discover";

let routes = [];

routes.push({
  path: "/projects",
  component: DefaultLayout,
  children: [...Projects, ...Discover],
  
});

export default routes;
