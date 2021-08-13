<template>
  <v-container fill-height>
    <v-row align="center" justify="center" class="mb-15">
      <v-col sm="12" lg="5" class="mb-5">
        <img
          alt="Shitleopard Logo"
          class="align-self-center mb-4"
          src="/img/logo2.png"
          style="width:50%; display:block; margin-left:auto; margin-right:auto"
        />
        <v-text-field
          outlined
          dense
          class="white"
          label="search, you greasy bastard"
          align="center"
          append-icon="mdi-magnify"
          v-model="question.text"
          hide-details
          single-line
          clearable
          @keyput.enter.native="search"
          @click:append="search"
        ></v-text-field>
      </v-col>
    </v-row>
  </v-container>
</template>

<script lang="ts" src="./Home.ts"></script>

<style scoped src="./Home.scss" lang="scss"></style>
