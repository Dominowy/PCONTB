import ModerationPanelView from "@/views/panel/ModerationPanelView.vue";

let routes = [];

routes.push({
  path: "moderation",
  name: "panel:moderation",
  component: ModerationPanelView,
  meta: { requiresAuth: true, role: "moderator" },
});

export default routes;
