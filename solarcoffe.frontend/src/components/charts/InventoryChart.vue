<template>
  <div class="inventory-chart">
    <apexchart
      type="area"
      :width="'100%'"
      height="300"
      :options="options"
      :series="series"
    >
    </apexchart>
  </div>
</template>

<script lang="ts">
import { IInventoryTimeline } from "@/types/InventoryGraph";
import { Options, Vue } from "vue-class-component";
import { Sync, Get } from "vuex-pathify";

@Options({
  name: "InventoryChart",
  components: {},
})
export default class InventoryChart extends Vue {
  @Sync("shapshotTimeLine")
  shapshotTimeLine?: IInventoryTimeline;

  @Get("isTimelineBuilt")
  timelineBuilt?: boolean;

  get options() {
    return {
      dataLabels: { enabled: false },
      fill: { type: "gradient" },
      stroke: { curve: "smooth" },
      xaxis: {
        categories: this.shapshotTimeLine?.timeline,
        type: "datetime",
      },
    };
  }
  get series() {
    return this.shapshotTimeLine?.productInventorySnaphots.map((snap) => ({
      name: snap.productId,
      data: snap.quantityOnHand,
    }));
  }

  $store: any;

  async created() {
    await this.$store.dispatch("assignSnaphots");
  }
}
</script>

<style lang="scss">
@import "@/scss/global.scss";
</style>
