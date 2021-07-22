<template>
  <v-app id="shitleopard">
    <v-main style="background-color:#0d0c18;">
      <v-container fluid>
        <v-row align="center" justify="center">
          <v-col md="6" xs="1" sm="1">
            <v-sheet elevation="10" rounded>
              <v-container>
                <transition name="fade-transition">
                  <router-view></router-view>
                </transition>
              </v-container>
            </v-sheet>
          </v-col>
        </v-row>
      </v-container>
    </v-main>
  </v-app>
</template>

<script lang="ts" src="./App.ts"></script>

<style scoped src="./App.scss" lang="scss"></style>
