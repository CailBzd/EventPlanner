import React from 'react';
import { View, Text, StyleSheet } from 'react-native';
import { Provider, Button } from '@ant-design/react-native';

export default function LoginScreen() {
  return (
    <Provider>
      <View style={styles.container}>
        <Text style={styles.title}>Écran de connexion</Text>
        <Button type="primary">Connexion</Button>
      </View>
    </Provider>
  );
}

const styles = StyleSheet.create({
  container: {
    flex: 1,
    justifyContent: 'center',
    alignItems: 'center',
    backgroundColor: '#f5f5f5',
  },
  title: {
    fontSize: 24,
    fontWeight: 'bold',
    marginBottom: 20,
  },
});
