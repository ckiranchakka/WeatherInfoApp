import Vue from 'vue'
import App from './App.vue'
import './registerServiceWorker'
import router from './router'
import store from './store'
import axios from 'axios'
import bootstrap from 'bootstrap-vue'
import Vuetify from 'vuetify'
import Bootstrap from 'bootstrap'
import 'bootstrap/dist/css/bootstrap.css'
import 'bootstrap-vue/dist/bootstrap-vue.css'
import 'material-design-icons-iconfont/dist/material-design-icons.css'
import 'jqwidgets-scripts/jqwidgets-vue/vue_jqxgrid.vue'
import 'chart.min.js'
import 'chart.js'

// library.add(faUserSecret)

// Vue.component('font-awesome-icon', FontAwesomeIcon)
Vue.use(bootstrap)

Vue.use(Vuetify, {
  iconfont: 'md'
})
Vue.prototype.Bootstrap = Bootstrap

Vue.prototype.axios = axios

Vue.config.productionTip = false
window.Vue = Vue

new Vue({
  router,
  store,
  render: h => h(App)
}).$mount('#app')
