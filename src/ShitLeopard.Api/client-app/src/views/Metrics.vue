<template>
<v-container>

    <p>
        view site metrics.
    </p>

    <v-data-table :headers="viewModel.headers" :items="model.result" :options.sync="options" :rows-per-page-items="viewModel.rowsPerPageItems" :server-items-length="model.count" :single-expand="singleExpand" :expanded.sync="expanded" single-expand show-expand item-key="id" group-by="ipaddress" :loading="busy" class="elevation-1">
        <template v-slot:expanded-item="{ headers,item }">
            <td :colspan="headers.length">
                <v-simple-table v-if="item.headers" elevation-4>
                    <template v-slot:default>

                        <tbody>
                            <tr v-for="(value, propertyName) in item.headers" :key="propertyName">
                                <td>{{propertyName}}</td>
                                <td>{{ item.headers[propertyName] }}</td>

                            </tr>
                        </tbody>
                    </template>
                </v-simple-table>

            </td>
        </template>

        <template v-slot:item.lastAccessTime="{ item }">

            {{ item.lastAccessTime | formatDateTime }}

        </template>

    </v-data-table>

</v-container>
</template>

<script lang="ts" src="./Metrics.ts">

</script>
