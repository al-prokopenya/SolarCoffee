<template>
  <div class="hello">
    <h1>{{ msg }}</h1>
    <h1>{{ myNumber }}</h1>
  </div>
  <button @click="incrementNumber">Click me</button>
</template>

<script lang="ts">
import { Options, Vue } from "vue-class-component";

@Options({
  props: {
    msg: String,
  },
})
export default class HelloWorld extends Vue {
  msg!: string;
  myNumber = 0;

  incrementNumber() {
    this.myNumber += 1;
  }

  created() {
    console.log("created!");
  }

  mounted() {
    console.log("mounted!");
  }

  destroyed() {
    console.log("destroyed!");
  }
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped lang="scss">
h3 {
  margin: 40px 0 0;
}
ul {
  list-style-type: none;
  padding: 0;
}
li {
  display: inline-block;
  margin: 0 10px;
}
a {
  color: #42b983;
}
</style>
