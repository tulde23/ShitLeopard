<template>
  <sl-content>
    <sl-header>
      <template v-slot:title>
        Shit Episodes
      </template>
    </sl-header>

    <div class="mt-5 ml-2 mr-2 white--text ">
      {{ showName }}
      {{ episodes.seasonCount }} Seasons and {{ episodes.episodeCount }} Episodes.

      <ul>
        <li v-for="(d, i) in episodes.episodes">
          {{ d.season }}
          <ul>
            <li v-for="(e, x) in d.episodes">Episode {{ e.offsetId }} - {{ e.title }}</li>
          </ul>
        </li>
      </ul>
    </div>
  </sl-content>
</template>

<script lang="ts" src="./Episodes.ts"></script>

<style scoped src="./Episodes.scss" lang="scss"></style>
