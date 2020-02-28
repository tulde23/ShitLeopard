<template>
<v-container>
    <v-select :items="episodes" @change="setEpisode($event)" clearable v-model="episode" label="Episodes" class="input-group--focused" item-text="title" item-value="id">
    </v-select>
    <br>

    <div v-if="selectedEpisode && selectedEpisode.script">

        <v-data-table :items="selectedEpisode.script[0].scriptLine" item-key="id" dense>
            <template v-if="busy" v-slot:progress>
                <v-progress-linear color="purple" :height="10" indeterminate></v-progress-linear>
            </template>

            <template v-slot:body="{ items }">
                <tbody>
                    <tr v-for="e in items" :key="e.id">
                        <td class="text-xs-left">{{ e.body }}</td>
                        <td>
                            <v-select :items="characters" @change="saveLine(e)" clearable v-model="e.characterId" label="Said By" class="input-group--focused" item-text="name" item-value="id">
                            </v-select>
                        </td>
                        <td>
                            <v-btn color="green" @click="upvote(e)" fab x-small title="Like this quote">
                                <v-icon color="white">mdi-thumb-up-outline</v-icon>
                            </v-btn>
                        </td>
                    </tr>
                </tbody>
            </template>
        </v-data-table>
    </div>

</v-container>
</template>

<script lang="ts" src="./Browse.ts">

</script>
