import React, { useState, useEffect } from 'react';
import { View, Text, FlatList, TouchableOpacity, Image } from 'react-native';
import AsyncStorage from '@react-native-async-storage/async-storage';
import { User } from '@/types/User';
import { userService } from '@/services/userService';
import { useTranslation } from 'react-i18next';
import { eventService } from '@/services/eventService';
import { Event } from '@/types/Event';
import dayjs from 'dayjs'; import { useNavigation } from '@react-navigation/native';
import { NativeStackNavigationProp } from '@react-navigation/native-stack';
import { NewsStyles } from '@/styles/screens/newsStyles';

type LoginScreenNavigationProp = NativeStackNavigationProp<RootStackParamList, 'LoginScreen'>;


export default function NewsScreen() {
  const { t } = useTranslation();
  const navigation = useNavigation<LoginScreenNavigationProp>();

  const [user, setUser] = useState<User | null>();
  const [events, setEvents] = useState<Event[]>([]);

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

    const loadEvents = async () => {
      try {
        const response = await eventService.getEvents();
        setEvents(response.data);
      } catch (error) {
        console.error('Error fetching events:', error);
      }
    };

    loadUserData();
    loadEvents();
  }, []);

  const handleCreateEvent = () => {
    // Logic to create an event
    console.log("Create an event");
  };

  const handleAvatarPress = () => {
    navigation.push('ProfileScreen');
  };

  const handleEventPress = (eventId) => {
    navigation.push('EventScreen', { eventId: eventId });
  };

  const avatarSource = user?.profilePicture
    ? { uri: `data:image/png;base64,${user.profilePicture}` }
    : require('../assets/images/logo.jpg');  // Make sure you have a default image in your assets folder

  return (
    <View style={NewsStyles.container}>
      <View style={NewsStyles.header}>
        <Text style={NewsStyles.title}>Journal</Text>
        <TouchableOpacity onPress={handleAvatarPress}>
          <Image
            source={avatarSource}
            style={NewsStyles.avatar}
          />
        </TouchableOpacity>
      </View>
      <FlatList
        data={events}
        keyExtractor={(item) => item.id}
        renderItem={({ item }) => (
          <TouchableOpacity onPress={() => handleEventPress(item.id)} style={NewsStyles.eventItem}>
            <Text style={NewsStyles.eventTitle}>{item.title}</Text>
            <Text>Date: {dayjs(item.startDate).format('DD/MM/YYYY')}</Text>
            {/* <Text>Participants: {item.participants}</Text> */}
            <Text>Location: {item.location}</Text>
            <Text>{item.description}</Text>
          </TouchableOpacity>
        )}
      />
      <View style={NewsStyles.buttonContainer}>
        <TouchableOpacity style={NewsStyles.button} onPress={handleCreateEvent}>
          <Text style={NewsStyles.buttonText}>{t("news:createEvent")}</Text>
        </TouchableOpacity>
      </View>
    </View>
  );
};
