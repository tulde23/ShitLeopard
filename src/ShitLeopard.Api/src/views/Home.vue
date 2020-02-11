<template>
<v-container>

    <v-layout row wrap>
        <v-flex xs12 sm12 md12 buffer>
            <v-text-field label="Solo" solo placeholder="Ask me a question you greasy bastard" append-icon="search" v-model="question" clearable @keyup.enter.native="search" @click:append="search"></v-text-field>

        </v-flex>

        <v-flex xs12 sm6 md3 buffer v-if="lines">
           
            <v-data-table :items="lines" class="elevation-1" hide-actions hide-headers>
                <template v-slot:items="props">

                    <td class="text-xs-left">{{ props.item.body }}</td>

                </template>
             
        </v-flex>

    </v-layout>
</v-container>
</template>

<script lang="ts" src="./Home.ts">

</script>
