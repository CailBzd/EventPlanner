import React from 'react';
import { Button } from 'antd';
import { useAuth } from '../AuthContext';

const UserPage: React.FC = () => {
    const { logout } = useAuth();

    return (
        <div style={{ textAlign: 'center', marginTop: '50px' }}>
            <h1>Welcome, User!</h1>
            <Button type="primary" onClick={logout}>
                Logout
            </Button>
        </div>
    );
};

export default UserPage;
