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
      try {
        const response = await ApiClient.request("account/auth/get-session", {});
        this.user = response.user;
        this.error = null;
      } catch (err) {
        this.user = null;
        this.error = err;
      }
    },

    async logout() {
      this.loading = true;
      this.user = null;

      await ApiClient.request("account/auth/logout", {});
      setTimeout(() => {
        this.loading = false;
      }, 300);
    },
    startLoading() {
      this.loading = true;
    },
    stopLoading() {
      this.loading = false;
    },
  },
  getters: {
    isAuthenticated: (state) => !!state.user,
    isPrivilaged: (state) => state.user.role === "moderator" || state.user.role === "admin",
    isAdmin: (state) => state.user.role === "admin",
    isUser: (state) => state.user.role === "user",
  },
});
