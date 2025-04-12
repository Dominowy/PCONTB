import Axios from "axios";

export class ApiClient {
  constructor() {
    this.$http = Axios.create();
  }

  async request(url, data, config) {
    try {
      var response = await this.$http.post(url, data, config);
      return response.data;
    } catch (error) {
      throw this.handleError(error);
    }
  }

  async validate(url, onlyValidate, data, config) {
    var isValidating = onlyValidate === true;

    var suffix = isValidating ? "/validation" : "";
    url = url + suffix;

    try {
      var response = await this.$http.post(url, data, config);
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

export default new ApiClient();
