<template>
  <v-container class="register-container" fluid>
    <v-row justify="center">
      <v-col cols="12" md="6">
        <v-card>
          <v-card-title>
            <h2>Register</h2>
          </v-card-title>
          <v-card-text>
            <v-form @submit.prevent="registerUser">
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
                prepend-icon="mdi-email"
              ></v-text-field>

              <v-text-field
                v-model="form.password"
                label="Password"
                type="password"
                required
                prepend-icon="mdi-lock"
              ></v-text-field>

              <v-text-field
                v-model="form.confirmPassword"
                label="Confirm Password"
                type="password"
                required
                prepend-icon="mdi-lock"
              ></v-text-field>

              <v-btn type="submit" color="primary" block>Register</v-btn>
            </v-form>
          </v-card-text>
          <v-card-actions>
            <v-btn text to="/login">Already have an account? Login</v-btn>
          </v-card-actions>
        </v-card>
      </v-col>
    </v-row>

    <v-dialog v-model="dialog" max-width="400">
      <v-card>
        <v-card-title class="headline">{{ dialogTitle }}</v-card-title>
        <v-card-text>{{ dialogMessage }}</v-card-text>
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
        password: '',
        confirmPassword: ''
      },
      errorMessage: '',
      successMessage: '',
      dialog: false,
      dialogMessage: '',
      dialogTitle: '',
      emailRules: [
        v => !!v || 'Email is required',
        v => /.+@.+\..+/.test(v) || 'Email must be valid'
      ]
    };
  },
  methods: {
    async registerUser() {
      if (this.form.password !== this.form.confirmPassword) {
        this.showDialog('Error', 'Passwords do not match!');
        return;
      }

      try {
        const response = await axios.post('http://localhost:5033/api/auth/register', {
          username: this.form.username,
          email: this.form.email,
          password: this.form.password
        });
        this.showDialog('Success', response.data.message);
      } catch (error) {
        this.showDialog('Error', 'Registration failed. Please try again.');
      }
    },
    showDialog(title, message) {
      this.dialogTitle = title;
      this.dialogMessage = message;
      this.dialog = true;
    },
    closeDialog() {
      this.dialog = false;
    }
  }
};
</script>

<style scoped>
.register-container {
  margin-top: 50px;
}
</style>