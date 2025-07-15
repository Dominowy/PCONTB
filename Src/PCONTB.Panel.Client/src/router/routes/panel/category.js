import CategoryAddUpdateView from "@/views/panel/categories/CategoryAddUpdateView.vue";

let routes = [];

routes.push(
  {
    path: "category/add",
    name: "panel:category:add",
    component: CategoryAddUpdateView,
    meta: { requiresAuth: true, role: "admin" },
  },
  {
    path: "category/:id/update",
    name: "panel:category:update",
    component: CategoryAddUpdateView,
    meta: { requiresAuth: true, role: "admin" },
  }
);

export default routes;
