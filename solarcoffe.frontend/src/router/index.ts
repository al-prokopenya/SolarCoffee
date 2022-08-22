import { createRouter, createWebHistory, RouteRecordRaw } from "vue-router";
import Inventory from "@/views/Inventory.vue";
import Customers from "@/views/Customers.vue";

const routes: Array<RouteRecordRaw> = [
  {
    path: "/",
    name: "home",
    component: Inventory,
  },
  {
    path: "/inventory",
    name: "inventory",
    component: Inventory,
  },
  {
    path: "/customers",
    name: "customers",
    component: Customers,
  },
  {
    path: "/orders",
    name: "Orders",
    component: Customers,
  },
];

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes,
});

export default router;
