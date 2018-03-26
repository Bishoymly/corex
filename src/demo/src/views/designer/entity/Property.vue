<template>
  <li class="property" :class="{ completed: property.done, editing: editing }">
    <div class="view" v-if="property.type!='Part'">
      <label v-text="property.name" @dblclick="editing = true"></label>
      <el-select v-model="property.type" class="property-type" placeholder="Type">
        <el-option-group label="Basic">
          <el-option value="String"></el-option>
          <el-option value="Integer"></el-option>
          <el-option value="Decimal"></el-option>
          <el-option value="DateTime"></el-option>
          <el-option value="Boolean"></el-option>
          <el-option value="Guid"></el-option>
          <el-option value="Location"></el-option>
        </el-option-group>
        <el-option-group label="Contains">
          <el-option value="Part"></el-option>
          <el-option value="Entity"></el-option>
          <el-option value="List"></el-option>
        </el-option-group>
        <el-option-group label="Reference">
          <el-option value="Reference"></el-option>
          <el-option value="ReferenceList"></el-option>
        </el-option-group>
      </el-select>
      <button class="destroy" @click="deleteProperty( property )"></button>
    </div>
    <div class="viewPart" v-if="property.type=='Part'">
      <div class="part-header">
        <label v-text="property.name" @dblclick="editing = true"></label>
        <el-select v-model="property.part" class="property-type" placeholder="Part">
            <el-option value="Address"></el-option>
        </el-select>
        <button class="destroy" @click="deleteProperty( property )"></button>
      </div>
      <ul class="property-list">
        <disabled-property v-for="(prop, name) in property.properties" :key="name"
          :property="prop"></disabled-property>
      </ul>
    </div>
    <input class="edit"
      v-show="editing"
      v-focus="editing"
      :value="property.name"
      @keyup.enter="doneEdit"
      @keyup.esc="cancelEdit"
      @blur="doneEdit">
  </li>
</template>

<script>
import DisabledProperty from './DisabledProperty.vue'

export default {
  name: 'Property',
  components: { DisabledProperty },
  props: ['property'],
  data() {
    return {
      editing: false
    }
  },
  directives: {
    focus(el, { value }, { context }) {
      if (value) {
        context.$nextTick(() => {
          el.focus()
        })
      }
    }
  },
  methods: {
    deleteProperty(property) {
      this.$emit('deleteProperty', property)
    },
    editProperty({ property, value }) {
      this.$emit('editProperty', { property, value })
    },
    toggleProperty(property) {
      this.$emit('toggleProperty', property)
    },
    doneEdit(e) {
      const value = e.target.value.trim()
      const { property } = this
      if (!value) {
        this.deleteProperty({
          property
        })
      } else if (this.editing) {
        this.editProperty({
          property,
          value
        })
        this.editing = false
      }
    },
    cancelEdit(e) {
      e.target.value = this.property.name
      this.editing = false
    }
  }
}
</script>