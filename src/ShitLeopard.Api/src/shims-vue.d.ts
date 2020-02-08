import { Subscription } from 'rxjs';
import Vue from 'vue';
import Vuetify from 'vuetify';

import { FileUploadService, Helper, HttpService, MessageService, DataService } from '@/services';

declare module '*.vue' {
  import Vue from 'vue';
  export default Vue;
}
// 2. Specify a file with the types you want to augment
//    Vue has the constructor type in types/vue.d.ts
declare module 'vue/types/vue' {
  // 3. Declare augmentation for Vue
  interface Vue {
    $subscription: Subscription;
    $helper: Helper;
    $messageBus: MessageService;
    $http: HttpService;
    $api: DataService;
    $upload: FileUploadService;
  }
}
