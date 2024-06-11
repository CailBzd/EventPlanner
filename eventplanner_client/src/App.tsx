import React from 'react';
import { Routes, Route, Navigate } from 'react-router-dom';
import AuthWrapper from './AuthWrapper';
import LoginForm from './Components/LoginForm';
import RegisterForm from './Components/RegisterForm';
import UserPage from './Components/UserPage';
import { useAuth } from './AuthContext';
import Header from './Components/Header'; // Importer le composant d'en-tête
import { Layout } from 'antd';

const { Content } = Layout;

const PrivateRoute: React.FC<{ component: React.FC }> = ({ component: Component }) => {
    const { isAuthenticated } = useAuth();

    return isAuthenticated ? <Component /> : <Navigate to="/login" />;
};

const App: React.FC = () => {
    return (
        <AuthWrapper>
            <Layout>
                <Header /> {/* Ajouter l'en-tête ici */}
                <Content style={{ padding: '20px' }}>
                    <Routes>
                        <Route path="/login" element={<LoginForm />} />
                        <Route path="/register" element={<RegisterForm />} />
                        <Route path="/user" element={<PrivateRoute component={UserPage} />} />
                        <Route path="/" element={<Navigate to="/login" />} />
                    </Routes>
                </Content>
            </Layout>
        </AuthWrapper>
    );
};

export default App;
