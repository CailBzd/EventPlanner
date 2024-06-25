import React, { useState, useEffect } from 'react';
import { View, Text, FlatList, TouchableOpacity, Image, Alert } from 'react-native';
import AsyncStorage from '@react-native-async-storage/async-storage';
import { useTranslation } from 'react-i18next';
import { eventService } from '@/services/eventService';
import { Event } from '@/types/Event';
import dayjs from 'dayjs';
import { useNavigation } from '@react-navigation/native';
import { NativeStackNavigationProp } from '@react-navigation/native-stack';
import { NewsStyles } from '@/styles/screens/newsStyles';
import EmptyScreen from './EmptyScreen';


type LoginScreenNavigationProp = NativeStackNavigationProp<RootStackParamList, 'LoginScreen'>;

export default function NewsScreen() {
  const { t } = useTranslation();
  const navigation = useNavigation<LoginScreenNavigationProp>();

  const [userPicture, setUserPicture] = useState<string | null>();
  const [token, setToken] = useState<string | undefined | null>();
  const [events, setEvents] = useState<Event[]>([]);

  useEffect(() => {
    const loadUserData = async () => {
      const storedUserPicture = await AsyncStorage.getItem('userPicture');
      setUserPicture(storedUserPicture);
    };

    const loadEvents = async () => {
      try {
        console.log("loadEvents");
        const _token = await AsyncStorage.getItem('userToken');
        setToken(_token);

        const response = await eventService.getEvents(_token);
        console.log(response.data);
        setEvents(response.data);
      } catch (error) {
        console.error('Error fetching events:', error);
      }
    };

    loadUserData();
    loadEvents();
  }, []);

  const handleCreateEvent = () => {
    navigation.navigate('CreateEventScreen');
  };

  const handleAvatarPress = () => {
    navigation.push('ProfileScreen');
  };

  const handleEventPress = (eventId) => {
    navigation.push('EventScreen', { eventId: eventId });
  };

  const avatarSource = userPicture
    ? { uri: `data:image/png;base64,${userPicture}` }
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
      {events.length === 0 ? (
        <EmptyScreen />
      ) : (
        <FlatList
          data={events}
          keyExtractor={(item) => item.id}
          renderItem={({ item }) => (
            <TouchableOpacity onPress={() => handleEventPress(item.id)} style={NewsStyles.eventItem}>
              <Text style={NewsStyles.eventTitle}>{item.title}</Text>
              <Text>Date: {dayjs(item.startDate).format('DD/MM/YYYY')}</Text>
              <Text>Location: {item.location}</Text>
              <Text>{item.description}</Text>
            </TouchableOpacity>
          )}
        />
      )}
      <View style={NewsStyles.buttonContainer}>
        <TouchableOpacity style={NewsStyles.button} onPress={handleCreateEvent}>
          <Text style={NewsStyles.buttonText}>{t("news:createEvent")}</Text>
        </TouchableOpacity>
      </View>
    </View>
  );
};
