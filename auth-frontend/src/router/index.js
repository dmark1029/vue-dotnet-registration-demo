import { createRouter, createWebHistory } from 'vue-router';
import Register from '../components/UserRegister.vue';
import Login from '../components/UserLogin.vue';
import Dashboard from '../components/UserDashboard.vue'; // Import Dashboard component

const routes = [
  {
    path: '/register',
    name: 'Register',
    component: Register
  },
  {
    path: '/login',
    name: 'Login',
    component: Login
  },
  {
    path: '/dashboard',
    name: 'Dashboard',
    component: Dashboard,
    meta: { requiresAuth: true }  // Protect this route
  },
  {
    path: '/',
    redirect: '/login'
  }
];

const router = createRouter({
  history: createWebHistory(),
  routes
});

// Add a navigation guard to protect routes that require authentication
router.beforeEach((to, from, next) => {
  const isAuthenticated = !!localStorage.getItem('user'); // Check if user is logged in
  if (to.matched.some(record => record.meta.requiresAuth) && !isAuthenticated) {
    next('/login'); // Redirect to login if not authenticated
  } else {
    next(); // Continue to the route
  }
});

export default router;