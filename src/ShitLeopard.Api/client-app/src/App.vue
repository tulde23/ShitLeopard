<template>
<v-app id="shitleopard">
    <v-progress-linear color="#D4AF37" :indeterminate="busy" height="4" style="paddin:0; margin:0"></v-progress-linear>

    <v-content>
        <v-container fluid>
            
            <v-row align="center" justify="center">
                <v-col cols="12">
                    <v-sheet>
                        <transition name="fade-transition">
                            <router-view></router-view>
                        </transition>
                    </v-sheet>
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
