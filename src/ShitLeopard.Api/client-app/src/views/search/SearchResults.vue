<template>
  <sl-content>
    <sl-header>
      <template v-slot:content>
        <v-text-field
          label="search, you greasy bastard"
          align="center"
          append-icon="mdi-magnify"
          v-model="question.text"
          hide-details
          single-line
          clearable
          @keyput.enter.native="search"
          @click:append="search"
          outlined
          dense
          class="white"
        ></v-text-field>
      </template>
    </sl-header>

    <div class="mx-5 my-auto" v-force-top>
      <template v-for="(d, i) in dialogLines">
        <v-card class="mr-5" style="background-color:transparent; border-color:#0d0c18ff;" :key="i">
          <v-list-item three-line class="m-0" v-if="medium">
            <v-list-item-content>
              <div class="text-overline mb-4 white--text">
                {{ d.showName }}
              </div>
              <v-list-item-title class="text-overline mb-1 white--text">
                {{ d.episodeTitle }} - s{{ d.seasonId }}, e{{ d.episodeNumber }}
              </v-list-item-title>
              <v-list-item-title class="text-overline mb-1 white--text"> </v-list-item-title>

              <p v-for="(l, i) in d.lines" class="white--text">
                <i>
                  <text-highlight :queries="query"> {{ l }} </text-highlight>
                </i>
              </p>
            </v-list-item-content>
            <v-list-item-content
              right
              class="d-none d-md-block white--text"
              v-if="d.synopsis && d.synopsis.length > 1"
            >
              <div class="text-overline mb-4 white--text">
                synopsis
              </div>
              <p class="white--text">
                {{ d.synopsis }}
              </p>
            </v-list-item-content>
          </v-list-item>
          <div v-else>
            <div class="text-overline mb-1 white--text">
              {{ d.showName }}
            </div>
            <div class="text-overline mb-1 white--text">
              {{ d.episodeTitle }} - s{{ d.seasonId }}, e{{ d.episodeNumber }}
            </div>
            <div class="d-flex flex-column align-stretch">
              <p
                v-for="(l, i) in d.lines"
                class="white--text d-flex flex-1 align-self-stretch"
                style="margin:0; padding:0"
              >
                <i>
                  <text-highlight :queries="query"> {{ l }} </text-highlight>
                </i>
              </p>
            </div>
            <a class="text-overline mb-4 " v-on:click="toggle(i)">
              show synopsis
            </a>
            <p
              :id="`synopsis${i}`"
              :class="{ 'white--text': true, 's-hidden': !isVisible(i) }"
              :ref="`synopsis${i}`"
            >
              {{ d.synopsis }}
            </p>
          </div>

          <v-card-actions> </v-card-actions>
        </v-card>
        <v-divider color="white" class="mr-5"></v-divider>
      </template>
    </div>
  </sl-content>
</template>

<script lang="ts" src="./SearchResults.ts"></script>
<style scoped src="./SearchResults.scss" lang="scss"></style>
