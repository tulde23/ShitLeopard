import Vue from 'vue';
import VueRouter from 'vue-router';

import Home from '../views/Home.vue';

Vue.use(VueRouter);

const routes = [
  {
    path: '/',
    name: 'Home',
    title: 'Home',
    icon: 'mdi-home-circle',

    component: Home
  },
  {
    path: '/timeline',
    name: 'timeline',
    title: 'Timeline',
    icon: 'mdi-chart-timeline-variant',

    component: Home
  }
];

const router = new VueRouter({
  routes
});

export default router;
