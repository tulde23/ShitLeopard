<template>
      <v-icon v-if="type=='material'">{{name}}</v-icon>
      <font-awesome-icon v-else v-bind:icon="name" />

</template>
<script lang="ts" src="./Metaphor.ts">

</script>