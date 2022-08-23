<template>
  <div class="customers-container">
    <h1 id="customers-title">Manage Customers</h1>
    <hr />
    <div class="customers-actions">
      <solar-button @button:click="showNewCustomerModal">
        Add Customer
      </solar-button>
    </div>
    <table id="customers-table" class="table">
      <tr>
        <th>Customer</th>
        <th>Address</th>
        <th>State</th>
        <th>Since</th>
        <th>Delete</th>
      </tr>
      <tr v-for="item in customers" :key="item.id">
        <td>{{ item.name + " " + item.lastName }}</td>
        <td>
          {{
            item.primaryAddress.addressLine1 +
            " " +
            item.primaryAddress.addressLine2
          }}
        </td>
        <td>{{ item.primaryAddress.state }}</td>
        <td>{{ $filters.humanizeDate(item.createdOn) }}</td>
        <td>
          <div class="customer-delete" @click="deleteCustomer(item.id)">
            <i class="lni lni-cross-circle"></i>
          </div>
        </td>
      </tr>
    </table>
    <customer-modal
      v-if="isCustomerModalVisible"
      @save:customer="saveNewCustomer"
      @close="closeModal"
    />
  </div>
</template>

<script lang="ts">
import { Options, Vue } from "vue-class-component";
import { CustomerService } from "@/services/customer-service";
import SolarButton from "@/components/SolarButton.vue";
import CustomerModal from "@/components/modals/CustomerModal.vue";
import { ICustomer } from "@/types/Customer";

const customerService = new CustomerService();

@Options({
  name: "Customers",
  components: { SolarButton, CustomerModal },
})
export default class Customers extends Vue {
  $filters: any;
  customers: ICustomer[] = [];
  isCustomerModalVisible = false;

  closeModal() {
    this.isCustomerModalVisible = false;
  }

  async fetchData() {
    this.customers = await customerService.getCustomers();
  }

  async created() {
    await this.fetchData();
  }

  async saveNewCustomer(customer: ICustomer) {
    console.log("save new cutomer triggered: " + customer);
    await customerService.addCustomer(customer);
    this.isCustomerModalVisible = false;
    this.fetchData();
  }

  async deleteCustomer(id: number) {
    await customerService.deleteCustomer(id);
    await this.fetchData();
  }

  showNewCustomerModal() {
    this.isCustomerModalVisible = true;
  }
}
</script>

<style scoped lang="scss">
@import "@/scss/global.scss";

.customer-actions {
  display: flex;
  margin-bottom: 0.8rem;
  div {
    margin-right: 0.8rem;
  }
}
.customer-delete {
  cursor: pointer;
  font-weight: bold;
  font-size: 1.2rem;
  color: $solar-red;
}
</style>
