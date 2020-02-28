import Vue from 'vue';
import Vuetify from 'vuetify';

import 'vuetify/dist/vuetify.min.css';

Vue.use(Vuetify);

export default new Vuetify({
  theme: {
    themes: {
      light: {
        primary: '#ECC91A',
        secondary: '#870111',
        accent: '#929982',
        error: '#870111',
        info: '#33658A',
        success: '#A8C256 ',
        warning: '#840032'
      }
    }
  }
});
