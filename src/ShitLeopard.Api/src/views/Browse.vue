<template>
<v-container>
    <v-select :items="episodes" @change="setEpisode($event)" clearable v-model="episode" label="Episodes" class="input-group--focused" item-text="title" item-value="id">
    </v-select>
    <br>

    <v-list two-line subheader v-if="selectedEpisode">
        <v-subheader>Script Lines</v-subheader>

        <v-list-tile avatar v-for="(line,index) in selectedEpisode.script[0].scriptLine" :key="index">
            <v-list-tile-content>

                <v-list-tile-sub-title> {{line.body}}</v-list-tile-sub-title>
            </v-list-tile-content>
        </v-list-tile>
    </v-list>
</v-container>
</template>

<script lang="ts" src="./Browse.ts">

</script>
