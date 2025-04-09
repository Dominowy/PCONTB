<template>
  <b-container>
    <b-spinner v-if="loading" label="Loading..." class="d-block mx-auto mb-3" />

    <b-form @submit.prevent="handleSubmit">
      <b-form-group label="UserName:" label-for="username">
        <b-form-input id="username" v-model="form.login" required placeholder="Enter username" />
      </b-form-group>

      <b-form-group label="Password:" label-for="password">
        <b-form-input
          id="password"
          v-model="form.password"
          required
          type="password"
          placeholder="Enter password"
        />
      </b-form-group>

      <b-button type="submit" variant="secondary" block>Login</b-button>
    </b-form>
  </b-container>
</template>

<script>
import { defineComponent } from "vue";
import ApiClient from "@/services/ApiClient";

export default defineComponent({
  props: {
    projectId: {
      type: String,
      default: null,
    },
  },
  data() {
    return {
      loading: false,
      form: {
        login: null,
        password: null,
      },
    };
  },
  methods: {
    async handleSubmit() {
      this.loading = true;

      await ApiClient.request("account/auth/login", this.form);

      this.loading = false;
    },
  },
});
</script>
