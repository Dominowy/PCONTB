import UserAddUpdateView from "@/views/panel/users/UserAddView.vue";
import UserUpdateRoleView from "@/views/panel/users/UserUpdateRoleView.vue";

let routes = [];

routes.push(
  {
    path: "user/add",
    name: "panel:user:add",
    component: UserAddUpdateView,
    meta: { requiresAuth: true, role: "admin" },
  },
  {
    path: "user/:id/update",
    name: "panel:user:update",
    component: UserUpdateRoleView,
    meta: { requiresAuth: true, role: "admin" },
  }
);

export default routes;
