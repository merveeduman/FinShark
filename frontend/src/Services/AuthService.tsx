import axios from "axios";
import { UserProfileToken } from "../Models/User";

const api = "http://localhost:5069/api/";

export const loginAPI = async (username: string, password: string) => {
  const response = await axios.post<UserProfileToken>(api + "account/login", {
    username,
    password,
  });

  return response.data;
};

export const registerAPI = async (
  email: string,
  username: string,
  password: string
) => {
  const response = await axios.post<UserProfileToken>(api + "account/register", {
    email,
    username,
    password,
  });

  return response.data;
};