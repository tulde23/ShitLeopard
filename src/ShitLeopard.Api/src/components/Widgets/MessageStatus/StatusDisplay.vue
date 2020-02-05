<template>
 <span v-if="level===0 || level ==='Unknown'" class="status-0" >{{text}}</span>
  <span v-else-if="level===1 || level ==='Running'" class="status-1" >{{text}}</span>
 <span v-else-if="level===10 || level === 'CompletedSuccessfully'" class="status-10" >{{text}}</span>
 <span v-else-if="level===20 || level === 'CompletedWithErrors'" class="status-20" >{{text}}</span>
 <span v-else-if="level===99 || level === 'Failed'" class="status-99">{{text}}</span>
  <span v-else-if=" level === 'Cancelled'" class="status-20">{{text}}</span>
   <span v-else-if=" level === 'Completed'" class="status-10">{{text}}</span>
    <span v-else-if=" level === 'Pending'" class="status-1">{{text}}</span>

  <span v-else class="status-20">Completed With Errors</span>
</template>
<script lang="ts" src="./StatusDisplay.ts">

</script>
<style scoped>
.status-0{
 color:red;
}
.status-1{
  color:blue;
}
.status-10{
 color:green;
}
.status-20{
  color:orange;
}
.status-99{
    color:red;
}
</style>

