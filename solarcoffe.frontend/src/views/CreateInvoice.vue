<template>
  <div>
    <h1 id="invoice-title">Create Invoice</h1>
    <hr />
    <div class="invoice-step" v-if="invoiceStep == 1">
      <h2>Step 1: Select Customer</h2>
      <div v-if="customers.length" class="invoice-step-detail">
        <label for="customer">Customer:</label>
        <select
          v-model="selectedCustomerId"
          class="invoiceCustomer"
          id="customer"
        >
          <option disabled value="">Please select a customer</option>
          <option v-for="c in customers" :value="c.id" :key="c.id">
            {{ c.name + " " + c.lastName }}
          </option>
        </select>
      </div>
    </div>
    <div class="invoice-step" v-if="invoiceStep == 2">
      <h2>Step 2: Create order</h2>
      <div v-if="inventory.length" class="invoice-step-detail">
        <label for="product">Product:</label>
        <select v-model="newItem.product" class="invoiceLineItem" id="product">
          <option disabled value="">Please select a product</option>
          <option v-for="i in inventory" :value="i.product" :key="i.product.id">
            {{ i.product.name }}
          </option>
        </select>
        <label for="quantity">Quantity:</label>
        <input type="number" v-model="newItem.quantity" id="quantity" min="1" />
      </div>
      <div class="invoice-item-actions">
        <solar-button
          :disabled="!newItem.product || !newItem.quantity"
          @button:click="addLineItem"
        >
          Add Line Item
        </solar-button>
        <solar-button
          :disabled="!lineItems.length"
          @button:click="finalizeOrder"
        >
          Finalize Order</solar-button
        >
      </div>

      <div class="invoice-order-list" v-if="lineItems.length">
        <div class="runningTotal">
          <h3>Running Total</h3>
          {{ $filters.price(runningTotal) }}
        </div>
        <hr />
        <table class="table">
          <tr>
            <th>Product Name</th>
            <th>Price</th>
            <th>Quantity</th>
            <th>Subtotal</th>
          </tr>
          <tr v-for="item in lineItems" :key="item.product?.id">
            <td>{{ item.product?.name }}</td>
            <td>{{ $filters.price(item.product?.price) }}</td>
            <td>{{ item.quantity }}</td>
            <td>
              {{
                $filters.price(
                  item.quantity * (item.product ? item.product.price : 0)
                )
              }}
            </td>
          </tr>
        </table>
      </div>
    </div>
    <div class="invoice-step" v-if="invoiceStep == 3">
      <h2>Step 3: Review and submit</h2>
      <solar-button @button:click="submitInvoice">
        Submit invoice
      </solar-button>
      <hr />
      <div class="invoice-step-detail" id="invoice" ref="invoice">
        <div class="invoice-logo">
          <img
            src="../assets/images/solar_coffee_logo.png"
            id="imgLogo"
            alt="logo"
          />
        </div>
        <h3>11123 Solar Line</h3>
        <h3>San Somwhere, CA 90000</h3>
        <h3>USA</h3>
        <div class="invoice-order-list">
          <div class="invoice-header">
            <h3>Invoice: {{ $filters.humanizeDate(new Date()) }}</h3>
            <h3>
              Customer:
              {{ selectedCustomer?.name + " " + selectedCustomer?.lastName }}
            </h3>
            <h3>
              Address:
              {{ selectedCustomer?.primaryAddress.addressLine1 }}
            </h3>
            <h3 v-if="selectedCustomer?.primaryAddress.addressLine2">
              {{ selectedCustomer?.primaryAddress.addressLine2 }}
            </h3>
            <h3>
              {{ selectedCustomer?.primaryAddress.city }},
              {{ selectedCustomer?.primaryAddress.state }},
              {{ selectedCustomer?.primaryAddress.zipCode }}
            </h3>
            <h3>{{ selectedCustomer?.primaryAddress.country }}</h3>
          </div>
          <table class="table">
            <thead>
              <tr>
                <th>Product Name</th>
                <th>Price</th>
                <th>Quantity</th>
                <th>Subtotal</th>
              </tr>
            </thead>
            <tr v-for="item in lineItems" :key="item.product?.id">
              <td>{{ item.product?.name }}</td>
              <td>{{ $filters.price(item.product?.price) }}</td>
              <td>{{ item.quantity }}</td>
              <td>
                {{
                  $filters.price(
                    item.quantity * (item.product ? item.product.price : 0)
                  )
                }}
              </td>
            </tr>
            <tr>
              <th colspan="3"></th>
              <th>Grand Total</th>
            </tr>
            <tfoot>
              <td colspan="3" class="due">Balance due upon receipt:</td>
              <td class="price-final">{{ $filters.price(runningTotal) }}</td>
            </tfoot>
          </table>
        </div>
      </div>
    </div>

    <hr />
    <div class="invoice-steps-actions">
      <solar-button @button:click="prev" :disabled="!canGoPrev"
        >Previous ({{ canGoPrev }})</solar-button
      >
      <solar-button @button:click="next" :disabled="!canGoNext"
        >Next ({{ canGoNext }})</solar-button
      >
      <solar-button @button:click="startOver">Start over</solar-button>
    </div>
  </div>
