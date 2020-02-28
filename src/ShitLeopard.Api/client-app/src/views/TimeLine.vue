<template>
<v-container>
    <v-chip-group column active-class="primary--text">
        <v-chip v-for="(s,i) in episodes" :key="s.id">

            <a :href="`#alert${i}`">
                {{ s.season }}
            </a>

        </v-chip>
    </v-chip-group>

    <v-sheet v-for="(season, i) in episodes" :key="i">

        <v-alert color="primary" dark border="left" prominent :id="`alert${i}`">
            {{season.season}} - ({{season.episodes.length}}) episodes.
        </v-alert>

        <v-timeline class="d-none d-md-block">
            <v-timeline-item v-for="(e, i) in season.episodes" :key="i" color="red" small left>
                <span slot="opposite"> {{season.season}} - Episode {{e.offsetId}} / {{e.id}}</span>

                <v-card>

                    <v-card-title class="red lighten-2">
                        <v-icon dark size="18" class="mr-1">
                            mdi-information
                        </v-icon>
                        <div class="white--text font-weight-light">{{e.title}}</div>
                    </v-card-title>
                    <v-container>
                        <v-row>
                            <v-col cols="12" md="12" class="text-xs-left">
                                {{e.synopsis}}
                            </v-col>

                        </v-row>
                    </v-container>
                </v-card>

            </v-timeline-item>
        </v-timeline>

        <div class="d-flex d-sm-none">

            <v-simple-table dense>
                <template v-slot:default>

                    <tbody v-for="(e, i) in season.episodes" :key="i">
                        <tr>
                            <td>
                                <p  class="subtitle-1">
                                  {{e.offsetId}}).  {{e.title}}
                                </p>
                            </td>

                        </tr>
                        <tr>
                            <td>
                               <p style="font-size:0.75em"> {{e.synopsis}} </p>

                            </td>
                        </tr>
                        
                    </tbody>
                </template>
            </v-simple-table>

        </div>
     
        <br>
    </v-sheet>

</v-container>
</template>

<script lang="ts" src="./TimeLine.ts">

</script>

<style scoped src="./TimeLine.scss" lang="scss">

</style>
