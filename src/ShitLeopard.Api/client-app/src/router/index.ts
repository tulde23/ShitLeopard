import Vue from 'vue';
import VueRouter from 'vue-router';

import Browse from '../views/Browse.vue';
import Home from '../views/Home.vue';
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
    path: '/browse',
    name: 'browse',
    title: 'Browse',
    icon: 'mdi-find-replace',

    component: Browse
  }
];

const router = new VueRouter({
  routes
});

export default router;
