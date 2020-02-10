<template>
<v-container>
  
    <v-layout row wrap>
        <v-flex xs12 sm12 md12 buffer>
            <v-text-field label="Solo" solo placeholder="Ask me a question you greasy bastard" append-icon="search" v-model="question" clearable @keyup.enter.native="search" @click:append="search"></v-text-field>

        </v-flex>

        <v-flex xs12 sm6 md3 buffer>
            <div v-if="lines">
                <ul>
                    <li v-for="(l,i) in lines" :key="i">
                        {{l.body}}
                    </li>
                </ul>
            </div>

        </v-flex>

    </v-layout>
</v-container>
</template>

<script lang="ts" src="./Home.ts">

</script>
