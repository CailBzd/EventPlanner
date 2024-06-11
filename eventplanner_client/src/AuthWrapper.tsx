import React, { ReactNode } from 'react';
import { BrowserRouter as Router } from 'react-router-dom';
import { AuthProvider } from './AuthContext';

interface AuthWrapperProps {
    children: ReactNode;
}

const AuthWrapper: React.FC<AuthWrapperProps> = ({ children }) => {
    return (
        <Router>
            <AuthProvider>
                {children}
            </AuthProvider>
        </Router>
    );
};

export default AuthWrapper;
