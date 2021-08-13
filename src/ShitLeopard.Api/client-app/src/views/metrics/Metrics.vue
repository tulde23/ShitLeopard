<template>
  <sl-content>
    <sl-header>
      <template v-slot:title>
        Shit Metrics
      </template>
    </sl-header>

    <div>
      <v-data-table
        dark
        :headers="viewModel.headers"
        :items="metrics"
        item-key="id"
        :items-per-page="25"
        class="elevation-1 dark-bg"
      ></v-data-table>
    </div>
  </sl-content>
</template>

<script lang="ts" src="./Metrics.ts"></script>

<style lang="scss" scoped>
.dark-bg {
  background-color: #0d0c18ff;
}

.dark-bg th {
  color: white;
}
</style>
