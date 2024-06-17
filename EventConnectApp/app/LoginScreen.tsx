import React, { useState } from 'react';
import { StyleSheet, Text, View, TextInput, Image, TouchableOpacity, Alert } from 'react-native';
import axios from 'axios';
import { authService } from '@/services/authService';
import { useRouter } from 'expo-router';
import AsyncStorage from '@react-native-async-storage/async-storage';
import { LoginResult } from '../types/LoginResult';
import Icon from 'react-native-vector-icons/AntDesign';
import { useTranslation } from 'react-i18next';

export default function LoginScreen() {

  const { t } = useTranslation();

  const [username, setUsername] = useState('');
  const [password, setPassword] = useState('');
  const [showPassword, setShowPassword] = useState(false);
  const router = useRouter();

  const handleLogin = async () => {
    try {
      const response = await authService.login(username, password);
      if (response.status === 200) {
        const data: LoginResult = response.data;
        if (data.token && data.userId) {
          await AsyncStorage.setItem('userToken', data.token);
          await AsyncStorage.setItem('userId', data.userId);

          router.push('/NewsScreen');
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
    router.push('/ForgotPasswordScreen');
  };

  const handleSignUp = () => {
    router.push('/RegisterScreen');
  };

  return (
    <View style={styles.container}>
      <Image source={require('@/assets/images/logo.jpg')} style={styles.presentationImage} />
      <Text style={styles.title}>{t("login:title")}</Text>

      <TextInput
        style={styles.input}
        placeholder={t("login:username")}
        autoCapitalize="none"
        value={username}
        onChangeText={setUsername}
      />
      <View style={styles.passwordContainer}>
        <TextInput
          style={styles.passwordInput}
          placeholder={t("login:password")}
          value={password}
          onChangeText={setPassword}
          secureTextEntry={!showPassword}
        />
        <TouchableOpacity
          style={styles.showPasswordButton}
          onPress={() => setShowPassword(!showPassword)}
        >
          <Icon name={showPassword ? 'eyeo' : 'eye'} size={20} color="#3F9296" />
        </TouchableOpacity>
      </View>

      <TouchableOpacity style={styles.button} onPress={handleLogin}>
        <Text style={styles.buttonText}>{t("login:signin")}</Text>
      </TouchableOpacity>

      <TouchableOpacity onPress={handleForgotPassword}>
        <Text style={styles.link}>{t("login:forgotPassword")}</Text>
      </TouchableOpacity>

      <TouchableOpacity onPress={handleSignUp}>
        <Text style={styles.link}>{t("login:signup")}</Text>
      </TouchableOpacity>
    </View>
  );
}

const styles = StyleSheet.create({
  container: {
    flex: 1,
    justifyContent: 'center',
    alignItems: 'center',
    padding: 20,
    backgroundColor: '#fff',
  },
  title: {
    fontSize: 28,
    fontWeight: 'bold',
    marginBottom: 20,
  },
  input: {
    width: '100%',
    height: 50,
    borderColor: '#ddd',
    borderWidth: 1,
    borderRadius: 5,
    paddingHorizontal: 10,
    marginBottom: 15,
  },
  passwordInput: {
    flex: 1,
    height: 50,
    borderColor: '#ddd',
    borderWidth: 1,
    borderRadius: 5,
    paddingHorizontal: 10,
    marginBottom: 15,
  },
  presentationImage: {
    width: '100%',
    height: 300,
    borderRadius: 10,
    marginBottom: 20,
  },
  button: {
    width: '100%',
    height: 50,
    backgroundColor: '#3F9296',
    borderRadius: 5,
    justifyContent: 'center',
    alignItems: 'center',
    marginBottom: 20,
  },
  buttonText: {
    color: '#fff',
    fontSize: 18,
    fontWeight: 'bold',
  },
  link: {
    color: '#3F9296',
    fontSize: 16,
    marginBottom: 15,
  },
  showPasswordButton: {
    padding: 10,
    justifyContent: 'center',
    alignItems: 'center',
  },
  passwordContainer: {
    flexDirection: 'row',
    alignItems: 'center',
    width: '100%',
  },
});
