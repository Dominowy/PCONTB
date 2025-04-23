import ListProjectsView from "@/views/projects/projects/ListProjectsView.vue";
import AddProjectView from "@/views/projects/projects/AddProjectView.vue";
import UpdateProjectView from "@/views/projects/projects/UpdateProjectView.vue";

let routes = [];

routes.push(
  {
    path: "",
    name: "projects:projects",
    component: ListProjectsView,
    meta: { requiresAuth: true },
  },
  {
    path: "project/add",
    name: "projects:project:add",
    component: AddProjectView,
    meta: { requiresAuth: true },
  },
  {
    path: ":id/update",
    name: "projects:project:update",
    component: UpdateProjectView,
    meta: { requiresAuth: true },
  }
);

export default routes;
