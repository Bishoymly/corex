<template>
  <li class="property disabled">
    <div class="view">
      <label v-text="property.name"></label>
      <label v-text="property.type" class="property-type"></label>
    </div>
  </li>
</template>

<script>
export default {
  name: 'DisabledProperty',
  props: ['property'],
  data() {
    return {
      
    }
  }
}
</script>