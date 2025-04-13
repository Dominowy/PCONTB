import { defineStore } from "pinia";
import ApiClient from "@/services/ApiClient";

export const useStore = defineStore("store", {
  state: () => ({
    user: null,
    loading: false,
    error: null,
  }),
  actions: {
    async fetchSession() {
      this.loading = true;
      try {
        const response = await ApiClient.request("account/auth/get-session", {});
        this.user = response.user;
        this.error = null;
      } catch (err) {
        this.user = null;
        this.error = err;
      } finally {
        this.loading = false;
      }
    },

    async logout() {
      this.user = null;
      await ApiClient.request("account/auth/logout", {});
    },
  },
  getters: {
    isAuthenticated: (state) => !!state.user,
  },
});
