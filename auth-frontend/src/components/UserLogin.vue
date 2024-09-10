<template>
  <v-container class="login-container" fluid>
    <v-row justify="center">
      <v-col cols="12" md="6">
        <v-card>
          <v-card-title>
            <h2>Login</h2>
          </v-card-title>
          <v-card-text>
            <v-form @submit.prevent="loginUser">
              <v-text-field
                v-model="form.username"
                label="Username"
                required
                prepend-icon="mdi-account"
              ></v-text-field>

              <v-text-field
                v-model="form.email"
                label="Email"
                type="email"
                :rules="emailRules"
                required
                prepend-icon="mdi-account"
              ></v-text-field>

              <v-text-field
                v-model="form.password"
                label="Password"
                type="password"
                required
                prepend-icon="mdi-lock"
              ></v-text-field>

              <v-btn type="submit" color="primary" block>Login</v-btn>
            </v-form>
          </v-card-text>
          <v-card-actions>
            <v-btn text to="/register">Don't have an account? Register</v-btn>
          </v-card-actions>
        </v-card>
      </v-col>
    </v-row>

    <!-- Modal for showing error messages -->
    <v-dialog v-model="dialog" max-width="400">
      <v-card>
        <v-card-title class="headline">Error</v-card-title>
        <v-card-text>{{ errorMessage }}</v-card-text>
        <v-card-actions>
          <v-spacer></v-spacer>
          <v-btn color="primary" text @click="closeDialog">OK</v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>
  </v-container>
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
      successMessage: '',
      dialog: false,
      emailRules: [
        v => !!v || 'Email is required',
        v => /.+@.+\..+/.test(v) || 'Email must be valid'
      ]
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
        this.openDialog();  // Show modal on login failure
      }
    },
    openDialog() {
      this.dialog = true;  // Show the modal
    },
    closeDialog() {
      this.dialog = false;  // Close the modal
    }
  }
};
</script>

<style scoped>
.login-container {
  margin-top: 50px;
}
.error {
  color: red;
  text-align: center;
}
.success {
  color: green;
  text-align: center;
}
</style>
