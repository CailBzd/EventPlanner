import { StyleSheet } from 'react-native';
import { Colors } from '../screens/globalStyles';

export const LoadingStyles = StyleSheet.create({
    container: {
      flex: 1,
      justifyContent: 'center',
      alignItems: 'center',
      backgroundColor: Colors.light.background,
    },
    loadingText: {
      marginTop: 20,
      fontSize: 18,
      color: Colors.primaryColor,
    },
  });