</template>

<script lang="ts">
import SolarButton from "@/components/SolarButton.vue";
import { CustomerService } from "@/services/customer-service";
import { InventoryService } from "@/services/inventory-service";
import InvoiceService from "@/services/invoice-service";
import { ProductService } from "@/services/product-service";
import { ICustomer } from "@/types/Customer";
import { IInvoice, ILineItem } from "@/types/Invoice";
import { IProductInventory } from "@/types/Product";

import jsPDF from "jspdf";
import html2canvas from "html2canvas";

const customerService = new CustomerService();
const inventoryService = new InventoryService();
const invoiceService = new InvoiceService();

const emprtyProduct = {
  id: 0,
  description: "",
  isArchived: false,
  price: 0,
  isTaxable: false,
  name: "",
  createdOn: new Date(),
  updatedOn: new Date(),
};
import { Options, Vue } from "vue-class-component";

@Options({
  name: "CreateInvoice",
  components: { SolarButton },
})
export default class CreateInvoice extends Vue {
  $filters: any;

  invoiceStep = 1;
  invoice: IInvoice = {
    createdOn: new Date(),
    updatedOn: new Date(),
    customerId: 0,
    lineItems: [],
  };

  customers: ICustomer[] = [];
  selectedCustomerId = 0;
  inventory: IProductInventory[] = [];
  lineItems: ILineItem[] = [];
  newItem: ILineItem = {
    quantity: 0,
    product: emprtyProduct,
  };

  addLineItem() {
    const lineItem = this.lineItems.find(
      (item) => item.product?.id === this.newItem.product?.id
    );

    if (lineItem !== undefined) {
      lineItem.quantity += this.newItem.quantity;
    } else {
      this.lineItems.push({ ...this.newItem });
    }

    this.newItem = { quantity: 0, product: emprtyProduct };
    console.log(this.lineItems);
  }

  finalizeOrder() {
    this.invoiceStep = 3;
  }

  get selectedCustomer() {
    return this.customers.find((c) => c.id === this.selectedCustomerId);
  }

  get runningTotal() {
    let total = this.lineItems.reduce(
      (sum, item) =>
        sum + item.quantity * (item.product ? item.product.price : 0),
      0
    );

    return total;
  }

  get canGoNext() {
    if (this.invoiceStep === 1) {
      return this.selectedCustomerId > 0;
    }

    if (this.invoiceStep === 2) {
      return this.lineItems.length > 0;
    }

    return false;
  }

  get canGoPrev() {
    return this.invoiceStep !== 1;
  }

  async submitInvoice() {
    this.invoice = {
      customerId: this.selectedCustomerId,
      createdOn: new Date(),
      updatedOn: new Date(),
      lineItems: this.lineItems,
    };

    await invoiceService.makeNewInvoice(this.invoice);

    this.downloadPdf();
    await this.$router.push("/orders/");
  }

  async downloadPdf() {
    const pdf = new jsPDF("p", "pt", "a4", true);
    const invoice = document.getElementById("invoice");

    const width = (this.$refs["invoice"] as any).clientWidth;
    const height = (this.$refs["invoice"] as any).clientHeight;

    const canvas = await html2canvas(invoice as HTMLElement);
    const image = canvas.toDataURL("image/png");
    pdf.addImage(image, "PNG", 100, 100, width * 0.99, height * 0.99);
    pdf.save("invoice");
  }

  startOver() {
    this.selectedCustomerId = 0;
    this.lineItems = [];
    this.invoiceStep = 1;
  }
  prev() {
    if (this.invoiceStep == 1) return;

    this.invoiceStep--;
  }
  next() {
    if (this.invoiceStep == 3) return;

    this.invoiceStep++;
  }

  async fetchData() {
    this.customers = await customerService.getCustomers();
    this.inventory = await inventoryService.getInventory();
  }

  async created() {
    await this.fetchData();
  }
}
</script>

<style scoped lang="scss">
@import "@/scss/global.scss";

.invoice-steps-actions {
  display: flex;
  width: 100%;
}
.invoice-step {
}
.invoice-step-detail {
  margin: 1.2rem;
}
.invoice-order-list {
  margin-top: 1.2rem;
  padding: 0.8rem;
  .line-item {
    display: flex;
    border-bottom: 1px dashed #ccc;
    padding: 0.8rem;
  }
  .item-col {
    flex-grow: 1;
  }
}
.invoice-item-actions {
  display: flex;
}
.price-pre-tax {
  font-weight: bold;
}
.price-final {
  font-weight: bold;
  color: $solar-green;
}
.due {
  font-weight: bold;
}
.invoice-header {
  width: 100%;
  margin-bottom: 1.2rem;
}
.invoice-logo {
  padding-top: 1.4rem;
  text-align: center;
  img {
    width: 280px;
  }
}
</style>
