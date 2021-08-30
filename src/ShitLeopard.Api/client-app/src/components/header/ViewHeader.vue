<template>
  <div id="sl-header" class="buffer">
    <div>
      <v-progress-linear
        indeterminate
        height="5"
        class="view-header"
        v-if="busy"
        style="z-index:101;background-color:#f7cf18"
      ></v-progress-linear>
      <v-toolbar color="#fecc00" class="pl-1 pr-1 view-header">
        <img src="/img/badge.png" width="45" class="mr-2" />

        <h3>
          <slot name="title"></slot>
        </h3>
        <slot name="content"></slot>
        <v-spacer></v-spacer>

        <sl-nav />
      </v-toolbar>
    </div>
  </div>
</template>

<script lang="ts" src="./ViewHeader.ts"></script>

<style scoped src="./ViewHeader.scss" lang="scss"></style>
