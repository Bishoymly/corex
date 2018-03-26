<template>
<div class="app-container">
  <section class="entityapp">
    <!-- header -->
    <header class="header">
      <h2>{{entity.name}}</h2>
    </header>
    <!-- main section -->
    <section class="main" v-show="entity.properties.length">
      <ul class="property-list">
        <property @toggleProperty='toggleProperty' @editProperty='editProperty' @deleteProperty='deleteProperty' v-for="(property, name) in entity.properties" :key="name"
          :property="property"></property>
        <input class="new-property" autocomplete="off" placeholder="add" @keyup.enter="addProperty">
      </ul>
    </section>
    <!-- footer -->
    <footer class="footer" v-show="entity.properties.length">
      <ul class="filters">
        <li>
          <a class="selected">parts</a>
          <a class="">properties</a>
          <a class="">UI</a>
        </li>
      </ul>
    </footer>
  </section>
</div>
</template>

<script>
import Property from './Property.vue'

export default {
  components: { Property },
  data() {
    return {
      entity: {
        name: "Order",
        properties: [
          {
            name: "Id",
            type: "Guid"
          },
          {
            name: "Date",
            type: "DateTime"
          },
          {
            name: "Username",
            type: "String"
          }
        ]
      }
    }
  },
  methods: {
    addProperty(e) {
      const text = e.target.value
      if (text.trim()) {
        this.entity.properties.push({
          name: text
        })
      }
      e.target.value = ''
    },
    toggleProperty(val) {
      val.done = !val.done
    },
    deleteProperty(property) {
      this.entity.properties.splice(this.entity.properties.indexOf(property), 1)
    },
    editProperty({ property, value }) {
      property.text = value
    },
    clearCompleted() {
      this.entity.properties = this.properties.filter(property => !property.done)
    },
    toggleAll({ done }) {
      this.entity.properties.forEach(property => {
        property.done = done
      })
    }
  },
  filters: {
    pluralize: (n, w) => n === 1 ? w : w + 's',
    capitalize: s => s.charAt(0).toUpperCase() + s.slice(1)
  }
}
</script>

<style lang="scss">
  @import './index.scss';
</style>