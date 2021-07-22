<template>
  <landing />
</template>

<script lang="ts">
import Vue from "vue";
import Landing from "../components/Landing.vue";

export default Vue.extend({
  name: "Home",

  components: {
    Landing,
  },
});
</script>
