import "./assets/main.css";
import { createApp } from "vue";
import App from "./App.vue";
import { createBootstrap } from "bootstrap-vue-next";

import "bootstrap/dist/css/bootstrap.min.css";
import "bootstrap";

const app = createApp(App);
app.use(createBootstrap);
app.mount("#app");
