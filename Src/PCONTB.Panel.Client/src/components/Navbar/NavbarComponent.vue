<template>
  <BNavbar v-b-color-mode="'dark'" toggleable variant="dark">
    <BNavbarBrand href="#">NavBar</BNavbarBrand>

    <BNavbarToggle v-b-toggle.navbar-toggle-collapse @click="toggleExpanded">
      <template #default>
        <IBiChevronBarUp v-if="isExpanded" />
        <IBiChevronBarDown v-else />
      </template>
    </BNavbarToggle>

    <BCollapse id="navbar-toggle-collapse" is-nav>
      <BNavbarNav class="ms-auto">
        <BNavItem href="#">Link 1</BNavItem>
        <BNavItem href="#">Link 2</BNavItem>
        <BNavItem href="#" disabled>Disabled</BNavItem>
      </BNavbarNav>
    </BCollapse>
  </BNavbar>
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
