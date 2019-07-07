<template>
  <li class="property" :class="{ completed: property.done, editing: editing }">
    <div v-if="property.type!='Part'" class="view">
      <label @dblclick="editing = true" v-text="property.name" />
      <el-select v-model="property.type" class="property-type" placeholder="Type">
        <el-option-group label="Basic">
          <el-option value="String" />
          <el-option value="Integer" />
          <el-option value="Decimal" />
          <el-option value="DateTime" />
          <el-option value="Boolean" />
          <el-option value="Guid" />
          <el-option value="Location" />
        </el-option-group>
        <el-option-group label="Contains">
          <el-option value="Part" />
          <el-option value="Entity" />
          <el-option value="List" />
        </el-option-group>
        <el-option-group label="Reference">
          <el-option value="Reference" />
          <el-option value="ReferenceList" />
        </el-option-group>
      </el-select>
      <button class="destroy" @click="deleteProperty( property )" />
    </div>
    <div v-if="property.type=='Part'" class="viewPart">
      <div class="part-header">
        <label @dblclick="editing = true" v-text="property.name" />
        <el-select v-model="property.part" class="property-type" placeholder="Part">
          <el-option value="Address" />
        </el-select>
        <button class="destroy" @click="deleteProperty( property )" />
      </div>
      <ul class="property-list">
        <disabled-property
          v-for="(prop, name) in property.properties"
          :key="name"
          :property="prop"
        />
      </ul>
    </div>
    <input
      v-show="editing"
      v-focus="editing"
      class="edit"
      :value="property.name"
      @keyup.enter="doneEdit"
      @keyup.esc="cancelEdit"
      @blur="doneEdit"
    >
  </li>
</template>

<script>
import DisabledProperty from './DisabledProperty.vue'

export default {
  name: 'Property',
  components: { DisabledProperty },
  directives: {
    focus(el, { value }, { context }) {
      if (value) {
        context.$nextTick(() => {
          el.focus()
        })
      }
    }
  },
  props: ['property'],
  data() {
    return {
      editing: false
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
