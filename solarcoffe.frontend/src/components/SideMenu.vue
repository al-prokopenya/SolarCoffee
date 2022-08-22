<template>
  <div class="side-menu-container">
    <router-link to="/">
      <img id="imgLogo" src="../assets/images/solar_coffee_logo.png"
    /></router-link>
    <h1>Management portal</h1>
    <solar-button
      id="menuInventory"
      @button:click="goToRoute('/inventory')"
      :isFullwidth="true"
    >
      Inventory
    </solar-button>
    <solar-button
      id="menuCustomers"
      @button:click="goToRoute('/customers')"
      :isFullwidth="true"
    >
      Manage customers
    </solar-button>
    <solar-button
      id="menuInvoice"
      @button:click="goToRoute('/invoice/new')"
      :isFullwidth="true"
    >
      New invoice
    </solar-button>
    <solar-button
      id="menuOrders"
      @button:click="goToRoute('/orders')"
      :isFullwidth="true"
    >
      Orders
    </solar-button>
  </div>
</template>

<script lang="ts">
import { Options, Vue } from "vue-class-component";
import SolarButton from "@/components/SolarButton.vue";

@Options({
  name: "SideMenu",
  components: { SolarButton },
})
export default class SideMenu extends Vue {
  async goToRoute(route: string) {
    await this.$router.push(route);
  }
}
</script>

<style scoped lang="scss">
@import "@/scss/global.scss";
.side-menu-container {
  background-color: #fcfcfc;
  height: 100vh;
  width: $menu-width;
  display: flex;
  flex-direction: column;
  padding: 0.8rem;
  box-shadow: 0 1px 1px rgba(0, 0, 0, 0.12), 0 1px 1px rgba(0, 0, 0, 0.24);
  box-sizing: border-box;
}
#imgLogo {
  width: 100%;
}
h1 {
  font-size: 1.2rem;
  margin: 1rem 0;
}
</style>
