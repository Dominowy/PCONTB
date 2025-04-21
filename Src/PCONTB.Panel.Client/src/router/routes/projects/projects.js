import ListProjectsView from "@/views/projects/ListProjectsView.vue";
import DisplayProjectView from "@/views/projects/DisplayProjectView.vue";
import AddProjectView from "@/views/projects/AddProjectView.vue";
import UpdateProjectView from "@/views/projects/UpdateProjectView.vue";

let routes = [];

routes.push(
  {
    path: "",
    name: "projects",
    component: ListProjectsView,
  },
  {
    path: ":id/display",
    name: "projects:display",
    component: DisplayProjectView,
  },
  {
    path: "add",
    name: "projects:add",
    component: AddProjectView,
  },
  {
    path: ":id/update",
    name: "projects:update",
    component: UpdateProjectView,
  }
);

export default routes;
