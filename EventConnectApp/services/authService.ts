import { AxiosResponse } from "axios";
import { axiosApiInstance } from "./axios";
import { LoginResult } from "@/types/LoginResult";
import { User } from "@/types/User";

const API_URL = 'Auth';

export const authKeys = {

};

export const authService = {
  login,
  register,
  forgotPassword,
  resetPassword,
  logout,
};

function login(username: string, password: string): Promise<AxiosResponse<LoginResult>> {

  return axiosApiInstance.post(API_URL + `/login`,
    { username, password },
    {
      headers: { 'accept': '*/*', 'Content-Type': 'application/json' }
    });
};

function register(username: string, email: string, password: string): Promise<AxiosResponse<LoginResult>> {

  return axiosApiInstance.post(API_URL + `/register`,
    { username, email, password },
    {
      headers: { 'accept': '*/*', 'Content-Type': 'application/json' }
    });
};

function forgotPassword(email: string): Promise<AxiosResponse<any>> {
  return axiosApiInstance.post(API_URL + `/forgot-password`, { email });
}

function resetPassword(email: string, token: string, newPassword: string): Promise<AxiosResponse<any>> {
  return axiosApiInstance.post(API_URL + '/reset-password', { email, token, newPassword });
}

function logout(token: string): Promise<AxiosResponse<any>> {
  return axiosApiInstance.post(API_URL + `/logout`,{},
    {
      headers: { Authorization: `Bearer ${token}` },
    }
  );
}
