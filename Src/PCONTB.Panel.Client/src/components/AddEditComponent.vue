<template>
  <div class="project-form">
    <div v-if="loading" class="loading">Loading...</div>
    <form @submit.prevent="handleSubmit">
      <div>
        <label for="name">Project Name:</label>
        <input type="text" id="name" v-model="form.name" />
      </div>
      <button type="submit">{{ projectId ? "Update Project" : "Create Project" }}</button>
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
    userId: {
      type: String,
      default: null,
    },
  },
  data() {
    return {
      loading: false,
      form: {},
    };
  },
  async created() {
    console.log(this.projectId);
    this.loading = true;
    if (this.projectId) {
      let response = await ApiClient.request("project/update/form", { id: this.projectId });
      this.form = response.formData;
    } else {
      let response = await ApiClient.request("project/add/form", {});
      this.form = response.formData;
    }
    this.loading = false;
  },
  methods: {
    async handleSubmit() {
      this.loading = true;
      this.form.userId = this.userId;
      if (this.projectId) {
        await ApiClient.request("project/update", this.form);
      } else {
        await ApiClient.request("project/add", this.form);
      }
      this.loading = false;
      this.$emit("fetchData");
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
input,
textarea {
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
