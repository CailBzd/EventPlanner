import { AxiosResponse } from "axios";
import { axiosApiInstance } from "./axios";
import { LoginResult } from "@/types/LoginResult";
import { User } from "@/types/User";

const API_URL = 'Auth';

export const authKeys = {

};

export const authService = {
  login,
};

function login(username: string, password: string): Promise<AxiosResponse<LoginResult>> {

  return axiosApiInstance.post(API_URL + `/login`,
    { username, password }, 
    {
    headers: {'accept': '*/*','Content-Type': 'application/json'}
  });
};


// export const registerService = async (username, password) => {
//   return await axios.post(
//     `${API_URL}/register`,
//     { "username": username, "password": password },
//     { headers: { 'Content-Type': 'application/json', 'accept': '*/*' } }
//   );
// };

// export const logoutService = async () => {
//   return await axios.post(
//     `${API_URL}/logout`,
//     {},
//     { headers: { 'Content-Type': 'application/json', 'accept': '*/*' } }
//   );
// };
