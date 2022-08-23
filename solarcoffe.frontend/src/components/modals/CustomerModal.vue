<template>
  <solar-modal>
    <template v-slot:header> New customer </template>
    <template v-slot:body>
      <ul class="newCustomer">
        <li>
          <label for="name">Customer Name</label>
          <input
            type="text"
            id="customerName"
            v-model="newCustomer.name"
            name="customerName"
          />
        </li>
        <li>
          <label for="customerLastName">Last name</label>
          <input
            type="text"
            id="customerLastName"
            v-model="newCustomer.lastName"
            name="customerLastName"
          />
        </li>
        <li>
          <label for="addressLine1">Address Line 1</label>
          <input
            type="text"
            id="addressLine1"
            v-model="newCustomer.primaryAddress.addressLine1"
            name="addressLine1"
          />
        </li>
        <li>
          <label for="addressLine2">Address Line 2</label>
          <input
            type="text"
            id="addressLine2"
            v-model="newCustomer.primaryAddress.addressLine2"
            name="addressLine2"
          />
        </li>
        <li>
          <label for="zipCode">Zip code</label>
          <input
            type="text"
            id="zipCode"
            v-model="newCustomer.primaryAddress.zipCode"
            name="zipCode"
          />
        </li>
        <li>
          <label for="country">Country</label>
          <input
            type="text"
            id="country"
            v-model="newCustomer.primaryAddress.country"
            name="country"
          />
        </li>
        <li>
          <label for="city">City</label>
          <input
            type="text"
            id="city"
            v-model="newCustomer.primaryAddress.city"
            name="city"
          />
        </li>
        <li>
          <label for="state">State</label>
          <input
            type="text"
            id="state"
            v-model="newCustomer.primaryAddress.state"
            name="state"
          />
        </li>
      </ul>
    </template>
    <template v-slot:footer>
      <solar-button
        type="button"
        @button:click="save"
        aria-label="save new customer"
      >
        Save customer</solar-button
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

import { ICustomer } from "@/types/Customer";

@Options({
  name: "CustomerModal",
  components: { SolarButton, SolarModal },
})
export default class extends Vue {
  newCustomer: ICustomer = {
    name: "",
    lastName: "",
    id: 0,
    createdOn: new Date(),
    updatedOn: new Date(),
    primaryAddress: {
      addressLine1: "",
      addressLine2: "",
      city: "",
      country: "",
      state: "",
      zipCode: "",
      createdOn: new Date(),
      id: 0,
    },
  };

  close() {
    this.$emit("close");
  }
  save() {
    console.log("save button in modal clicked");
    this.$emit("save:customer", this.newCustomer);
  }
}
</script>

<style lang="scss">
@import "@/scss/global.scss";

.newCustomer {
  display: flex;
  flex-wrap: wrap;
  list-style: none;
  padding: 0;
  margin: 0;
  input {
    width: 80%;
    height: 1.8rem;
    margin: 0.8rem 2rem 0.8rem 0.8rem;
    font-size: 1.1rem;
    line-height: 1.3rem;
    padding: 0.2rem;
    color: #555;
  }
  label {
    font-weight: bold;
    margin: 0.4rem;
    display: block;
  }
}
</style>
