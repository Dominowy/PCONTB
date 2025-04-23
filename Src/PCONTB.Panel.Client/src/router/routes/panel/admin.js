import AdminPanelView from "@/views/panel/AdminPanelView.vue";

let routes = [];

routes.push({
  path: "admin",
  name: "panel:admin",
  component: AdminPanelView,
  meta: { requiresAuth: true, role: "admin" },
});

export default routes;
