// app/_layout.tsx
import React from 'react';
import { createNativeStackNavigator } from '@react-navigation/native-stack';
import HomeScreen from './HomeScreen';
import LoginScreen from './LoginScreen';
import RegisterScreen from './RegisterScreen';
import NotFoundScreen from './NotFoundScreen';
import NewsScreen from './NewsScreen';
import ProfileScreen from './ProfileScreen';
const Stack = createNativeStackNavigator();
import '@/utils/i18n'; 
import ForgotPasswordScreen from './ForgotPasswordScreen';
import EventScreen from './EventScreen';


export default function RootNavigator() {
  return (
    <Stack.Navigator initialRouteName="HomeScreen">
      <Stack.Screen name="HomeScreen" component={HomeScreen} options={{ headerShown: false }} />
      <Stack.Screen name="LoginScreen" component={LoginScreen} />
      <Stack.Screen name="RegisterScreen" component={RegisterScreen} />
      <Stack.Screen name="ForgotPasswordScreen" component={ForgotPasswordScreen} />
      <Stack.Screen name="NewsScreen" component={NewsScreen} options={{ headerShown: false }}/>
      <Stack.Screen name="ProfileScreen" component={ProfileScreen} />
      <Stack.Screen name="EventScreen" component={EventScreen} />
      <Stack.Screen name="NotFoundScreen" component={NotFoundScreen} options={{ title: 'Oops!' }} />
    </Stack.Navigator>
  );
}
