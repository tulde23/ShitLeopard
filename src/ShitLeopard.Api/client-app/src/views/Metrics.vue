<template>
<v-container>

    <p>
        view site metrics.
    </p>

    <v-data-table :headers="viewModel.headers" :items="model.result" :options.sync="options" :rows-per-page-items="viewModel.rowsPerPageItems" :server-items-length="model.count" :single-expand="singleExpand" :expanded.sync="expanded" :loading="busy" class="elevation-1">
     <template v-slot:expanded-item="{ headers }">
      <td :colspan="headers.length">Peek-a-boo!</td>
    </template>
    </v-data-table>

</v-container>
</template>

<!-- 
  public headers?: any,
    public ipaddress?: string,
    public agentString?: string,
    public route?: string,
    public lastAccessTime?: string
-->

<script lang="ts" src="./Metrics.ts">

</script>
