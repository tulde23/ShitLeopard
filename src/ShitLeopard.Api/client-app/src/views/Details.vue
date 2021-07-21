<template>
<v-card style="padding:0px; border:none">
    <v-toolbar dark color="info" dark>

        <v-toolbar-title>Episode {{selectedDialog.episodeNumber}}, Season {{selectedDialog.seasonId}}</v-toolbar-title>
        <v-spacer></v-spacer>

    </v-toolbar>
    <v-card-title class="text-xs-center pb-5">{{ selectedDialog.episodeTitle}}</v-card-title>
    <v-card-subtitle>
        {{ selectedDialog.synopsis}}

    </v-card-subtitle>
    <v-card-text>

        <p v-for="(d,i) in adjacentText" :key="i">
            <i>
                <text-highlight :queries="highlightedText"> {{ d.body }} </text-highlight>
            </i>
        </p>

        <p>

            <v-slider v-model="distance" label="Distance From Search Term" class="align-center" max="10" min="1" :thumb-size="24" thumb-label="always">

            </v-slider>
        </p>
    </v-card-text>
    <v-card-actions>
        <v-btn text color="success" @click="closeDetails()">
            close
        </v-btn>
    </v-card-actions>
</v-card>
</template>

<script lang="ts" src="./Details.ts">

</script>
