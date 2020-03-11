<template>
<v-alert dense v-if="quote" border="left" colored-border color="primary accent-4" elevation="2" transition="fade-transition">

    <v-row align="center">

        <v-col class="grow">

            <v-tooltip bottom>
                <template v-slot:activator="{ on }">
                    <span v-on="on">
                        {{quote.body}}

                    </span>
                </template>
                <span>{{quote.episodeTitle}} {{quote.episodeId}}</span>
            </v-tooltip>

        </v-col>
        <v-col class="shrink">
            <v-btn-toggle dense rounded>
                <v-btn @click="refresh" color="red" fab x-small title="Get a new quote pussy tits!">
                    <v-icon color="white">mdi-refresh</v-icon>
                </v-btn>
                <v-btn @click="upvote" color="blue" fab x-small title="Like this quote">
                    <v-icon color="white">mdi-thumb-up-outline</v-icon>
                </v-btn>
            </v-btn-toggle>

        </v-col>
    </v-row>
</v-alert>
</template>

<script lang="ts" src="./RandomQuote.ts">

</script>

<style scoped src="./RandomQuote.scss" lang="scss">

</style>
