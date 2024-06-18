import React, { useState } from 'react';
import { Text, Image, View, TextInput, TouchableOpacity, Alert } from 'react-native';
import axios from 'axios';
import { useTranslation } from 'react-i18next';
import Icon from 'react-native-vector-icons/AntDesign';
import { authService } from '@/services/authService';
import { isMail, isPasswordSecure } from '@/utils/validation';
import { RegisterStyles } from '@/styles/screens/registerStyles'; import { useNavigation } from '@react-navigation/native';
import { NativeStackNavigationProp } from '@react-navigation/native-stack';

type LoginScreenNavigationProp = NativeStackNavigationProp<RootStackParamList, 'LoginScreen'>;

export default function RegisterScreen() {
  const { t } = useTranslation();
  const navigation = useNavigation<LoginScreenNavigationProp>();

  const [username, setUsername] = useState('');
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [confirmPassword, setConfirmPassword] = useState('');
  const [showPassword, setShowPassword] = useState(false);

  const handleSignUp = async () => {
    if (username === '' || email === '' || password === '') {
      Alert.alert(t("register:title"), t("general:alert.emptyMessage"))
      return;
    }

    if (!isMail(email)) {
      Alert.alert(t("register:title"), t('general:alert.mailInvalid'));
      return;
    }

    if (password !== confirmPassword) {
      Alert.alert(t("register:title"), t("general:alert.wrongPassword"));
      return;
    }

    if (!isPasswordSecure(password)) {
      Alert.alert(t("register:title"), t('general:alert.passwordNotSecure'));
      return;
    }

    try {
      const response = await authService.register(
        username,
        email,
        password,
      );

      if (response.status === 200) {
        Alert.alert(t('general:success'), t('general:alert.accountCreated'), [
          {
            text: t('ok'),
            onPress: () => navigation.push('LoginScreen'),
          },
        ]);
      } else {
        Alert.alert(t("general:error"), t("general:alert.createAccountfailed"));
      }
    } catch (error) {
      if (axios.isAxiosError(error)) {
        if (error.response && error.response.status === 400) {
          Alert.alert(t("general:error"), t("general:alert.invalidData"));
        } else {
          Alert.alert(t("general:error"), t("general:alert.generalError"));
        }
      } else {
        Alert.alert(t("general:error"), t("general:alert.generalError"));
      }
    }
  };

  const handleSignIn = () => {
    navigation.push('LoginScreen');
  };

  return (
    <View style={RegisterStyles.container}>
      <Image source={require('@/assets/images/logo.jpg')} style={RegisterStyles.presentationImage} />
      <Text style={RegisterStyles.title}>{t("register:title")}</Text>

      <TextInput
        style={RegisterStyles.input}
        placeholder={t("register:username")}
        autoCapitalize="none"
        value={username}
        onChangeText={setUsername}
      />
      <TextInput
        style={RegisterStyles.input}
        placeholder={t("register:mail")}
        autoCapitalize="none"
        value={email}
        onChangeText={setEmail}
      />
      <View style={RegisterStyles.passwordContainer}>
        <TextInput
          style={RegisterStyles.passwordInput}
          placeholder={t("register:password")}
          value={password}
          onChangeText={setPassword}
          secureTextEntry={!showPassword}
        />
        <TouchableOpacity
          style={RegisterStyles.showPasswordButton}
          onPress={() => setShowPassword(!showPassword)}
        >
          <Icon name={showPassword ? 'eyeo' : 'eye'} size={20} color="#3F9296" />
        </TouchableOpacity>
      </View>
      <TextInput
        style={RegisterStyles.input}
        placeholder={t("register:confirmPassword")}
        value={confirmPassword}
        onChangeText={setConfirmPassword}
        secureTextEntry={!showPassword}
      />

      <TouchableOpacity style={RegisterStyles.button} onPress={handleSignUp}>
        <Text style={RegisterStyles.buttonText}>{t("register:signup")}</Text>
      </TouchableOpacity>

      <TouchableOpacity onPress={handleSignIn}>
        <Text style={RegisterStyles.link}>{t("login:signin")}</Text>
      </TouchableOpacity>
    </View>
  );
}
