<template>
<v-container>
    <v-layout>
        <v-flex xs6>
        <v-text-field label="Solo" solo placeholder="Ask me a question you greasy bastard"
            append-icon="search"
            clearable
             @click:append="search"
       
        ></v-text-field>
        </v-flex>
        
    </v-layout>
</v-container>

</template>

<script lang="ts" src="./Home.ts">

</script>
