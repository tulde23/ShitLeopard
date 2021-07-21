import Vue from 'vue';
import VueRouter from 'vue-router';

import Details from '../views/Details.vue';
import Home from '../views/Home.vue';
import Metrics from '../views/Metrics.vue';
import TimeLine from '../views/TimeLine.vue';

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

    component: TimeLine
  },
  {
    path: '/metrics',
    name: 'metrics',
    title: 'Metrics',
    icon: 'mdi-finance',

    component: Metrics
  },
  {
    path: '/details/:id',
    name: 'details',
    title: 'details',
    icon: 'mdi-finance',

    component: Details
  }
];

const router = new VueRouter({
  routes
});

export default router;
