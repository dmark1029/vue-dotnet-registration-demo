<template>
  <div class="login-form">
    <h2>Login</h2>
    <form @submit.prevent="loginUser">
      <div>
        <label for="username">Username:</label>
        <input v-model="form.username" id="username" type="text" required />
      </div>
			<div>
        <label for="email">Email:</label>
        <input v-model="form.email" id="email" type="email" required />
      </div>
      <div>
        <label for="password">Password:</label>
        <input v-model="form.password" id="password" type="password" required />
      </div>
      <button type="submit">Login</button>
    </form>
    <p v-if="errorMessage" class="error">{{ errorMessage }}</p>
    <p v-if="successMessage" class="success">{{ successMessage }}</p>
  </div>
</template>

<script>
import axios from 'axios';

export default {
  data() {
    return {
      form: {
        username: '',
        password: '',
				email: ''
      },
      errorMessage: '',
      successMessage: ''
    };
  },
  methods: {
    async loginUser() {
      try {
        const response = await axios.post('http://localhost:5033/api/auth/login', {
          username: this.form.username,
          email: this.form.email,
          password: this.form.password
        });

        const user = {
          username: this.form.username,
          email: this.form.email
        };
        localStorage.setItem('user', JSON.stringify(user)); 

        this.$router.push('/dashboard');
        this.successMessage = response.data.message;
        this.errorMessage = '';
      } catch (error) {
        this.errorMessage = 'Login failed. Please check your credentials.';
        this.successMessage = '';
      }
    }
  }
};
</script>

<style scoped>
.error {
  color: red;
}
.success {
  color: green;
}
</style>