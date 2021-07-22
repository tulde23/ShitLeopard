<template>
<v-container fill-height fluid>
    <v-row align="center" justify="center" class="mb-12">

        <v-col cols="12" align="center" justify="center">
            <v-img alt="Shitleopard Logo" class="shrink mr-2" align="center" contain src="/MainLogo.png" transition="scale-transition" width="400" />
        </v-col>
        <v-col class="col-xs-12 col-sm-6 mb-12">

            <v-autocomplete v-model="model" :items="items" :loading="isLoading" :search-input.sync="search" prepend-inner-icon="mdi-magnify" chips clearable hide-details hide-selected item-text="name" item-value="symbol" label="Start searching you greasy bastard!" solo>
                <template v-slot:no-data>
                    <v-list-item>
                        <v-list-item-title>

                        </v-list-item-title>
                    </v-list-item>
                </template>
                <template v-slot:selection="{ attr, on, item, selected }">
                    <v-chip v-bind="attr" :input-value="selected" color="blue-grey" class="white--text" v-on="on">
                        <v-icon left>
                            mdi-bitcoin
                        </v-icon>
                        <span v-text="item.name"></span>
                    </v-chip>
                </template>
                <template v-slot:item="{ item }">
                    <v-list-item-avatar color="indigo" class="text-h5 font-weight-light white--text">
                        {{ item.name.charAt(0) }}
                    </v-list-item-avatar>
                    <v-list-item-content>
                        <v-list-item-title v-text="item.name"></v-list-item-title>
                        <v-list-item-subtitle v-text="item.symbol"></v-list-item-subtitle>
                    </v-list-item-content>
                    <v-list-item-action>
                        <v-icon>mdi-bitcoin</v-icon>
                    </v-list-item-action>
                </template>
            </v-autocomplete>
        </v-col>

    </v-row>
</v-container>
</template>

<script lang="ts">
import Vue from "vue";
import axios from 'axios';

export default Vue.extend({
    name: "Landing",

    data: () => ({
        isLoading: false,
        items: [],
        model: null,
        search: null,
        tab: null,
    }),
    watch: {
        
        search(val) {
            // Items have already been loaded
            if (this.items.length > 0) return

            const data = {};
            this.isLoading = true
            axios({
                    url: 'http://localhost:9000/api/Search',
                    method: 'POST',

                    data
                })
                .then(resp => {
                    this.items = resp.data;
                    return resp;
                })
                .catch(err => {

                    return err;
                }).finally(() => (this.isLoading = false));

        },
    },

});
</script>
