import UserAddView from "@/views/panel/users/UserAddView.vue";
import UserDisplayView from "@/views/panel/users/UserDisplayView.vue";

let routes = [];

routes.push(
  {
    path: "user",
    name: "panel:user",
    component: UserDisplayView,
    meta: { requiresAuth: true, role: "admin" },
  },
  {
    path: "user/add",
    name: "panel:user:add",
    component: UserAddView,
    meta: { requiresAuth: true, role: "admin" },
  }
);

export default routes;
