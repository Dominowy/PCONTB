<template>
  <BNavbar v-b-color-mode="'dark'" toggleable="lg" variant="primary">
    <BNavbarBrand href="#">PCONTB</BNavbarBrand>
    <BNavbarToggle v-b-toggle.navbar-toggle-collapse />
    <BCollapse id="navbar-toggle-collapse" is-nav>
      <BNavbarNav>
        <BNavItem href="#">Link</BNavItem>
        <BNavItem href="#" disabled>Disabled</BNavItem>
      </BNavbarNav>
      <BNavbarNav class="ms-auto mb-2 mb-lg-0">
        <BNavItemDropdown text="Lang" right>
          <BDropdownItem href="#">EN</BDropdownItem>
          <BDropdownItem href="#">ES</BDropdownItem>
          <BDropdownItem href="#">RU</BDropdownItem>
          <BDropdownItem href="#">FA</BDropdownItem>
        </BNavItemDropdown>
        <BNavItemDropdown right>
          <template #button-content>
            <em>User</em>
          </template>
          <BDropdownItem href="#">Profile</BDropdownItem>
          <BDropdownItem href="#">Sign Out</BDropdownItem>
        </BNavItemDropdown>
      </BNavbarNav>
      <BNavForm class="d-flex">
        <BFormInput class="me-2" placeholder="Search" />
        <BButton type="submit" variant="outline-success">Search</BButton>
      </BNavForm>
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
