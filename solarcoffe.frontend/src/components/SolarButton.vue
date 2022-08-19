<template>
  <div class="btn-link">
    <button @click="visitRoute">
      <slot></slot>
    </button>
  </div>
</template>

<script lang="ts">
import { Options, Vue } from "vue-class-component";

@Options({
  props: {
    link: String,
  },
  name: "SolarButton",
  component: {},
})
export default class SolarButton extends Vue {
  link?: string;

  visitRoute() {
    console.log(this.link);
    this.$router.push(this.link);
  }
}
</script>

<style scoped></style>
