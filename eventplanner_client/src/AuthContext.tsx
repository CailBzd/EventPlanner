import React, { createContext, useState, useContext, ReactNode } from 'react';
import { useNavigate } from 'react-router-dom';
import * as authApi from './api/auth';

interface AuthContextType {
    isAuthenticated: boolean;
    login: (username: string, password: string) => Promise<void>;
    register: (email: string, username: string, password: string) => Promise<void>;
    logout: () => void;
}

const AuthContext = createContext<AuthContextType | undefined>(undefined);

export const AuthProvider: React.FC<{ children: ReactNode }> = ({ children }) => {
    const [isAuthenticated, setIsAuthenticated] = useState<boolean>(false);
    const navigate = useNavigate();

    const login = async (username: string, password: string) => {
        try {
            const response = await authApi.login(username, password);

            if (response.status === 200) {
                setIsAuthenticated(true);
                navigate('/user');
            }
        } catch (error) {
            console.error('Login failed', error);
        }
    };

    const register = async (email: string, username: string, password: string) => {
        try {
            const response = await authApi.register(email, username, password);

            if (response.status === 201) {
                setIsAuthenticated(true);
                navigate('/user');
            }
        } catch (error) {
            console.error('Registration failed', error);
        }
    };

    const logout = () => {
        setIsAuthenticated(false);
        navigate('/login');
    };

    return (
        <AuthContext.Provider value={{ isAuthenticated, login, register, logout }}>
            {children}
        </AuthContext.Provider>
    );
};

export const useAuth = () => {
    const context = useContext(AuthContext);
    if (!context) {
        throw new Error('useAuth must be used within an AuthProvider');
    }
    return context;
};
