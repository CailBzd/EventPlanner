import React, { useState } from 'react';
import { StyleSheet, Text, View, TextInput, Image, TouchableOpacity, Alert } from 'react-native';
import { useNavigation } from '@react-navigation/native';
import axios, { AxiosError } from 'axios';
import { authService } from '@/services/authService';
import { router } from 'expo-router';
import { useRouter } from 'expo-router';
import AsyncStorage from '@react-native-async-storage/async-storage';
import { LoginResult } from '../types/LoginResult';

export default function RegisterScreen() {
  const [username, setUsername] = useState('');
  const [password, setPassword] = useState('');
  const [showPassword, setShowPassword] = useState(false);
  const router = useRouter();

  const handleLogin = async () => {
    try {

      // const response = await authService.login(username, password);
      const response = await authService.login("testuser", "Password123!");
      if (response.status === 200) {
        const data: LoginResult = response.data;
        console.log(data);
        if (data.token && data.userId) {
          console.log("OK");
          // Stockez le token et l'ID de l'utilisateur pour une utilisation ultérieure
          await AsyncStorage.setItem('userToken', data.token);
          await AsyncStorage.setItem('userId', data.userId);

          router.push('/dashboard');
          Alert.alert('Success', 'Logged in successfully');
        } else {
          Alert.alert('Erreur', 'Identifiants incorrects');
        }
      } else {
        Alert.alert('Erreur', 'Identifiants incorrects');
      }
    } catch (error: unknown) {

      if (axios.isAxiosError(error)) {
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
    // Gérer la navigation vers la page de réinitialisation du mot de passe
    Alert.alert('Mot de passe oublié', 'Fonctionnalité non implérmentée.');
  };

  const handleSignUp = () => {
    // navigation.navigate('SignUp');
  };

  return (
    <View style={styles.container}>

      <Image source={require('@/assets/images/logo.jpg')} style={styles.presentationImage} />
      <Text style={styles.title}>S'inscrire</Text>

      <TextInput
        style={styles.input}
        placeholder="Nom d'utilisateur"
        autoCapitalize="none"
        value={username}
        onChangeText={setUsername}
      />
      <View style={styles.passwordContainer}>
        <TextInput
          style={styles.input}
          placeholder="Mot de passe"
          value={password}
          onChangeText={setPassword}
          secureTextEntry={!showPassword}
        />
        <TouchableOpacity
          style={styles.showPasswordButton}
          onPress={() => setShowPassword(!showPassword)}
        >
          <Text style={styles.showPasswordText}>
            {showPassword ? 'Cacher' : 'Voir'}
          </Text>
        </TouchableOpacity>
      </View>

      <TouchableOpacity style={styles.button} onPress={handleLogin}>
        <Text style={styles.buttonText}>Se connecter</Text>
      </TouchableOpacity>

      <TouchableOpacity onPress={handleForgotPassword}>
        <Text style={styles.link}>Mot de passe oublié ?</Text>
      </TouchableOpacity>

      <TouchableOpacity onPress={handleSignUp}>
        <Text style={styles.link}>S'inscrire</Text>
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
  presentationImage: {
    width: '100%',
    height: 300,
    borderRadius: 10,
    marginBottom: 20,
  },
  button: {
    width: '100%',
    height: 50,
    backgroundColor: '#3b5998',
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
    color: '#3b5998',
    fontSize: 16,
    marginBottom: 15,
  },
  showPasswordButton: {
    position: 'absolute',
    right: 10,
  },
  showPasswordText: {
    color: '#007BFF',
    fontSize: 16,
  },
  passwordContainer: {
    flexDirection: 'row',
    alignItems: 'center',
    width: '100%',
  },
});
