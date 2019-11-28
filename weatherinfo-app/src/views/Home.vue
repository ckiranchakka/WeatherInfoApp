<template>
  <div class="home">
   
    <weather/>
  </div>
</template>

<script>
// @ is an alias to /src

import weather from "@/components/weather.vue";

export default {
  name: 'home',
  components: {
    weather
  }
}
</script>
