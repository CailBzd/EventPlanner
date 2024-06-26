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

dayjs.locale("fr");

const Stack = createNativeStackNavigator<RootStackParamList>();


export default function RootNavigator() {

  const { t } = useTranslation();

  const [isLoading, setIsLoading] = useState(true);
  const [initialRouteName, setInitialRouteName] = useState<keyof RootStackParamList>('HomeScreen');

  useEffect(() => {
    const checkLoginStatus = async () => {
      const token = await AsyncStorage.getItem('userToken');
      if (token) {
        // Optionally, you could validate the token with your backend here
        setInitialRouteName('NewsScreen');
      }
      setIsLoading(false);
      setInitialRouteName('HomeScreen');
    };

    checkLoginStatus();
  }, []);

  if (isLoading) {
    return null; // or a loading spinner
  }


  return (
    <Stack.Navigator initialRouteName={initialRouteName}>
      <Stack.Screen name="NewsScreen" component={NewsScreen} options={{ headerShown: false, }} />
      <Stack.Screen name="ProfileScreen" component={ProfileScreen} options={{ title: t("profile:headerTitle") }} />
      <Stack.Screen name="EventScreen" component={EventScreen} options={{ headerShown: false }} />
      <Stack.Screen name="NotFoundScreen" component={NotFoundScreen} options={{ title: 'Oops!' }} />
      <Stack.Screen name="HomeScreen" component={HomeScreen} options={{ headerShown: false }} />
      <Stack.Screen name="LoginScreen" component={LoginScreen} options={{ headerShown: false }} />
      <Stack.Screen name="RegisterScreen" component={RegisterScreen} options={{ headerShown: false }} />
      <Stack.Screen name="CalendarScreen" component={CalendarScreen} options={{ headerShown: false }} />
      <Stack.Screen name="ForgotPasswordScreen" component={ForgotPasswordScreen} options={{ headerShown: false }} />
      <Stack.Screen name="CreateEventScreen" component={CreateEventScreen} options={{ headerShown: false }} />
    </Stack.Navigator>
  );
}
