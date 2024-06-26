import { LoadingStyles } from '@/styles/components/LoadingStyles';
import { Colors } from '@/styles/screens/globalStyles';
import React from 'react';
import { useTranslation } from 'react-i18next';
import { View, Text, ActivityIndicator } from 'react-native';


const LoadingScreen = () => {
  
  const { t } = useTranslation();
  
  return (
    <View style={LoadingStyles.container}>
      <ActivityIndicator size="large" color={Colors.primaryColor} />
      <Text style={LoadingStyles.loadingText}>{t('general:loading')}</Text>
    </View>
  );
};

export default LoadingScreen;
