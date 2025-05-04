import DiscoverProjectsView from "@/views/projects/discover/DiscoverProjectsView.vue";
import DiscoverDisplayProjectView from "@/views/projects/discover/DiscoverDisplayProjectView.vue";

let routes = [];

routes.push({
  path: "discover",
  name: "projects:discover",
  component: DiscoverProjectsView,
},
{
  path: "discover/:id/display",
  name: "projects:discover:display",
  component: DiscoverDisplayProjectView,
},);

export default routes;
