import React, { useState } from 'react';
import { Text, View, TextInput, Image, TouchableOpacity, Alert } from 'react-native';
import axios from 'axios';
import { authService } from '@/services/authService';
import AsyncStorage from '@react-native-async-storage/async-storage';
import { LoginResult } from '../types/LoginResult';
import Icon from 'react-native-vector-icons/AntDesign';
import { useTranslation } from 'react-i18next';
import { LoginStyles } from '@/styles/screens/loginStyles';
import { useNavigation } from '@react-navigation/native';
import { NativeStackNavigationProp } from '@react-navigation/native-stack';

type LoginScreenNavigationProp = NativeStackNavigationProp<RootStackParamList, 'LoginScreen'>;

export default function LoginScreen() {

  const navigation = useNavigation<LoginScreenNavigationProp>();

  const { t } = useTranslation();

  const [username, setUsername] = useState('');
  const [password, setPassword] = useState('');
  const [showPassword, setShowPassword] = useState(false);

  const handleLogin = async () => {
    try {
      const response = await authService.login("testuser", "Password123!");
      
      if (response.status === 200) {
        const data: LoginResult = response.data;
        if (data.token && data.userId) {
          console.log(data.token);
          await AsyncStorage.setItem('userToken', data.token);
          await AsyncStorage.setItem('userId', data.userId);
          // await AsyncStorage.setItem('userPicture', data.profilePicture);

          navigation.reset({
            index: 0,
            routes: [{ name: 'NewsScreen' }],
          });
          // Alert.alert('Succès', 'Connexion réussie');
        } else {
          Alert.alert('Erreur', 'Identifiants incorrects');
        }
      } else {
        Alert.alert('Erreur', 'Identifiants incorrects');
      }
    } catch (error) {
      if (axios.isAxiosError(error)) {
        axios.isAxiosError(error);
        if (error.response && error.response.status === 401) {
          Alert.alert('Erreur', 'Identifiants incorrects');
        } else {
          Alert.alert('Erreur', 'Une erreur est survenue');
        }
      } else {
        Alert.alert('Erreur', 'Une erreur est survenue');
      }
    }
  };

  const handleForgotPassword = () => {
    navigation.push('ForgotPasswordScreen');
  };

  const handleSignUp = () => {
    navigation.push('RegisterScreen');
  };

  return (
    <View style={LoginStyles.container}>
      <Image source={require('@/assets/images/logo.jpg')} style={LoginStyles.presentationImage} />
      <Text style={LoginStyles.title}>{t("login:title")}</Text>

      <TextInput
        style={LoginStyles.input}
        placeholder={t("login:username")}
        autoCapitalize="none"
        value={username}
        onChangeText={setUsername}
      />
      <View style={LoginStyles.passwordContainer}>
        <TextInput
          style={LoginStyles.passwordInput}
          placeholder={t("login:password")}
          value={password}
          onChangeText={setPassword}
          secureTextEntry={!showPassword}
        />
        <TouchableOpacity
          style={LoginStyles.showPasswordButton}
          onPress={() => setShowPassword(!showPassword)}
        >
          <Icon name={showPassword ? 'eyeo' : 'eye'} size={20} color="#3F9296" />
        </TouchableOpacity>
      </View>

      <TouchableOpacity style={LoginStyles.button} onPress={handleLogin}>
        <Text style={LoginStyles.buttonText}>{t("login:signin")}</Text>
      </TouchableOpacity>

      <TouchableOpacity onPress={handleForgotPassword}>
        <Text style={LoginStyles.link}>{t("login:forgotPassword")}</Text>
      </TouchableOpacity>

      <TouchableOpacity onPress={handleSignUp}>
        <Text style={LoginStyles.link}>{t("login:signup")}</Text>
      </TouchableOpacity>
    </View>
  );
}
