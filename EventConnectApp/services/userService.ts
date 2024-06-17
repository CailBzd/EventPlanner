import { AxiosResponse } from "axios";
import { axiosApiInstance } from "./axios";
import { LoginResult } from "@/types/LoginResult";
import { User } from "@/types/User";

const API_URL = 'Users';

export const userKeys = {

};

export const userService = {
  updateUser,
  getUser
};


function updateUser(userData: User, token: string): Promise<AxiosResponse<void>> {
  return axiosApiInstance.put(API_URL + '/update', userData, {
    headers: {
      'accept': '*/*',
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${token}`,
    },
  });
}

function getUser(id: string, token: string): Promise<AxiosResponse<User>> {
  return axiosApiInstance.get(API_URL + '/' + id, {
    headers: {
      'accept': '*/*',
      'Authorization': `Bearer ${token}`,
    },
  });
}

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
