import React, { useState, useEffect } from 'react';
import { View, Text, StyleSheet, FlatList, TouchableOpacity, Image } from 'react-native';
import AsyncStorage from '@react-native-async-storage/async-storage';
import { useNavigation } from '@react-navigation/native';
import { User } from '@/types/User';
import { userService } from '@/services/userService';
import { useTranslation } from 'react-i18next';
import { router } from 'expo-router';

const data = [
  { id: '1', title: 'Event 1', date: '2023-06-15', participants: 50, location: 'Paris', content: 'Details about event 1' },
  { id: '2', title: 'Event 2', date: '2023-07-20', participants: 75, location: 'London', content: 'Details about event 2' },
  { id: '3', title: 'Event 3', date: '2023-08-25', participants: 100, location: 'New York', content: 'Details about event 3' },
  // Add more events here
];

const NewsScreen = () => {
  const { t } = useTranslation();
  const navigation = useNavigation();

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
    // Logic to create an event
    console.log("Create an event");
  };

  const handleAvatarPress = () => {
    router.push('ProfileScreen');
  };

  const handleEventPress = (eventId) => {
    router.push({ pathname: '/EventScreen', params: { eventId } });
  };

  const avatarSource = user?.profilePicture
    ? { uri: `data:image/png;base64,${user.profilePicture}` }
    : require('../assets/images/logo.jpg');  // Make sure you have a default image in your assets folder

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
          <TouchableOpacity onPress={() => handleEventPress(item.id)} style={styles.eventItem}>
            <Text style={styles.eventTitle}>{item.title}</Text>
            <Text>Date: {item.date}</Text>
            <Text>Participants: {item.participants}</Text>
            <Text>Location: {item.location}</Text>
            <Text>{item.content}</Text>
          </TouchableOpacity>
        )}
      />
      <View style={styles.buttonContainer}>
        <TouchableOpacity style={styles.button} onPress={handleCreateEvent}>
          <Text style={styles.buttonText}>{t("news:createEvent")}</Text>
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
  eventItem: {
    marginBottom: 15,
    padding: 10,
    borderColor: '#ddd',
    borderWidth: 1,
    borderRadius: 5,
    marginHorizontal: 20,
  },
  eventTitle: {
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
