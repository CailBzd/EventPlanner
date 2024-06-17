import React, { useState, useEffect } from 'react';
import { View, Text, StyleSheet, FlatList, TouchableOpacity, Image, Button } from 'react-native';
import AsyncStorage from '@react-native-async-storage/async-storage';
import { router } from 'expo-router';
import { User } from '@/types/User';
import { userService } from '@/services/userService';

const data = [
  { id: '1', title: 'News Item 1', content: 'Content for news item 1' },
  { id: '2', title: 'News Item 2', content: 'Content for news item 2' },
  { id: '3', title: 'News Item 3', content: 'Content for news item 3' },
  // Ajoutez plus d'articles de journal ici
];

const NewsScreen = () => {
  const [user, setUser] = useState<User | null>();

  useEffect(() => {
    const loadUserData = async () => {
      const storedUserId = await AsyncStorage.getItem('userId');
      const storedToken = await AsyncStorage.getItem('userToken');
      if (storedUserId && storedToken) {
        try {
          const response = await userService.getUser(storedUserId, storedToken);
          setUser(response.data);
        } catch (error) {
          console.error('Error fetching user data:', error);
        }
      }
    };

    loadUserData();
  }, []);

  const handleCreateEvent = () => {
    // Logique pour créer un événement
    console.log("Créer un événement");
  };

  const handleAvatarPress = () => {
    router.push('/ProfileScreen');
  };

  const avatarSource = user?.profilePicture
  ? { uri: `data:image/png;base64,${user.profilePicture}` }
  : require('../assets/images/logo.jpg');  // Assurez-vous d'avoir une image par défaut dans votre dossier assets


  return (
    <View style={styles.container}>
      <View style={styles.header}>
        <Text style={styles.title}>Journal</Text>
        <TouchableOpacity onPress={handleAvatarPress}>
          <Image
            source={avatarSource}
            style={styles.avatar}
          />
        </TouchableOpacity>
      </View>
      <FlatList
        data={data}
        keyExtractor={(item) => item.id}
        renderItem={({ item }) => (
          <View style={styles.newsItem}>
            <Text style={styles.newsTitle}>{item.title}</Text>
            <Text>{item.content}</Text>
          </View>
        )}
      />
      <View style={styles.buttonContainer}>
        <TouchableOpacity style={styles.button} onPress={handleCreateEvent}>
          <Text style={styles.buttonText}>Créer un événement</Text>
        </TouchableOpacity>
      </View>
    </View>
  );
};

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: '#fff',
  },
  header: {
    flexDirection: 'row',
    justifyContent: 'space-between',
    alignItems: 'center',
    padding: 20,
    paddingTop: 40,
    marginBottom: 10,
    backgroundColor: '#3F9296',
  },
  title: {
    fontSize: 24,
    color: '#fff',
  },
  avatar: {
    width: 40,
    height: 40,
    borderRadius: 20,
    borderWidth: 2,
    borderColor: '#fff',
  },
  newsItem: {
    marginBottom: 15,
    padding: 10,
    borderColor: '#ddd',
    borderWidth: 1,
    borderRadius: 5,
    marginHorizontal: 20,
  },
  newsTitle: {
    fontSize: 18,
    fontWeight: 'bold',
    marginBottom: 5,
  },
  buttonContainer: {
    padding: 20,
    alignItems: 'center',
  },
  button: {
    backgroundColor: '#3F9296',
    padding: 15,
    borderRadius: 5,
    width: '100%',
    alignItems: 'center',
  },
  buttonText: {
    color: '#fff',
    fontSize: 16,
    fontWeight: 'bold',
  },
});

export default NewsScreen;
