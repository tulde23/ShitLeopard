<template>
  <sl-content>
    <sl-header>
      <template v-slot:title>
        Shit Shows
      </template>
    </sl-header>

    <div class="mt-5 ml-2 mr-2">
      <v-card
        class=" mb-5"
        width="75%"
        outlined
        v-for="(d, i) in shows"
        :key="i"
        style="background-color:#0d0c18ff; border-color:white;"
      >
        <v-list-item three-line>
          <v-list-item-content>
            <div class="text-overline mb-4 white--text">
              {{ d.title }}
            </div>
            <div class="mb-4 white--text">
              {{ d.description }}
            </div>
          </v-list-item-content>
        </v-list-item>

        <v-card-actions>
          <router-link :to="getEpisodePath(d.id)">
            Go to Episodes
          </router-link>
        </v-card-actions>
      </v-card>
    </div>
  </sl-content>
</template>

<script lang="ts" src="./Shows.ts"></script>

<style scoped src="./Shows.scss" lang="scss"></style>
