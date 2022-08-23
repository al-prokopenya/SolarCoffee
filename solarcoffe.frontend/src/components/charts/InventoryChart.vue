<template>
  <div v-if="$store.state.isTimelineBuilt">
    <apexchart
      type="area"
      :width="'100%'"
      height="300"
      :options="options"
      :series="series"
    ></apexchart>
  </div>
</template>

<script lang="ts">
import { IInventoryTimeline } from "@/types/InventoryGraph";
import { Options, Vue } from "vue-class-component";
import VueApexCharts from "vue3-apexcharts";

@Options({
  name: "InventoryChart",
  components: { apexchart: VueApexCharts },
})
export default class InventoryChart extends Vue {
  get options() {
    let timeLine = (this.$store.state.snapshotTimeline as IInventoryTimeline)
      .timeLine;

    return {
      dataLabels: { enabled: false },
      fill: {
        type: "gradient",
      },
      stroke: {
        curve: "smooth",
      },
      xaxis: {
        categories: timeLine,
        type: "datetime",
      },
    };
  }
  get series() {
    let products = (this.$store.state.snapshotTimeline as IInventoryTimeline)
      .productInventorySnapshots;

    return products.map((snapshot) => ({
      name: snapshot.productId,
      data: snapshot.quantityOnHand,
    }));
  }

  $store: any;

  async created() {
    await this.$store.dispatch("assignSnapshots");
  }
}
</script>

<style lang="scss">
@import "@/scss/global.scss";
</style>
