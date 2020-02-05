

<template>
  <v-app class="login-bg">
     
   <v-navigation-drawer id="left-nav" disable-resize-watcher="true" 
     permanent
      absolute
v-if="isLoggedIn"
  app  class="complement">
      <img src="/gozer.png" style="display:block;margin:0 auto">
      
      <v-list >
        <v-list-tile
          v-for="item in authorizedRoutes"
          :key="item.meta.title"
          :to="item.path"
        >
          <v-list-tile-action v-if="item.meta.display" style="color:white">
            <v-icon style="color:white">{{ item.meta.icon }}</v-icon>
          </v-list-tile-action>
          <v-list-tile-content v-if="item.meta.display" style="color:white">
            <v-list-tile-title style="color:white">{{ item.meta.title }}</v-list-tile-title>
          </v-list-tile-content>
        </v-list-tile>
      </v-list>
      
   
      <v-chip v-if="buildVersion"
      class="alert success"
      label style="color:white"
    >
      Sleuth API Version: {{buildVersion.assemblyVersion}}
    </v-chip>


     <v-btn @click="logout" style="bottom:15px"
                color="warning"
                dark
                small
                absolute
                bottom
                right
                fab
              >
                <v-icon>exit_to_app</v-icon>
              </v-btn>
    </v-navigation-drawer>
    
    

    <v-content id="main-app" style="padding-top:0px">
      <v-container>
        <transition name="fade-transition">
          <router-view></router-view>
        </transition>
      </v-container>
    </v-content>
   <v-snackbar
      v-model="isErrorAvailable"
      color="error"
  
      timeout="3000"
      top>
      
      <template v-if="errorMessage">
              {{errorMessage.body}}
      </template>

     
    </v-snackbar>
  </v-app>
</template>

<script lang="ts" src="./App.ts">

</script>
<style scoped src="./App.scss" lang="scss">

</style>
