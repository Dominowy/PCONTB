import UserSettingsView from "@/views/account/users/UserSettingsView.vue";
import UserProfileView from "@/views/account/users/UserProfileView.vue";

let routes = [];

routes.push(
  {
    path: "user/:id/profile",
    name: "account:users:profile",
    component: UserProfileView,
  },
  {
    path: "user/settings",
    name: "account:users:settings",
    component: UserSettingsView,
    meta: { requiresAuth: true },
  }
);

export default routes;
