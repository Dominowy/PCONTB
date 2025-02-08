<template>
  <div class="weather-component">
    <div v-if="loading" class="loading">Loading...</div>
    <div v-if="data" class="content">
      <login-component v-if="!userId" @setUserId="setUserId" />
      <button v-if="userId" @click="showEditAdd = !showEditAdd">Add</button>
      <table v-if="userId">
        <thead>
          <tr>
            <th>Name</th>
            <th>User</th>
            <th>Edit</th>
            <th>Delete</th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="project in data.projects" :key="project.id">
            <td>{{ project.name }}</td>
            <td>{{ project.user.displayName }}</td>
            <td><button @click="setProjecId(project.id)">Edit</button></td>
            <td><button @click="deleteProject(project.id)">Delete</button></td>
          </tr>
        </tbody>
      </table>
    </div>
    <add-edit-component
      v-if="showEditAdd && userId"
      :userId="userId"
      :projectId="selectedProjectId"
      @fetchData="fetchData"
    />
  </div>
</template>

<script>
import { defineComponent } from "vue";
import AddEditComponent from "./AddEditComponent.vue";
import ApiClient from "../services/ApiClient";
import LoginComponent from "./LoginComponent.vue";

export default defineComponent({
  components: {
    AddEditComponent,
    LoginComponent,
  },
  data() {
    return {
      loading: false,
      data: null,
      selectedProjectId: null,
      showEditAdd: false,
      userId: null,
    };
  },
  async created() {
    await this.fetchData();
  },
  watch: {
    $route: "fetchData",
  },
  methods: {
    async fetchData() {
      this.showEditAdd = false;
      this.data = null;
      this.loading = true;

      this.data = await ApiClient.request("project/get-all", {});

      this.loading = false;
    },
    setProjecId(id) {
      this.selectedProjectId = id;
      this.showEditAdd = !this.showEditAdd;
    },
    setUserId(id) {
      this.userId = id;
    },
    async deleteProject(id) {
      await ApiClient.request("project/delete", { id: id });
      await this.fetchData();
    },
  },
});
</script>

<style scoped>
th {
  font-weight: bold;
}

th,
td {
  padding-left: 0.5rem;
  padding-right: 0.5rem;
}

.weather-component {
  text-align: center;
}

table {
  margin-left: auto;
  margin-right: auto;
}
</style>
