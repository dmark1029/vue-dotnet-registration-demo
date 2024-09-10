<template>
  <div id="app">
    <router-view></router-view>
  </div>
</template>

<script>
export default {
  data() {
    return {
      isLoggedIn: false
    };
  },
  mounted() {
    this.isLoggedIn = !!localStorage.getItem('user');
  },
  methods: {
    logout() {
      localStorage.removeItem('user');
      this.isLoggedIn = false;
      this.$router.push('/login');
    }
  },
  watch: {
    $route() {
      this.isLoggedIn = !!localStorage.getItem('user');
    }
  }
};
</script>

<style>
nav {
  margin-bottom: 20px;
}
nav a {
  margin-right: 15px;
}
button {
  background-color: red;
  color: white;
  border: none;
  padding: 10px;
  cursor: pointer;
}
button:hover {
  background-color: darkred;
}
</style>