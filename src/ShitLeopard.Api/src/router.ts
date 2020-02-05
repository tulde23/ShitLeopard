import Home from '@/views/Home';
import Vue from 'vue';
import Router from 'vue-router';

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
    }
  ]
});

export default router;
