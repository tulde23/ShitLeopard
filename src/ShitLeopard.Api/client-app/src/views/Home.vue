<template>
  <div>
    <v-text-field
      label="search, you greasy bastard"
      append-icon="mdi-search"
      v-model="question.text"
      clearable
      @keyup.enter.native="search"
      @click:append="search"
    ></v-text-field>
    <v-chip-group column active-class="primary--text">
      <v-chip v-for="tag in shows" :key="tag.id">
        {{ tag.title }}
      </v-chip>
    </v-chip-group>

    <div style="font-weight:bold">Super Greasy Searches:</div>
    <v-chip v-for="tag in tags" :key="tag.id" @click="searchByTag(tag.name)">
      <v-avatar left color="info lighten-1" style="color:white">
        {{ tag.frequency }}
      </v-avatar>
      {{ tag.name }}
    </v-chip>

    <template v-if="dialogLines && dialogLines.length > 0">
      <div>
        <v-card class="mx-auto">
          <v-virtual-scroll
            :items="dialogLines"
            height="600"
            item-height="64"
            transition
            name="fade-transition"
          >
            <template v-slot:default="{ item }">
              <v-list-item :key="item.id">
                <v-list-item-action>
                  <v-btn fab small depressed color="primary"> S{{ item.seasonId }} </v-btn>
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
        <v-dialog
          v-model="isOpen"
          fullscreen
          hide-overlay
          transition="dialog-bottom-transition"
          persistent
        >
          <v-card>
            <v-toolbar dark color="info" dense dark>
              <v-toolbar-title
                >Episode {{ selectedDialog.episodeNumber }}, Season
                {{ selectedDialog.seasonId }}</v-toolbar-title
              >
              <v-spacer></v-spacer>
            </v-toolbar>
            <v-card-title class="text-xs-center pb-5">{{
              selectedDialog.episodeTitle
            }}</v-card-title>
            <v-card-subtitle>
              {{ selectedDialog.synopsis }}
            </v-card-subtitle>
            <v-card-text>
              <p v-for="(d, i) in adjacentText" :key="i">
                <i :class="{ active: d.id === selectedDialog.id }">
                  <text-highlight :queries="highlightedText"> {{ d.body }} </text-highlight>
                </i>
              </p>

              <v-btn
                class="mx-2"
                icon
                dark
                color="primary"
                @click="increase()"
                v-if="distance < 10"
              >
                <v-icon dark title="More Context">
                  mdi-plus
                </v-icon>
              </v-btn>
              <v-btn
                class="mx-2"
                icon
                dark
                color="secondary"
                @click="decrease()"
                v-if="distance > 1"
              >
                <v-icon dark title="Less Context">
                  mdi-minus
                </v-icon>
              </v-btn>
              <v-btn class="mx-2" icon dark color="warning" @click="reset()" v-if="distance !== 2">
                <v-icon title="Reset">mdi-cached</v-icon>
              </v-btn>
            </v-card-text>
            <v-divider></v-divider>
            <v-card-actions>
              <v-btn color="success" @click="closeDetails()">
                close
              </v-btn>
            </v-card-actions>
          </v-card>
        </v-dialog>
      </div>
    </template>
  </div>
</template>

<script lang="ts" src="./Home.ts"></script>
<style scoped src="./Home.scss" lang="scss"></style>
