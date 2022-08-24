<template>
  <div class="inventory-container">
    <h1 id="inventoryTitle">Inventory Dashboard</h1>
    <hr />
    <inventory-chart></inventory-chart>
    <div class="inventory-actions">
      <solar-button @button:click="showNewProductModal" id="addNewBtn">
        Add new item
      </solar-button>
      <solar-button @button:click="showShipmentModal" id="recieveShipmentBtn">
        Recieve Shipment
      </solar-button>
    </div>

    <table id="inventory-table" class="table">
      <tr>
        <th>Item</th>
        <th>On hand</th>
        <th>Unit price</th>
        <th>Taxable</th>
        <th>Delete</th>
      </tr>
      <tr v-for="item in inventory" :key="item.id">
        <td>{{ item.product.name }}</td>
        <td :class="`${appplyColor(item.quantityOnHand, item.idealQuantity)}`">
          {{ item.quantityOnHand }}
        </td>
        <td>{{ $filters.price(item.product.price) }}</td>
        <td>
          <span v-if="item.product.isTaxable"> yes </span>
          <span v-else>no</span>
        </td>
        <td>
          <div class="product-archive" @click="archiveProduct(item.product.id)">
            <i class="lni lni-cross-circle"></i>
          </div>
        </td>
      </tr>
    </table>

    <new-product-modal
      v-if="isNewProductVisible"
      @save:product="saveNewProduct"
      @close="closeModals"
    />

    <shipment-modal
      @close="closeModals"
      v-if="isShipmentVisible"
      @save:shipment="saveNewShipment"
      :inventory="inventory"
    />
  </div>
</template>
<script lang="ts">
import { Options, Vue } from "vue-class-component";
import { IProductInventory } from "@/types/Product";
import SolarButton from "@/components/SolarButton.vue";
import ShipmentModal from "@/components/modals/ShipmentModal.vue";
import NewProductModal from "@/components/modals/NewProductModal.vue";
import { IShipment } from "@/types/Shipment";
import { IProduct } from "@/types/Product";
import { InventoryService } from "@/services/inventory-service";
import { ProductService } from "@/services/product-service";
import InventoryChart from "@/components/charts/InventoryChart.vue";

const inventoryService = new InventoryService();
const productService = new ProductService();

@Options({
  name: "Inventory",
  components: { SolarButton, ShipmentModal, NewProductModal, InventoryChart },
})
export default class Inventory extends Vue {
  isNewProductVisible = false;
  isShipmentVisible = false;

  async archiveProduct(id: number) {
    await productService.deleteProduct(id);
    await this.fetchData();
  }

  async saveNewProduct(newProduct: IProduct) {
    await productService.saveNewProduct(newProduct);
    this.isNewProductVisible = false;
    await this.fetchData();
  }

  appplyColor(value: number, target: number) {
    if (value <= 0) return "red";

    if (Math.abs(value - target) > 8) return "yellow";

    return "green";
  }

  inventory: IProductInventory[] = [
    {
      id: 1,
      product: {
        id: 1,
        name: "Some product",
        description: "Good stuff",
        price: 100,
        createdOn: new Date(),
        updatedOn: new Date(),
        isTaxable: false,
        isArchived: false,
      },
      idealQuantity: 10,
      quantityOnHand: 3,
    },
    {
      id: 2,
      product: {
        id: 2,
        name: "Some another product",
        description: "Good stuff",
        price: 150,
        createdOn: new Date(),
        updatedOn: new Date(),
        isTaxable: true,
        isArchived: false,
      },
      idealQuantity: 200,
      quantityOnHand: 12,
    },
  ];

  $filters: any;
  $store: any;

  showShipmentModal() {
    console.log("click");
    this.isShipmentVisible = true;
  }
  showNewProductModal() {
    console.log("click");
    this.isNewProductVisible = true;
  }
  closeModals() {
    this.isShipmentVisible = false;
    this.isNewProductVisible = false;
  }

  async saveNewShipment(shipment: IShipment) {
    await inventoryService.updateInventoryQuantity(shipment);

    this.isShipmentVisible = false;

    await this.fetchData();
  }

  async fetchData() {
    this.inventory = await inventoryService.getInventory();

    await this.$store.dispatch("assignSnapshots");
  }

  async created() {
    await this.fetchData();
  }
}
</script>

<style scoped lang="scss">
@import "@/scss/global.scss";
.green {
  font-weight: bold;
  color: $solar-green;
}
.yellow {
  font-weight: bold;
  color: $solar-yellow;
}
.red {
  font-weight: bold;
  color: $solar-red;
}
.inventory-actions {
  display: flex;
  margin-bottom: 0.8rem;
}
.product-archive {
  cursor: pointer;
  font-weight: bold;
  font-size: 1.2rem;
  color: $solar-red;
}
</style>
