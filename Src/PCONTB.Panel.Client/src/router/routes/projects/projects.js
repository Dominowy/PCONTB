import ProjectsListView from "@/views/projects/projects/ProjectsListView.vue";
import ProjectAddView from "@/views/projects/projects/ProjectAddView.vue";
import ProjectSettingsView from "@/views/projects/projects/ProjectSettingsView.vue";

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
    component: ProjectAddView,
    meta: { requiresAuth: true },
  },
  {
    path: ":id/settings",
    name: "projects:project:settings",
    component: ProjectSettingsView,
    meta: { requiresAuth: true },
  }
);

export default routes;
