<template>
<div>
    <v-img src="/MainLogo.png" max-width="250" class="text-center"></v-img>
    <v-text-field label="search, you greasy bastard" append-icon="mdi-search" v-model="question.text" clearable @keyup.enter.native="search" @click:append="search"></v-text-field>
    <v-toolbar flat dense>
        <v-switch v-model="question.isFuzzy" color="info" label='Fuzzy Search?'></v-switch>
        <v-spacer></v-spacer>
        <v-chip color="info" dense style="color:white">Showing Results: {{resultCount}}</v-chip>
    </v-toolbar>

    <template v-if="dialogLines && dialogLines.length > 0">
        <div>

            <v-card class="mx-auto">
                <v-virtual-scroll :items="dialogLines" height="600" item-height="64" transition name="fade-transition">
                    <template v-slot:default="{ item }">
                        <v-list-item :key="item.id">
                            <v-list-item-action>
                                <v-btn fab small depressed color="primary">
                                    S{{ item.seasonId }}
                                </v-btn>
                            </v-list-item-action>

                            <v-list-item-content>
                                <v-list-item-title>

                                    <text-highlight :queries="highlightedText">{{ item.body }}</text-highlight>
                                </v-list-item-title>
                                <v-list-item-title>
                                    Episode: <strong> {{ item.episodeTitle }}</strong>
                                </v-list-item-title>

                            </v-list-item-content>

                            <v-list-item-action>

                                <v-icon @click="openDeails(item)">
                                    mdi-open-in-new
                                </v-icon>

                            </v-list-item-action>
                        </v-list-item>

                        <v-divider></v-divider>
                    </template>
                </v-virtual-scroll>
            </v-card>
            <v-dialog v-model="isOpen" fullscreen hide-overlay transition="dialog-bottom-transition" persistent>

                <v-card>
                    <v-toolbar dark color="info" dark>

                        <v-toolbar-title>Episode {{selectedDialog.episodeNumber}}, Season {{selectedDialog.seasonId}}</v-toolbar-title>
                        <v-spacer></v-spacer>

                    </v-toolbar>
                    <v-card-title class="text-xs-center pb-5">{{ selectedDialog.episodeTitle}}</v-card-title>
                    <v-card-subtitle>

                        <p v-for="(d,i) in adjacentText" :key="i">
                            <i>
                                <text-highlight :queries="highlightedText"> {{ d.body }} </text-highlight>
                            </i>
                        </p>

                        <p>

                            <v-slider v-model="distance" label="Distance From Search Term" class="align-center" max="10" min="1" :thumb-size="24" thumb-label="always">

                            </v-slider>
                        </p>
                    </v-card-subtitle>
                    <v-card-text>

                        {{ selectedDialog.synopsis}}
                    </v-card-text>
                    <v-card-actions>
                        <v-btn text color="success" @click="closeDetails()">
                            close
                        </v-btn>
                    </v-card-actions>
                </v-card>

            </v-dialog>
        </div>

    </template>

</div>
</template>

<script lang="ts" src="./Home.ts">

</script>
