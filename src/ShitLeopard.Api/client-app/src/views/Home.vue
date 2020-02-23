<template>
<v-container>

    <v-text-field label="Solo" solo placeholder="Ask me a question you greasy bastard" append-icon="mdi-search" v-model="question" clearable @keyup.enter.native="search" @click:append="search"></v-text-field>

    <v-simple-table dense>
        <template v-slot:default>
           
            <tbody>
                <tr v-for="item in lines" :key="item.id">
                    <td class="text-xs-left">{{ item.body }}</td>
                    <td>s{{item.seasonId}}e{{item.episodeId}}</td>
                    <td>
                        <p style="font-size:0.75em">
                            {{item.episodeTitle}}
                        </p>
                    </td>
                    <td>
                        <v-btn color="brown" @click="upvote(item)" fab small title="Like this quote">
                            <v-icon>mdi-thumb-up-outline</v-icon>
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
