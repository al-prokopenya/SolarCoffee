<template>
  <solar-modal>
    <template v-slot:header> New product</template>
    <template v-slot:body>
      <ul class="new-product">
        <li>
          <label for="isTaxable">Is this a taxable product?</label>
          <input
            type="checkbox"
            id="isTaxable"
            v-model="newProduct.isTaxable"
            name="isTaxable"
          />
        </li>
        <li>
          <label for="productName">Name</label>
          <input
            type="text"
            id="productName"
            v-model="newProduct.name"
            name="productName"
          />
        </li>
        <li>
          <label for="productDesc">Description</label>
          <input
            type="text"
            id="productDesc"
            v-model="newProduct.description"
            name="productDesc"
          />
        </li>
        <li>
          <label for="productPrice">Price (USD)</label>
          <input
            type="number"
            id="productPrice"
            v-model="newProduct.price"
            name="productPrice"
          />
        </li>
      </ul>
    </template>
    <template v-slot:footer>
      <solar-button
        type="button"
        @button:click="save"
        aria-label="save new product"
      >
        Save product</solar-button
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
import { IProductInventory, IProduct } from "@/types/Product";
import SolarButton from "@/components/SolarButton.vue";
import SolarModal from "@/components/modals/SolarModal.vue";

@Options({
  name: "NewProductModal",
  components: { SolarButton, SolarModal },
})
export default class NewProductModal extends Vue {
  newProduct: IProduct = {
    id: 0,
    createdOn: new Date(),
    updatedOn: new Date(),
    isArchived: false,
    isTaxable: false,
    name: "",
    description: "",
    price: 0,
  };
  close() {
    this.$emit("close");
  }
  save() {
    this.$emit("save:product", this.newProduct);
  }
}
</script>

<style scoped lang="scss">
@import "@/scss/global.scss";

.new-product {
  list-style: none;
  padding: 0;
  margin: 0;
  input {
    width: 100%;
    height: 1.8rem;
    margin-bottom: 1.2rem;
    font-size: 1.1rem;
    line-height: 1.3rem;
    padding: 0.2rem;
    color: #555;
  }
  label {
    font-weight: bold;
    display: block;
    margin-bottom: 0.3rem;
  }
}
</style>
