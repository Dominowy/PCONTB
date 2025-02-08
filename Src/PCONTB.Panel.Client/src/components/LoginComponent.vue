<template>
  <div class="project-form">
    <div v-if="loading" class="loading">Loading...</div>
    <form @submit.prevent="handleSubmit">
      <div>
        <label for="name">UserName:</label>
        <input type="text" id="name" v-model="form.username" required />
        <label for="name">Password:</label>
        <input type="text" id="name" v-model="form.password" required />
      </div>
      <button type="submit">Login</button>
    </form>
  </div>
</template>

<script>
import { defineComponent } from "vue";
import ApiClient from "../services/ApiClient";

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
        username: null,
        password: null,
      },
    };
  },
  methods: {
    async handleSubmit() {
      this.loading = true;
      var response = await ApiClient.request("authentication/login", this.form);

      this.$emit("setUserId", response.userId);

      this.loading = false;
    },
  },
});
</script>

<style scoped>
.project-form {
  max-width: 400px;
  margin: auto;
  padding: 1rem;
  border: 1px solid #ccc;
  border-radius: 5px;
  box-shadow: 2px 2px 10px rgba(0, 0, 0, 0.1);
}
.loading {
  text-align: center;
  font-weight: bold;
}
label {
  display: block;
  margin-top: 10px;
}
input {
  width: 100%;
  padding: 8px;
  margin-top: 5px;
  border: 1px solid #ccc;
  border-radius: 4px;
}
button {
  margin-top: 15px;
  padding: 10px;
  width: 100%;
  background-color: #007bff;
  color: white;
  border: none;
  border-radius: 4px;
  cursor: pointer;
}
button:hover {
  background-color: #0056b3;
}
</style>
