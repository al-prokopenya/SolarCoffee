import { createApp } from "vue";
import App from "./App.vue";
import router from "./router";
import store from "./store";
import moment from "moment";

const app = createApp(App);

app.config.globalProperties.$filters = {
  price(value: number) {
    if (isNaN(value)) {
      return "-";
    }
    return "$ " + value.toFixed(2);
  },
  humanizeDate(date: Date) {
    return moment(date).format("MMMM Do YYYY");
  },
};

app.use(store).use(router).mount("#app");
