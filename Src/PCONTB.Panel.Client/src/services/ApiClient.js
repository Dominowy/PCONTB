import Axios from "axios";

export class ApiClient {
  constructor() {
    this.$http = Axios.create();
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

    let suffix = isValidating ? "/validation" : "";
    let validationUrl = url + suffix;

    try {
      let response = await this.$http.post(validationUrl, data, config);

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
