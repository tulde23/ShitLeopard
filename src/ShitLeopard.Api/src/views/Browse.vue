<template>
<v-container>
    <v-select :items="episodes" @change="setEpisode($event)" clearable v-model="episode" label="Episodes" class="input-group--focused" item-text="title" item-value="id">
    </v-select>
    <br>

    <div v-if="selectedEpisode && selectedEpisode.script">

        <v-data-table :items="selectedEpisode.script[0].scriptLine" item-key="id" :rows-per-page-items="gridModel.rowsPerPageItems" v-bind:pagination.sync="gridModel.pagination" class="elevation-1" hide-headers>
            <template v-slot:items="props">

                <td class="text-xs-left">{{ props.item.body }}</td>
                <td>
                    <v-select :items="characters" @change="saveLine(props.item)" clearable v-model="props.item.characterId" label="Said By" class="input-group--focused" item-text="name" item-value="id">
                    </v-select>
                </td>
                <td>
                    <v-btn color="green" @click="upvote(props.item)" fab small title="Like this quote">
                        <v-icon>thumb_up</v-icon>
                    </v-btn>
                </td>

            </template>
        </v-data-table>
    </div>

</v-container>
</template>

<script lang="ts" src="./Browse.ts">

</script>
