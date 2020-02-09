<template>
<v-app>
  <v-progress-linear color="yellow" :indeterminate="busy" height="4" style="paddin:0; margin:0"></v-progress-linear>
    <v-toolbar flat dark>
        <v-toolbar-title>
        <img src="/MainLogo.png">
        </v-toolbar-title>
        <v-spacer></v-spacer>
   <v-toolbar-items class="hidden-sm-and-down">
      <v-btn flat  to="/">Ask Me</v-btn>
      <v-btn flat  to="/browse">Browse</v-btn>

    </v-toolbar-items>
    </v-toolbar>

    <v-content id="main-app" style="padding-top:50px">
        <v-container>
            <transition name="fade-transition">
                <router-view></router-view>
            </transition>
        </v-container>
    </v-content>
</v-app>
</template>

<script lang="ts" src="./App.ts">

</script>

<style scoped src="./App.scss" lang="scss">

</style>
