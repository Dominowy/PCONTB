import ProjectsListView from "@/views/projects/projects/ProjectsTableView.vue";
import ProjectAddUpdateView from "@/views/projects/projects/ProjectAddUpdate.vue";

let routes = [];

routes.push(
  {
    path: "",
    name: "projects:projects",
    component: ProjectsListView,
    meta: { requiresAuth: true },
  },
  {
    path: "project/add",
    name: "projects:project:add",
    component: ProjectAddUpdateView,
    meta: { requiresAuth: true },
  },
  {
    path: ":id/update",
    name: "projects:project:update",
    component: ProjectAddUpdateView,
    meta: { requiresAuth: true },
  }
);

export default routes;
