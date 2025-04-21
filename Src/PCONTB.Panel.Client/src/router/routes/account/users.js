import UserSettingsView from "@/views/account/users/UserSettingsView.vue";
import UserProfileView from "@/views/account/users/UserProfileView.vue";

let routes = [];

routes.push(
  {
    path: "profile",
    name: "users:profile",
    component: UserProfileView,
  },
  {
    path: "settings",
    name: "users:settings",
    component: UserSettingsView,
  }
);

export default routes;
