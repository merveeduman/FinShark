import axios from "axios";

const backendApi = axios.create({
  baseURL: "http://localhost:5069",
});

export default backendApi;