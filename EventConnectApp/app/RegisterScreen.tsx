import React, { useState } from 'react';
import { StyleSheet, Text, Image, View, TextInput, TouchableOpacity, Alert } from 'react-native';
import axios from 'axios';
import { useRouter } from 'expo-router';
import { useTranslation } from 'react-i18next';
import Icon from 'react-native-vector-icons/AntDesign';
import { authService } from '@/services/authService';
import { Colors } from '@/constants/Colors';
import { isMail, isPasswordSecure } from '@/utils/validation';

export default function RegisterScreen() {
  const { t } = useTranslation();
  const [username, setUsername] = useState('');
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [confirmPassword, setConfirmPassword] = useState('');
  const [showPassword, setShowPassword] = useState(false);
  const router = useRouter();

  const handleSignUp = async () => {
    if (username === '' || email === '' || password === '') {
      Alert.alert(t("general:alert.title"), t("general:alert.emptyMessage") )
      return;
  }

  if (!isMail(email)) {
    Alert.alert(t("general:alert.title"),t('general:alert.mailInvalid'));
    return;
  }

  if (password !== confirmPassword) {
    Alert.alert(t("general:alert.title"), t("general:alert.wrongPassword"));
    return;
  }

  if (!isPasswordSecure(password)) {
    Alert.alert(t("general:alert.title"),t('general:alert.passwordNotSecure'));
    return;
  }

  try {
    const response = await authService.register(
      username,
      email,
      password,
    );

    if (response.status === 200) {
      router.push('/LoginScreen');
    } else {
      Alert.alert(t("general:alert.title"), t("general:alert.createAccountfailed"));
    }
  } catch (error) {
    if (axios.isAxiosError(error)) {
      if (error.response && error.response.status === 400) {
        Alert.alert(t("general:alert.title"), t("general:alert.invalidData"));
      } else {
        Alert.alert(t("general:alert.title"), t("general:alert.generalError"));
      }
    } else {
      Alert.alert(t("general:alert.title"), t("general:alert.generalError"));
    }
  }
};

const handleSignIn = () => {
  router.push('/LoginScreen');
};

return (
  <View style={styles.container}>
    <Image source={require('@/assets/images/logo.jpg')} style={styles.presentationImage} />
    <Text style={styles.title}>{t("register:title")}</Text>

    <TextInput
      style={styles.input}
      placeholder={t("register:username")}
      autoCapitalize="none"
      value={username}
      onChangeText={setUsername}
    />
    <TextInput
      style={styles.input}
      placeholder={t("register:mail")}
      autoCapitalize="none"
      value={email}
      onChangeText={setEmail}
    />
    <View style={styles.passwordContainer}>
      <TextInput
        style={styles.passwordInput}
        placeholder={t("register:password")}
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
    <TextInput
      style={styles.input}
      placeholder={t("register:confirmPassword")}
      value={confirmPassword}
      onChangeText={setConfirmPassword}
      secureTextEntry={!showPassword}
    />

    <TouchableOpacity style={styles.button} onPress={handleSignUp}>
      <Text style={styles.buttonText}>{t("register:signup")}</Text>
    </TouchableOpacity>

    <TouchableOpacity onPress={handleSignIn}>
      <Text style={styles.link}>{t("login:signin")}</Text>
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
    color: Colors.light.text,
    fontSize: 16,
    marginBottom: 15,
  },
  showPasswordButton: {
    padding: 10,
    justifyContent: 'center',
    alignItems: 'center',
  },
  presentationImage: {
    width: '100%',
    height: 300,
    borderRadius: 10,
    marginBottom: 20,
  },
  passwordContainer: {
    flexDirection: 'row',
    alignItems: 'center',
    width: '100%',
  },
});
