// @ts-ignore
import App from './App.vue'
import { createApp } from 'vue'
import { registerGlobalComponents } from './registerGlobalComponents'
import router from './router'
import popper from 'popper.js'
const root = createApp(App)
registerGlobalComponents(root)
root
  .use(router, popper)
  .mount('#app')
