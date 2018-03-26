<template>
<div class="app-container">
  <h1>Entity Designer</h1>
  
  <h2>{{ entity.name }}</h2>
  <h3>Properties</h3>
  <ul>
    <li v-for="property in entity.properties" v-bind:key="property.Id">
      {{ property.name }}
    </li>
  </ul>
</div>
</template>

<script>
export default {
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
    };
  }
};
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
h1,
h2 {
  font-weight: normal;
}
ul {
  list-style-type: none;
  padding: 0;
}
li {
  display: inline-block;
  margin: 0 10px;
}
a {
  color: #42b983;
}
</style>
