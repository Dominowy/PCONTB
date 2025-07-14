import DiscoverProjectsView from "@/views/projects/discover/ProjectsTableView.vue";
import DiscoverDisplayProjectView from "@/views/projects/discover/ProjectDisplayView.vue";

let routes = [];

routes.push(
  {
    path: "discover",
    name: "projects:discover",
    component: DiscoverProjectsView,
  },
  {
    path: "discover/:id/display",
    name: "projects:discover:display",
    component: DiscoverDisplayProjectView,
  }
);

export default routes;
