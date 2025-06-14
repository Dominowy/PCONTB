import Axios from "axios";
import router from "@/router";
import { useStore } from "@/store/index";

export class ApiClient {
  constructor(baseUrl) {
    this.$http = Axios.create({ baseURL: baseUrl, withCredentials: true });

    this.$http.interceptors.response.use(
      (response) => response,
      (error) => {
        const store = useStore();
        const status = error?.response?.status;

        if (store.user && !store.loading && status === 401) {
          router.push("/");
        }

        return Promise.reject(error);
      }
    );
  }

  async request(url, data, config) {
    try {
      let response = await this.$http.post(url, data, config);
      return response.data;
    } catch (error) {
      throw this.handleError(error);
    }
  }

  async validate(url, onlyValidate, data, config) {
    let isValidating = onlyValidate === true;

    let suffix = isValidating ? "/validate" : "";
    let validationUrl = url + suffix;

    try {
      let response = await this.$http.post(validationUrl, data, config);

      return response.data;
    } catch (error) {
      throw this.handleError(error);
    }
  }

  async requestFormData(url, formData, config = {}) {
    try {
      const response = await this.$http.post(url, formData, {
        headers: {
          'Content-Type': 'multipart/form-data',
          ...(config.headers || {})
        },
        ...config
      });
      return response.data;
    } catch (error) {
      throw this.handleError(error);
    }
  }

  async requestFormDataValidate(url, onlyValidate, formData, config = {}) {
    let isValidating = onlyValidate === true;

    let suffix = isValidating ? "/validate" : "";
    let validationUrl = url + suffix;

    try {
      const response = await this.$http.post(validationUrl, formData, {
        headers: {
          'Content-Type': 'multipart/form-data',
          ...(config.headers || {})
        },
        ...config
      });
      return response.data;
    } catch (error) {
      throw this.handleError(error);
    }
  }

  handleError(error) {
    var response = error.response;

    return {
      code: response.status,
      message: response.data,
    };
  }
}

export default new ApiClient("/api");
