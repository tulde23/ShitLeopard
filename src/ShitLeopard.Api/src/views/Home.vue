<template>
<v-container>

    <v-text-field label="Solo" solo placeholder="Ask me a question you greasy bastard" append-icon="search" v-model="question" clearable @keyup.enter.native="search" @click:append="search"></v-text-field>

    <v-data-table :items="lines" class="elevation-1" hide-actions hide-headers>
        <template v-slot:items="props">

            <td class="text-xs-left">{{ props.item.body }}</td>
            <td>s{{props.item.seasonId}}e{{props.item.episodeId}}</td>
            <td>
            <p style="font-size:0.75em">
            {{props.item.episodeTitle}}
            </p>
            </td>
            <td>
                <v-btn color="brown" @click="upvote(props.item)" fab small title="Like this quote">
                    <v-icon>thumb_up</v-icon>
                </v-btn>
            </td>
        </template>
    </v-data-table>
   

</v-container>
</template>

<script lang="ts" src="./Home.ts">

</script>
