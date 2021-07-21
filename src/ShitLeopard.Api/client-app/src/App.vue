<template>
  <v-app id="shitleopard">
    <v-dialog v-model="busy" persistent hide-overlay width="300">
      <v-progress-linear
        elevation="20"
        color="#D4AF37"
        indeterminate
        height="20"
      ></v-progress-linear>
    </v-dialog>
    <v-main style="background-color:maroon;">
      <v-container fluid>
        
        </v-row>
        <v-row align="center" justify="center">
          <v-col md="6" xs="1" sm="1">
      
      
         
            <v-sheet elevation="10" rounded>
              <v-container>
                 
                <v-img src="/MainLogo.png" max-width="250" class="text-center"></v-img>

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
