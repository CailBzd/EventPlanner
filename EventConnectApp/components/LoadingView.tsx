import { LoadingStyles } from '@/styles/components/LoadingStyles';
import { Colors } from '@/styles/screens/globalStyles';
import React from 'react';
import { View, Text, ActivityIndicator } from 'react-native';

const LoadingScreen = () => {
  return (
    <View style={LoadingStyles.container}>
      <ActivityIndicator size="large" color={Colors.primaryColor} />
      <Text style={LoadingStyles.loadingText}>Loading...</Text>
    </View>
  );
};

export default LoadingScreen;
