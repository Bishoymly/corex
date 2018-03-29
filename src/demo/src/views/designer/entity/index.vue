<template>
<div class="app-container">
  <section class="entityapp">
    <!-- header -->
    <header class="header">
      <ul class="filters right">
        <li>
          <a class="selected">Properties</a>
          <a class="">Actions</a>
          <a class="">Validations</a>
          <a class="">UI</a>
        </li>
      </ul>
      <h2>{{entity.name}}</h2>
    </header>
    <!-- main section -->
    <section class="main" v-show="entity.properties.length">
      <ul class="property-list">
        <disabled-property :property="entity.id"></disabled-property>
        <draggable v-model="entity.properties" :options="{group:'properties'}" @start="drag=true" @end="drag=false">
          <property @toggleProperty='toggleProperty' @editProperty='editProperty' @deleteProperty='deleteProperty' v-for="(property, name) in entity.properties" :key="name"
            :property="property"></property>
        </draggable>
        <input class="new-property" autocomplete="off" placeholder="Add" @keyup.enter="addProperty">
      </ul>
    </section>
    <!-- footer -->
    <footer class="footer" v-show="entity.properties.length">
      Ready
    </footer>
  </section>
</div>
</template>

<script>
import draggable from 'vuedraggable'
import Property from './Property.vue'
import DisabledProperty from './DisabledProperty.vue'

export default {
  components: { DisabledProperty, Property, draggable },
  data() {
    return {
      entity: {
        name: "Order",
        id: {
            name: "Id",
            type: "Guid"
        },
        properties: [
          {
            name: "Date",
            type: "DateTime"
          },
          {
            name: "Username",
            type: "String"
          },
          {
            name: "BillingAddress",
            type: "Part",
            part: "Address",
            properties: [
              {
                name: "Street",
                type: "String"
              },
              {
                name: "City",
                type: "String"
              },
              {
                name: "ZipCode",
                type: "String"
              }
            ]
          }
        ],
        actions: [
          "Get",
          "Create",
          "Update",
          "Delete"
        ]
      }
    }
  },
  computed: {
    // a computed getter
    allProperties: function () {
      return this.entity.properties
    }
  },
  methods: {
    addProperty(e) {
      const text = e.target.value
      if (text.trim()) {
        this.entity.properties.push({
          name: text,
          type: 'String'
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
      property.name = value
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