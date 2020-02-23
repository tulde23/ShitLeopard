<template>
<v-container>
    <v-badge color="red" left>
        <template v-slot:badge>
            <span>{{answer.popularity}}</span>
        </template>

    </v-badge>
    <p v-if="quote">
        {{quote.body}}
    </p>
    <v-btn @click="refresh" fab small>
        <v-icon>refresh</v-icon>
    </v-btn>
    <v-btn color="blue" @click="upvote" fab small>
        <v-icon>thumb_up</v-icon>

    </v-btn>

</v-container>
</template>

<script lang="ts" src="./RandomQuote.ts">

</script>

<style scoped src="./RandomQuote.scss" lang="scss">

</style>
