import DiscoverProjectsView from "@/views/projects/discover/DiscoverProjectsView.vue";
import DiscoverDisplayProjectView from "@/views/projects/discover/DiscoverDisplayProjectView.vue";

let routes = [];

routes.push({
  path: "discover",
  name: "discover",
  component: DiscoverProjectsView,
},
{
  path: "discover/:id/display",
  name: "projects:display",
  component: DiscoverDisplayProjectView,
},);

export default routes;
