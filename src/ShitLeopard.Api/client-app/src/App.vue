<template>
  <v-app style="background-color:#0d0c18ff">
    <v-main>
      <v-progress-linear
        :indeterminate="true"
        v-if="busy"
        color="red lighten-2"
      ></v-progress-linear>
      <v-container fluid fill-height style="padding:revert;margin:revert;overflow:auto">
        <transition name="fade" mode="out-in">
          <router-view />
        </transition>
      </v-container>
      <div class="buffer" style="background-color:#0d0c18ff"></div>
      <v-footer class="site-footer" style="background-color:#0d0c18ff" dense padless>
        <v-toolbar flat dense dark style="background-color:#0d0c18ff">
          {{ new Date().getFullYear() }} <strong>Shitleopard</strong>
          <v-spacer></v-spacer>

          <v-breadcrumbs small>
            <v-breadcrumbs-item>
              <a href="#/">Home</a>
            </v-breadcrumbs-item>
            <v-breadcrumbs-item>
              <a href="#/shows">Shows</a>
            </v-breadcrumbs-item>

            <v-breadcrumbs-item>
              <a href="#/terms">Terms</a>
            </v-breadcrumbs-item>
          </v-breadcrumbs>
        </v-toolbar>
      </v-footer>
    </v-main>
  </v-app>
</template>

<script lang="ts" src="./App.ts"></script>

<style scoped src="./App.scss" lang="scss"></style>
