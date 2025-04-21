import DefaultLayout from "@/layouts/DefaultLayout.vue";
import HomeView from "@/views/home/HomeView.vue";

let routes = [];

routes.push({
  path: "/",
  component: DefaultLayout,
  children: [
    {
      path: "",
      name: "home",
      component: HomeView,
    },
  ],
});

export default routes;
