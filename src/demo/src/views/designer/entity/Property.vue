<template>
  <li class="property" :class="{ completed: property.done, editing: editing }">
    <div class="view">
      <label v-text="property.name" @dblclick="editing = true"></label>
      <el-dropdown>
        <span class="el-dropdown-link">
          {{property.type}}<i class="el-icon-arrow-down el-icon--right"></i>
        </span>
        <el-dropdown-menu slot="dropdown">
          <el-dropdown-item>Number</el-dropdown-item>
          <el-dropdown-item>String</el-dropdown-item>
          <el-dropdown-item>DateTime</el-dropdown-item>
        </el-dropdown-menu>
      </el-dropdown>
      <button class="destroy" @click="deleteProperty( property )"></button>
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
export default {
  name: 'Property',
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