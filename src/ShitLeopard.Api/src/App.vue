<template>
<v-app>
    <v-progress-linear color="#D4AF37" :indeterminate="busy" height="4" style="paddin:0; margin:0"></v-progress-linear>
    <v-toolbar flat>
        <v-toolbar-title>

            <img src="/img/logo300.png" class="responsive">

        </v-toolbar-title>
        <v-spacer></v-spacer>
        <v-toolbar-items class="hidden-sm-and-down">
            <v-btn flat to="/">Ask Me</v-btn>
            <v-btn flat to="/browse">Browse</v-btn>

        </v-toolbar-items>
    </v-toolbar>

    <div id="random-quote" v-if="answer">
        <v-container>
            <v-layout>
                <v-flex xs1 sm1 md1>
                    <v-badge color="red" left>
                        <template v-slot:badge>
                            <span>{{answer.popularity}}</span>
                        </template>

                    </v-badge>
                </v-flex>
                <v-flex xs12 sm6 md6 buffer>
                    <p class="text-xs-left"> {{answer.body}}</p>

                </v-flex>
                <v-flex xs12 sm5 md5 buffer class="text-xs-left">
                    <v-btn @click="refresh" fab small>
                        <v-icon>refresh</v-icon>
                    </v-btn>
                    <v-btn color="blue" @click="upvote" fab small>
                        <v-icon>thumb_up</v-icon>

                    </v-btn>

                </v-flex>
            </v-layout>
        </v-container>

    </div>

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
