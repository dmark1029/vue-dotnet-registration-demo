<template>
  <div class="register-form">
    <h2>Register</h2>
    <form @submit.prevent="registerUser">
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
      <button type="submit">Register</button>
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
        email: '',
        password: ''
      },
      errorMessage: '',
      successMessage: ''
    };
  },
  methods: {
    async registerUser() {
      try {
        const response = await axios.post('http://localhost:5033/api/auth/register', {
          username: this.form.username,
          email: this.form.email,
          password: this.form.password
        });
        this.successMessage = response.data.message;
        this.errorMessage = '';
      } catch (error) {
        this.errorMessage = 'Registration failed. Please try again.';
        this.successMessage = '';
      }
    }
  }
};
</script>