<template>
  <div class="inventory-container">
    <h1>Inventory Dashboard</h1>
    <hr />
    <div class="inventory-actions">
      <solar-button @click="showNewProductModal" id="addNewBtn">
        Add new item
      </solar-button>
      <solar-button @click="showShipmentModal" id="recieveShipmentBtn">
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
        <td>{{ item.quantityOnHand }}</td>
        <td>{{ $filters.price(item.product.price) }}</td>
        <td>
          <span v-if="item.product.isTaxable"> yes </span>
          <span v-else>no</span>
        </td>
        <td>
          <div>X</div>
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

@Options({
  name: "Inventory",
  components: { SolarButton, ShipmentModal, NewProductModal },
})
export default class Inventory extends Vue {
  isNewProductVisible = false;
  isShipmentVisible = false;

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
  saveNewProduct(product: IProduct) {
    console.log("new product:");
    console.log(product);
  }

  saveNewShipment(shipment: IShipment) {
    console.log("new shipment:");
    console.log(shipment);
  }
}
</script>

<style scoped></style>
