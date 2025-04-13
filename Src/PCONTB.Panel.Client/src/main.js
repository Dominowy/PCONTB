import "./assets/main.css";
import { createApp } from "vue";
import App from "./App.vue";
import { createBootstrap } from "bootstrap-vue-next";
import router from "./router/index";
import { createPinia } from "pinia";

import "bootstrap/dist/css/bootstrap.min.css";
import "bootstrap";

const app = createApp(App);
const pinia = createPinia();
app.use(router);
app.use(createBootstrap);
app.use(pinia);
app.mount("#app");
