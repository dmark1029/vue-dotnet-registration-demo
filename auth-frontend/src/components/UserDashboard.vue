<template>
	<div class="dashboard">
		<h2>Welcome, {{ user.username }}!</h2>
		<p>Email: {{ user.email }}</p>
		<button @click="logout">Logout</button>
	</div>
</template>

<script>
export default {
	data() {
		return {
			user: JSON.parse(localStorage.getItem('user')) || {}
		};
	},
	methods: {
		logout() {
			localStorage.removeItem('user');
			this.$router.push('/login');
		}
	}
};
</script>

<style scoped>
.dashboard {
	margin: 50px;
}

button {
	padding: 10px 20px;
	background-color: #f44336;
	color: white;
	border: none;
	cursor: pointer;
}

button:hover {
	background-color: #e53935;
}
</style>