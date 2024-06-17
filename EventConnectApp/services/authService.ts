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
  forgotPassword
};

function login(username: string, password: string): Promise<AxiosResponse<LoginResult>> {

  return axiosApiInstance.post(API_URL + `/login`,
    { username, password }, 
    {
    headers: {'accept': '*/*','Content-Type': 'application/json'}
  });
};

function register(username: string, email: string, password: string): Promise<AxiosResponse<LoginResult>> {

  return axiosApiInstance.post(API_URL + `/register`,
    { username, email, password }, 
    {
    headers: {'accept': '*/*','Content-Type': 'application/json'}
  });
};

function forgotPassword(email: string): Promise<AxiosResponse<LoginResult>> {
  return axiosApiInstance.post(API_URL + `/forgotPassword`,
    { email }, 
    {
    headers: {'accept': '*/*','Content-Type': 'application/json'}
  });
};



