import { defineStore } from "pinia";
import ApiClient from "@/services/ApiClient";

export const useStore = defineStore("store", {
  state: () => ({
    user: null,
    loading: false,
    transition: false,
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

      try {
        await ApiClient.request("account/auth/logout", {});
      } finally {
        setTimeout(() => {
          this.loading = false;
        }, 300);
      }
    },
    startTransition() {
      this.transition = true;
    },
    stopTransition() {
      this.transition = false;
    },
    startLoading() {
      this.loading = true;
    },
    stopLoading() {
      this.loading = false;
    },
    hasRole(role) {
      return this.user.roles.includes(role);
    },
  },
  getters: {
    isAuthenticated: (state) => !!state.user,
  },
});
