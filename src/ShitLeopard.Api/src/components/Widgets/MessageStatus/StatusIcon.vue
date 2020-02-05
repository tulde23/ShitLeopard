<template>
 <v-icon v-if="level===0 || level ==='Unknown'" color="red" title="Unkown">priority_high</v-icon>
  <v-icon v-else-if="level===1 || level ==='Running'" color="blue" title="Running">loop</v-icon>
 <v-icon v-else-if="level===10 || level === 'CompletedSuccessfully'" color="green" title="Completed">done</v-icon>
 <v-icon v-else-if="level===20 || level === 'CompletedWithErrors'" color="orange" title="Completed With Errors">warning</v-icon>
 <v-icon v-else-if="level===99 || level === 'Failed'" color="red" title="Failed">error</v-icon>
   <v-icon v-else-if=" level === 'Cancelled'" color="orange" title="Cancelled">warning</v-icon>
   <v-icon v-else-if=" level === 'Completed'" color="green" title="Completed">done</v-icon>
    <v-icon v-else-if=" level === 'Pending'" color="blue" title="Pending">loop</v-icon>
  <v-icon v-else color="orange">warning</v-icon>
</template>
<script lang="ts" src="./StatusIcon.ts">

</script>
