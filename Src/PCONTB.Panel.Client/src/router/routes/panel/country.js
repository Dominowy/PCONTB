import CountryAddUpdateView from "@/views/panel/countries/CountryAddUpdateView.vue";

let routes = [];

routes.push(
  {
    path: "country/add",
    name: "panel:country:add",
    component: CountryAddUpdateView,
    meta: { requiresAuth: true, role: "admin" },
  },
  {
    path: "country/:id/update",
    name: "panel:country:update",
    component: CountryAddUpdateView,
    meta: { requiresAuth: true, role: "admin" },
  }
);

export default routes;
