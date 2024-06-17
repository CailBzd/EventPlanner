import React, { useState } from 'react';
import { View, Text, TextInput, TouchableOpacity, StyleSheet, Alert } from 'react-native';
import { useTranslation } from 'react-i18next';
import { userService } from '@/services/userService'; // Assurez-vous que ce service est correctement configuré pour gérer les demandes de réinitialisation de mot de passe

const ForgotPasswordScreen = () => {
  const { t } = useTranslation();
  const [email, setEmail] = useState('');
  const [isLoading, setIsLoading] = useState(false);

  const handleForgotPassword = async () => {
    if (!email) {
      Alert.alert(t('forgotPassword:enterEmail'));
      return;
    }

    setIsLoading(true);
    try {
      await userService.forgotPassword(email);
      Alert.alert(t('forgotPassword:successMessage'));
    } catch (error) {
      console.error('Error requesting password reset:', error);
      Alert.alert(t('forgotPassword:errorMessage'));
    } finally {
      setIsLoading(false);
    }
  };

  return (
    <View style={styles.container}>
      <Text style={styles.title}>{t('forgotPassword:title')}</Text>
      <TextInput
        style={styles.input}
        placeholder={t('forgotPassword:emailPlaceholder')}
        value={email}
        onChangeText={setEmail}
        keyboardType="email-address"
        autoCapitalize="none"
      />
      <TouchableOpacity style={styles.button} onPress={handleForgotPassword} disabled={isLoading}>
        <Text style={styles.buttonText}>
          {isLoading ? t('forgotPassword:loading') : t('forgotPassword:submitButton')}
        </Text>
      </TouchableOpacity>
    </View>
  );
};

const styles = StyleSheet.create({
  container: {
    flex: 1,
    justifyContent: 'center',
    padding: 20,
    backgroundColor: '#fff',
  },
  title: {
    fontSize: 24,
    marginBottom: 20,
    textAlign: 'center',
  },
  input: {
    borderWidth: 1,
    borderColor: '#ddd',
    borderRadius: 5,
    padding: 10,
    marginBottom: 20,
    fontSize: 16,
  },
  button: {
    backgroundColor: '#3F9296',
    padding: 15,
    borderRadius: 5,
    alignItems: 'center',
  },
  buttonText: {
    color: '#fff',
    fontSize: 16,
    fontWeight: 'bold',
  },
});

export default ForgotPasswordScreen;
