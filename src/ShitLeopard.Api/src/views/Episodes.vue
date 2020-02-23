<template>
<v-container>

    <br>

    <v-expansion-panel>
        <v-expansion-panel-content v-for="(item,i) in episodes" :key="i">
            <template v-slot:header>
                <div>{{item.season}}</div>
            </template>
            <v-card>
                <v-data-table :items="item.episodes" :key="seasonId" hide-actions hide-headers>
                    <template v-slot:items="props">

                        <tr>
                            <td class="text-xs-left episode">
                                Episode#: {{props.item.id}}
                            </td>
                            <td class="text-xs-left episode-offset">
                                Season#: {{props.item.offsetId}}
                            </td>
                            <td class="text-xs-left episode-title">
                                {{props.item.title}}
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" class="text-xs-left episode-synopis">
                                <p> {{props.item.synopsis}}</p>
                            </td>
                        </tr>
                    
                    </template>
                     </v-data-table>
            </v-card>
        </v-expansion-panel-content>
    </v-expansion-panel>
</v-container>
</template>

<script lang="ts" src="./Episodes.ts">

</script>

<style scoped src="./Episodes.scss" lang="scss">

</style>
