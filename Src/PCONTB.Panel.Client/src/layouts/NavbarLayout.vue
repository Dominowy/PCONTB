<template>
  <b-navbar v-b-color-mode="'dark'" toggleable="lg" variant="dark">
    <b-navbar-brand to="/">PCONTB</b-navbar-brand>
    <b-navbar-toggle v-b-toggle.navbar-toggle-collapse />
    <b-collapse id="navbar-toggle-collapse" is-nav>
      <b-navbar-nav>
        <b-nav-item to="/">Home</b-nav-item>
        <b-nav-item to="/dashboard">Dashboard</b-nav-item>
      </b-navbar-nav>
      <b-navbar-nav class="ms-auto mb-2 mb-lg-0">
        <b-nav-item to="/auth/login">Login</b-nav-item>
        <b-nav-item-dropdown right>
          <template #button-content>
            <em>User</em>
          </template>
          <b-dropdown-item href="#">Profile</b-dropdown-item>
          <b-dropdown-item href="#">Sign Out</b-dropdown-item>
        </b-nav-item-dropdown>
      </b-navbar-nav>
      <b-nav-form class="d-flex">
        <b-form-input class="me-2" placeholder="Search" />
        <b-button type="submit" variant="outline-success">Search</b-button>
      </b-nav-form>
    </b-collapse>
  </b-navbar>
</template>

<script>
export default {
  data() {
    return {
      isExpanded: false,
    };
  },
  methods: {
    toggleExpanded() {
      this.isExpanded = !this.isExpanded;
    },
  },
};
</script>
