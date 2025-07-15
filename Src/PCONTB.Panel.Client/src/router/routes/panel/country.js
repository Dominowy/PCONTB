import CountryAddUpdateView from "@/views/panel/countries/CountryAddUpdateView.vue";
import CountryDeleteView from "@/views/panel/countries/CountryDeleteView.vue";

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
  },
  {
    path: "country/:id/delete",
    name: "panel:country:delete",
    component: CountryDeleteView,
    meta: { requiresAuth: true, role: "admin" },
  }
);

export default routes;
