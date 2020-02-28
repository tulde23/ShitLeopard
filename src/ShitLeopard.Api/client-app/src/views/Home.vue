<template>
<v-container>

    <v-text-field label="Solo" solo placeholder="Ask me a question you greasy bastard" append-icon="mdi-search" v-model="question" clearable @keyup.enter.native="search" @click:append="search"></v-text-field>
    <v-chip-group column active-class="primary--text">
        <v-chip v-for="tag in tags" :key="tag.id" @click="question = tag.name">

            <v-avatar left class="green lighten-4">
                {{ tag.frequency }}
            </v-avatar>
            {{ tag.name }}
        </v-chip>
    </v-chip-group>

    <v-simple-table dense>
        <template v-slot:default>

            <tbody>
                <tr v-for="item in lines" :key="item.id" >
                    <td class="text-xs-left">{{ item.body }}</td>
                    <td style="font-size:0.75em">s{{item.seasonId}}e{{item.offsetId}}</td>
                    <td  style="font-size:0.75em">
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
    </v-simple-table>

</v-container>
</template>

<script lang="ts" src="./Home.ts">

</script>
