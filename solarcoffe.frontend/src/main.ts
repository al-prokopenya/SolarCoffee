import { createApp } from "vue";
import App from "./App.vue";
import router from "./router";
import store from "./store";

const app = createApp(App);
app.config.globalProperties.$filters = {
  price(value: number) {
    if (isNaN(value)) {
      return "-";
    }
    return "$ " + value.toFixed(2);
  },
};

app.use(store).use(router).mount("#app");
