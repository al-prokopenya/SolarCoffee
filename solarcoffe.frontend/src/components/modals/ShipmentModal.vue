<template>
  <solar-modal>
    <template v-slot:header> Recieve shipment</template>
    <template v-slot:body>
      <label for="product">Product recieved</label>
      <select v-model="selectedProduct" class="shipmentItems" id="product">
        <option disabled value="">Select one</option>
        <option
          v-for="item in inventory"
          :value="item.product"
          :key="item.product.id"
        >
          {{ item.product.name }}
        </option>
      </select>
      <label for="qtyRecieved">Quantity recieved</label>
      <input type="number" id="qtyRecieved" v-model="qtyRecieved" />
    </template>
    <template v-slot:footer>
      <solar-button
        type="button"
        @button:click="save"
        aria-label="save new shipment"
      >
        Save shipment</solar-button
      >
      <solar-button
        type="button"
        @button:click="close"
        aria-label="Close modal"
      >
        Close</solar-button
      >
    </template>
  </solar-modal>
</template>

<script lang="ts">
import { Options, Vue } from "vue-class-component";
import SolarButton from "@/components/SolarButton.vue";
import SolarModal from "@/components/modals/SolarModal.vue";
import { IProductInventory, IProduct } from "@/types/Product";
import { IShipment } from "@/types/Shipment";

@Options({
  name: "ShipmentModal",
  props: {
    inventory: { type: Object, required: true },
  },
  components: { SolarButton, SolarModal },
})
export default class ShipmentModal extends Vue {
  inventory?: IProductInventory[];
  selectedProduct: IProduct = {
    id: 0,
    createdOn: new Date(),
    updatedOn: new Date(),
    isArchived: false,
    isTaxable: false,
    name: "0",
    description: "",
    price: 0,
  };
  qtyRecieved = 0;

  close() {
    this.$emit("close");
  }
  save() {
    const shipment: IShipment = {
      productId: this.selectedProduct.id,
      adjustment: this.qtyRecieved,
    };

    this.$emit("save:shipment", shipment);
  }
}
</script>

<style scoped lang="scss">
@import "@/scss/global.scss";
</style>
