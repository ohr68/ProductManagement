import './assets/main.css'

import { createApp } from 'vue'
import App from './App.vue'
import router from './router'
import PrimeVue from 'primevue/config';
import ToastService from 'primevue/toastservice';
import Aura from '@primeuix/themes/aura';
import FocusTrap from 'primevue/focustrap';

const app = createApp(App)

app.use(router);
app.use(PrimeVue, {
    theme: {
        preset: Aura
    }
});
app.directive('focustrap', FocusTrap);
app.use(ToastService);

app.mount('#app')