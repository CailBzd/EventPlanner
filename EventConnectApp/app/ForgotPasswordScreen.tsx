import React, { useState } from 'react';
import { View, Text, TextInput, TouchableOpacity, Image, Alert } from 'react-native';
import { useTranslation } from 'react-i18next';
import { authService } from '@/services/authService';
import { ForgotPasswordStyles } from '@/styles/screens/forgotPasswordStyles';
import { isMail } from '@/utils/validation';
import { useNavigation } from '@react-navigation/native';
import { NativeStackNavigationProp } from '@react-navigation/native-stack';

type LoginScreenNavigationProp = NativeStackNavigationProp<RootStackParamList, 'LoginScreen'>;
export default function ForgotPasswordScreen() {
  const { t } = useTranslation();
  const [email, setEmail] = useState('');
  const [isLoading, setIsLoading] = useState(false);
  const navigation = useNavigation<LoginScreenNavigationProp>();

  const handleForgotPassword = async () => {
    if (!email) {
      Alert.alert(t('forgotPassword:enterEmail'));
      return;
    }

    if (!isMail(email)) {
      Alert.alert(t("forgotPassword:title"), t('general:alert.mailInvalid'));
      return;
    }

    setIsLoading(true);
    try {
      const response = await authService.forgotPassword(email);
      if (response.status === 200) {
        Alert.alert(t('general:success'), t('forgotPassword:emailSent'));
      } else {
        Alert.alert(t('general:error'), t('forgotPassword:failedToSendEmail'));
      }
    } catch (error) {
      Alert.alert(t('general:error'), t('forgotPassword:anErrorOccurred'));
    } finally {
      setIsLoading(false);
    }
  };

  const handleSignUp = () => {
    navigation.push('LoginScreen');
  };

  return (
    <View style={ForgotPasswordStyles.container}>
      <Image source={require('@/assets/images/logo.jpg')} style={ForgotPasswordStyles.presentationImage} />
      <Text style={ForgotPasswordStyles.title}>{t('forgotPassword:title')}</Text>
      <View style={ForgotPasswordStyles.mainContainer}>
        <TextInput
          style={ForgotPasswordStyles.input}
          placeholder={t('forgotPassword:emailPlaceholder')}
          value={email}
          onChangeText={setEmail}
          keyboardType="email-address"
          autoCapitalize="none"
        />
      </View>
      <TouchableOpacity style={ForgotPasswordStyles.button} onPress={handleForgotPassword} disabled={isLoading}>
        <Text style={ForgotPasswordStyles.buttonText}>
          {isLoading ? t('forgotPassword:loading') : t('forgotPassword:submitButton')}
        </Text>
      </TouchableOpacity>

      <TouchableOpacity onPress={handleSignUp}>
        <Text style={ForgotPasswordStyles.link}>{t("general:back")}</Text>
      </TouchableOpacity>
    </View>
  );
};

