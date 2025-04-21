import ListProjectsView from "@/views/projects/ListProjectsView.vue";
import DisplayProjectView from "@/views/projects/DisplayProjectView.vue";
import AddUpdateProjectView from "@/views/projects/AddUpdateProjectView.vue";

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
    component: AddUpdateProjectView,
  },
  {
    path: ":id/update",
    name: "projects:update",
    component: AddUpdateProjectView,
  }
);

export default routes;
