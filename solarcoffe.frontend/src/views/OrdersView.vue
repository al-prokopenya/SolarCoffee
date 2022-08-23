<template>
  <div class="orders-list">
    <h1 id="ordersTitle">Sales orders</h1>
    <table v-if="orders.length" id="sales-orders" class="table">
      <thead>
        <tr>
          <th>Customer</th>
          <th>Order id</th>
          <th>Order total</th>
          <th>Order order status</th>
          <th>Mark complete</th>
        </tr>
      </thead>
      <tr v-for="order in orders" :key="order.id">
        <td>
          {{ order.customer.name + " " + order.customer.lastName }}
        </td>
        <td>
          {{ order.id }}
        </td>
        <td>
          {{ getTotal(order) }}
        </td>
        <td :class="{ green: order.isPaid }">
          {{ getStatus(order.isPaid) }}
        </td>
        <td>
          <div
            v-if="!order.isPaid"
            class="lni lni-checkmark-circle green order-complete"
            @click="markComplete(order.id)"
          ></div>
        </td>
      </tr>
    </table>
  </div>
</template>

<script lang="ts">
import { OrderService } from "@/services/orders-service";
import { IOrder } from "@/types/Order";
import { Options, Vue } from "vue-class-component";

const ordersService = new OrderService();
@Options({
  name: "OrdersView",
  components: {},
})
export default class OrdersView extends Vue {
  orders: IOrder[] = [];
  $filters: any;

  getTotal(order: IOrder): number {
    const total = order.salesOrderItems.reduce(
      (a, b) => a + b.product.price * b.quantity,
      0
    );

    return this.$filters.price(total);
  }

  getStatus(isPaid: boolean) {
    return isPaid ? "Paid in full" : "Unpaid";
  }
  async markComplete(orderId: number) {
    await ordersService.makeOrderCompleted(orderId);
    this.fetchData();
  }
  async fetchData() {
    this.orders = await ordersService.getOrders();
  }
  async created() {
    this.fetchData();
  }
}
</script>

<style scoped lang="scss">
@import "@/scss/global.scss";
.green {
  font-weight: bold;
  color: $solar-green;
}
.order-complete {
  cursor: pointer;
  text-align: center;
}
</style>
