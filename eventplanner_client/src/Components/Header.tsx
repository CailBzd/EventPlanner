import React from 'react';
import { Layout } from 'antd';

const { Header: AntHeader } = Layout;

const Header: React.FC = () => {
    return (
        <AntHeader style={{ display: 'flex', alignItems: 'center', padding: '0 20px' }}>
            <img src="/logo.jpg" alt="EventConnect Logo" style={{ height: '40px', marginRight: '20px' }} />
            <h1 style={{ color: 'white', margin: 0 }}>EventConnect</h1>
        </AntHeader>
    );
};

export default Header;
