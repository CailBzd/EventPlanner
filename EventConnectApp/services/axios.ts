import axios from "axios";
import { stringify } from "qs";

export const axiosApiInstance = axios.create({
  // baseURL: 'http://192.168.1.110:5117/',
  baseURL: process.env.EXPO_PUBLIC_API_URL,
  paramsSerializer: (params) => {
    return stringify(params, { skipNulls: true });
  },
});
