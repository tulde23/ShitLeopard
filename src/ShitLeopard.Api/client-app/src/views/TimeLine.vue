<template>
<v-container>

    <v-timeline>
        <v-timeline-item v-for="(season, i) in episodes" :key="i" color="red" small>
            <template v-slot:opposite>
                <span :class="`headline font-weight-bold red--text`" v-text="season.season"></span>
            </template>
            <div class="py-4">

                <v-timeline>
                    <v-timeline-item v-for="(episode, t) in season.episodes" :key="t" color="green" small>
                        <template v-slot:opposite>
                            <span :class="`headline font-weight-bold greem--text`" v-text="episode.offsetId"></span>
                        </template>
                        <div class="py-4">
                            <h2 :class="`headline font-weight-light mb-4 red--text`">{{episode.title}}</h2>

                        </div>
                    </v-timeline-item>
                </v-timeline>
            </div>
        </v-timeline-item>
    </v-timeline>

</v-container>
</template>

<script lang="ts" src="./TimeLine.ts">

</script>

<style scoped src="./TimeLine.scss" lang="scss">

</style>
