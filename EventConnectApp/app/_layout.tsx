// app/_layout.tsx
import React, { useEffect, useState } from 'react';
import { createNativeStackNavigator } from '@react-navigation/native-stack';
import HomeScreen from './HomeScreen';
import LoginScreen from './LoginScreen';
import RegisterScreen from './RegisterScreen';
import NotFoundScreen from './NotFoundScreen';
import NewsScreen from './NewsScreen';
import ProfileScreen from './ProfileScreen';
import '@/utils/i18n';
import "dayjs/locale/fr";
import * as dayjs from "dayjs";
import ForgotPasswordScreen from './ForgotPasswordScreen';
import EventScreen from './EventScreen';
import { useTranslation } from 'react-i18next';
import AsyncStorage from '@react-native-async-storage/async-storage';
import CreateEventScreen from './CreateEventScreen';
import CalendarScreen from './CalendarScreen';
import { Tabs } from 'expo-router';
import { Icon } from '@/components/ui';
import { useAuth } from '@/services/AuthContext';

dayjs.locale("fr");

const Stack = createNativeStackNavigator<RootStackParamList>();


export default function RootNavigator() {

  const { t } = useTranslation();
  function AuthenticatedTabs() {
    return (
      <Tabs>
        <Tabs.Screen name="NewsScreen" options={{ title: 'News', tabBarIcon: () => <Icon name="newspaper-o" size={20} /> }} />
        <Tabs.Screen name="CreateEventScreen" options={{ title: 'Create Event', tabBarIcon: () => <Icon name="plus-circle" size={20} /> }} />
        <Tabs.Screen name="ContactsScreen" options={{ title: 'Contacts', tabBarIcon: () => <Icon name="address-book" size={20} /> }} />
        <Tabs.Screen name="ProfileScreen" options={{ title: 'Profile', tabBarIcon: () => <Icon name="user" size={20} /> }} />
      </Tabs>
    );
  }
  
  function UnauthenticatedStack() {
    return (
      <Stack.Navigator>
        <Stack.Screen name="HomeScreen" component={HomeScreen} options={{ headerShown: false }} />
        <Stack.Screen name="LoginScreen" component={LoginScreen} options={{ headerShown: false }} />
        <Stack.Screen name="RegisterScreen" component={RegisterScreen} options={{ headerShown: false }} />
      </Stack.Navigator>
    );
  }
  
  function AppNavigator() {
    const { isAuthenticated } = useAuth();
    console.log("IsAuthenticate : " + isAuthenticated);
    return isAuthenticated ? <AuthenticatedTabs /> : <UnauthenticatedStack />;
  }


  return (
    <Stack.Navigator>
      {/* <Stack.Screen name="NewsScreen" component={NewsScreen} options={{ headerShown: false, }} />
      <Stack.Screen name="ProfileScreen" component={ProfileScreen} options={{ title: t("profile:headerTitle") }} />
      <Stack.Screen name="EventScreen" component={EventScreen} options={{ headerShown: false }} />
      <Stack.Screen name="NotFoundScreen" component={NotFoundScreen} options={{ title: 'Oops!' }} />
      <Stack.Screen name="HomeScreen" component={HomeScreen} options={{ headerShown: false }} />
      <Stack.Screen name="LoginScreen" component={LoginScreen} options={{ headerShown: false }} />
      <Stack.Screen name="RegisterScreen" component={RegisterScreen} options={{ headerShown: false }} />
      <Stack.Screen name="CalendarScreen" component={CalendarScreen} options={{ headerShown: false }} />
      <Stack.Screen name="ForgotPasswordScreen" component={ForgotPasswordScreen} options={{ headerShown: false }} />
      <Stack.Screen name="CreateEventScreen" component={CreateEventScreen} options={{ headerShown: false }} /> */}

      <AppNavigator />
    </Stack.Navigator>
  );
}
