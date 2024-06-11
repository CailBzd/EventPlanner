import React from 'react';
import { Form, Input, Button } from 'antd';
import { UserOutlined, LockOutlined } from '@ant-design/icons';
import { useAuth } from '../AuthContext';

const LoginForm: React.FC = () => {
    const { login } = useAuth();

    const onFinish = async (values: any) => {
        await login(values.username, values.password);
    };

    return (
        <Form
            name="login_form"
            initialValues={{ remember: true }}
            onFinish={onFinish}
            style={{ maxWidth: '300px', margin: 'auto', marginTop: '50px' }}
        >
            <Form.Item
                name="username"
                rules={[{ required: true, message: 'Please input your Username!' }]}
            >
                <Input prefix={<UserOutlined />} placeholder="Username" />
            </Form.Item>
            <Form.Item
                name="password"
                rules={[{ required: true, message: 'Please input your Password!' }]}
            >
                <Input
                    prefix={<LockOutlined />}
                    type="password"
                    placeholder="Password"
                />
            </Form.Item>
            <Form.Item>
                <Button type="primary" htmlType="submit" style={{ width: '100%' }}>
                    Log in
                </Button>
            </Form.Item>
        </Form>
    );
};

export default LoginForm;
