import Vue from 'vue';
import VueRouter from 'vue-router';

import Home from '../views/home/Home.vue';
import Metrics from '../views/metrics/Metrics.vue';
import SearchResults from '../views/search/SearchResults.vue';
import Episodes from '../views/episodes/Episodes.vue';
import Shows from '../views/shows/Shows.vue';
import Terms from '../views/terms/Terms.vue';
Vue.use(VueRouter);

const routes = [
  {
    path: '/',
    name: 'Home',
    title: 'Home',
    icon: 'mdi-home-circle',

    component: Home,
  },
  {
    path: '/query/:pattern',
    name: 'Search',
    title: 'Search',

    component: SearchResults,
  },
  {
    path: '/metrics',
    name: 'metrics',
    title: 'Metrics',
    icon: 'mdi-finance',

    component: Metrics,
  },
  {
    path: '/episodes/:showid',
    name: 'episodes',
    title: 'episodes',
    icon: 'mdi-finance',

    component: Episodes,
  },
  {
    path: '/shows',
    name: 'shows',
    title: 'shows',
    icon: 'mdi-finance',

    component: Shows,
  },
  {
    path: '/terms',
    name: 'terms',
    title: 'terms',
    icon: 'mdi-finance',

    component: Terms,
  },
];

const router = new VueRouter({
  routes,
});

export default router;
