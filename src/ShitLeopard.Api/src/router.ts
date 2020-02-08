import Home from '@/views/Home';
import Vue from 'vue';
import Router from 'vue-router';

import Browse from './views/Browse';

Vue.use(Router);

const router = new Router({
  routes: [
    {
      path: '/',
      name: 'Home',
      meta: {
        title: 'Error Logs',
        icon: 'person_pin',
        display: true
      },
      component: Home
    },
    {
      path: '/browse',
      name: 'browse',
      meta: {
        title: 'Error Logs',
        icon: 'person_pin',
        display: true
      },
      component: Browse
    }
  ]
});

export default router;
