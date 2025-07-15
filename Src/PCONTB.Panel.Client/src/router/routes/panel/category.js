import CategoryAddUpdateView from "@/views/panel/categories/CategoryAddUpdateView.vue";
import CategoryDeleteView from "@/views/panel/categories/CategoryDeleteView.vue";

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
  },
  {
    path: "category/:id/delete",
    name: "panel:category:delete",
    component: CategoryDeleteView,
    meta: { requiresAuth: true, role: "admin" },
  }
);

export default routes;
