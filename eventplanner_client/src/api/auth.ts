import axios from 'axios';

const serverUrl = process.env.REACT_APP_SERVER_URL;

export const login = async (username: string, password: string) => {
    return axios.post(`${serverUrl}/api/Auth/login`, { username, password });
};

export const register = async (email: string, username: string, password: string) => {
    return axios.post(`${serverUrl}/api/Auth/register`, { email, username, password });
};
