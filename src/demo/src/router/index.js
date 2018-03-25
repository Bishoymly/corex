import Vue from 'vue'
import Router from 'vue-router'
import EntityDesigner from '@/components/EntityDesigner'

Vue.use(Router)

export default new Router({
  routes: [
    {
      path: '/',
      name: 'EntityDesigner',
      component: EntityDesigner
    }
  ]
})
