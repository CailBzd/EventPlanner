import React from 'react';
import { View, Text, Image, StyleSheet } from 'react-native';
import { Button, Provider } from '@ant-design/react-native';

export default function HomeScreen() {
  return (
    <Provider>
      <View style={styles.container}>
        <View style={styles.header}>
          <Image
            source={{ uri: 'https://via.placeholder.com/150' }}
            style={styles.logo}
          />
          <Text style={styles.title}>Bienvenue</Text>
        </View>
        <View style={styles.buttonContainer}>
          <Button type="primary" style={styles.button}>Se connecter</Button>
          <Button style={styles.button}>S'inscrire</Button>
        </View>
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
  header: {
    alignItems: 'center',
    marginBottom: 40,
  },
  logo: {
    width: 150,
    height: 150,
    marginBottom: 10,
  },
  title: {
    fontSize: 24,
    fontWeight: 'bold',
  },
  buttonContainer: {
    width: '80%',
  },
  button: {
    marginBottom: 10,
  },
});
