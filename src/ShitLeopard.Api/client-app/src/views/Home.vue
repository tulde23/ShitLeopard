<template>
<v-container>

    <v-text-field label="Solo" solo placeholder="Ask me a question you greasy bastard" append-icon="mdi-search" v-model="question" clearable @keyup.enter.native="search" @click:append="search"></v-text-field>
    <v-chip-group column color="accent">
        <v-chip v-for="tag in tags" :key="tag.id" @click="question = tag.name">

            <v-avatar left color="info lighten-1" style="color:white">
                {{ tag.frequency }}
            </v-avatar>
            {{ tag.name }}
        </v-chip>
    </v-chip-group>

    <v-alert v-if="response && response.comment" dense type="warning">
        {{response.comment}}
    </v-alert>
    <!--  <v-simple-table dense v-if="response && response.isArray">
        <template v-slot:default>

            <tbody>
                <tr v-for="item in lines" :key="item.id">
                    <td class="text-xs-left">{{ item.body }}</td>
                    <td style="font-size:0.75em">s{{item.seasonId}}e{{item.offsetId}}</td>
                    <td style="font-size:0.75em">
                        {{item.episodeTitle}}

                    </td>
                    <td style="padding:2px">
                        <v-btn color="brown" @click="upvote(item)" fab x-small title="Like this quote">
                            <v-icon color="white">mdi-thumb-up-outline</v-icon>
                        </v-btn>
                    </td>
                </tr>
            </tbody>
        </template>
    </v-simple-table> -->
    <template v-if="response && response.isArray">
        <v-data-table :headers="viewModel.headers" :items="lines" item-key="id" :loading="busy" class="elevation-1">

            <template v-slot:item.seasonId="{ item }">
                s{{item.seasonId}}e{{item.offsetId}}
            </template>
            <template v-slot:item.episodeId="{ item }">
                <v-btn color="success" @click="upvote(item)" fab x-small title="Like this quote">
                    <v-icon color="white">mdi-thumb-up-outline</v-icon>
                </v-btn>
            </template>

        </v-data-table>
    </template>

    <div v-if="response && !response.isArray">
        {{answer}}
    </div>

</v-container>
</template>

<script lang="ts" src="./Home.ts">

</script>
