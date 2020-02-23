<template>
<v-app id="shitleopard">
    <v-progress-linear color="#D4AF37" :indeterminate="busy" height="4" style="paddin:0; margin:0"></v-progress-linear>
  <!--  <v-app-bar app clipped-left color="amber">
        <v-app-bar-nav-icon @click="drawer = !drawer" />
        <span class="title ml-3 mr-5">Google&nbsp;<span class="font-weight-light">Keep</span></span>
        <v-text-field solo-inverted flat hide-details label="Search" prepend-inner-icon="search" />

        <v-spacer />
    </v-app-bar>-->
    <v-navigation-drawer :expand-on-hover="expandOnHover" :mini-variant="isMini" absolute>
        <v-list dense nav class="py-0">
            <v-list-item two-line :class="isMini && 'px-0'">
                <v-list-item-avatar>
                    <img src="logo.png">
                </v-list-item-avatar>

                <v-list-item-content>
                    <v-list-item-title>Shit Leopard</v-list-item-title>
                    <v-list-item-subtitle>Trailer Park Boys</v-list-item-subtitle>
                </v-list-item-content>
            </v-list-item>

            <v-divider></v-divider>

            <v-list-item v-for="item in authorizedRoutes" :key="item.path" link :to="item.path">
                <v-list-item-icon>
                    <v-icon>{{ item.icon}}</v-icon>
                </v-list-item-icon>

                <v-list-item-content>
                    <v-list-item-title>{{ item.title }}</v-list-item-title>
                </v-list-item-content>
            </v-list-item>

        </v-list>
    </v-navigation-drawer>

    <v-content>
        <v-container>
            <v-row align="center" justify="center" style="margin-left:250px">
                <v-col class="text-center">
                    <div class="text-center">
                        <v-sheet>
                        <xquote></xquote>
                            <transition name="fade-transition">
                                <router-view></router-view>
                            </transition>
                        </v-sheet>
                    </div>

                </v-col>
            </v-row>
        </v-container>
    </v-content>

</v-app>
</template>

<script lang="ts" src="./App.ts">

</script>

<style scoped src="./App.scss" lang="scss">

</style>
