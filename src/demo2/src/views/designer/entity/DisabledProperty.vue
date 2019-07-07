<template>
  <li class="property disabled">
    <div class="view">
      <label v-text="property.name" />
      <label class="property-type" v-text="property.type" />
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
