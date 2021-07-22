import Vue from 'vue';
import Vuetify from 'vuetify';

import 'vuetify/dist/vuetify.min.css';

Vue.use(Vuetify);
const opts = {
  theme: {
    themes: {
      light: {
        primary: '#870111',
        secondary: '#ECC91A',
        accent: '#929982',
        error: '#870111',
        info: '#33658A',
        success: '#A8C256 ',
        warning: '#585123',
      },
    },
  },
};

export default new Vuetify(opts);
