import React, { useState, useEffect, useCallback } from 'react';
import { View, Text, FlatList, TouchableOpacity, Image, Alert } from 'react-native';
import AsyncStorage from '@react-native-async-storage/async-storage';
import { User } from '@/types/User';
import { userService } from '@/services/userService';
import { useTranslation } from 'react-i18next';
import { eventService } from '@/services/eventService';
import { Event } from '@/types/Event';
import dayjs from 'dayjs';
import { useNavigation, useFocusEffect } from '@react-navigation/native';
import { NativeStackNavigationProp } from '@react-navigation/native-stack';
import { NewsStyles } from '@/styles/screens/newsStyles';
import { authService } from '@/services/authService';
import Icon from 'react-native-vector-icons/AntDesign';
import LoadingScreen from '@/components/LoadingView';

type LoginScreenNavigationProp = NativeStackNavigationProp<RootStackParamList, 'LoginScreen'>;

export default function NewsScreen() {
  const { t } = useTranslation();
  const navigation = useNavigation<LoginScreenNavigationProp>();

  const [userPicture, setUserPicture] = useState<string | null>();
  const [token, setToken] = useState<string | undefined | null>();
  const [events, setEvents] = useState<Event[]>([]);
  const [loading, setLoading] = useState(true);

  const loadUserData = async () => {
    const storedUserPicture = await AsyncStorage.getItem('userPicture');
    setUserPicture(storedUserPicture);
  };

  const loadEvents = async () => {
    try {
      const _token = await AsyncStorage.getItem('userToken');
      setToken(_token);

      const response = await eventService.getEvents(_token);
      setEvents(response.data);
    } catch (error) {
      console.error('Error fetching events:', error);
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    loadUserData();
    loadEvents();
  }, []);

  useFocusEffect(
    useCallback(() => {
      loadEvents();
    }, [])
  );

  const handleCreateEvent = async () => {
    navigation.push('CreateEventScreen');
  };

  const handleAvatarPress = () => {
    navigation.push('ProfileScreen');
  };

  const handleEventPress = (eventId) => {
    navigation.push('EventScreen', { eventId: eventId });
  };

  const handleCalendarPress = () => {
    navigation.push('CalendarScreen');
  };

  const avatarSource = userPicture
    ? { uri: `data:image/png;base64,${userPicture}` }
    : require('../assets/images/logo.jpg');

  const renderEventItem = ({ item }) => {
    const imageSource = item.image
      ? { uri: `data:image/png;base64,${item.image}` }
      : require('../assets/images/logo.jpg'); // Default image if none is provided

    return (
      <TouchableOpacity onPress={() => handleEventPress(item.id)} style={NewsStyles.eventItem}>
        <View style={NewsStyles.eventTextContainer}>
          <Text style={NewsStyles.eventTitle}>{item.title}</Text>
          <Text>Date: {dayjs(item.startDate).format('DD/MM/YYYY')}</Text>
          <Text>Location: {item.location}</Text>
          <Text>{item.description}</Text>
        </View>
        <Image source={imageSource} style={NewsStyles.eventImage} />
      </TouchableOpacity>
    );
  };

  if (loading) {
    return <LoadingScreen />;
  }

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
        renderItem={renderEventItem}
      />
      <View style={NewsStyles.buttonContainer}>
        <TouchableOpacity style={NewsStyles.button} onPress={handleCreateEvent}>
          <Text style={NewsStyles.buttonText}>{t("news:createEvent")}</Text>
        </TouchableOpacity>
        <TouchableOpacity style={NewsStyles.calendarButton} onPress={handleCalendarPress}>
          <Icon name="calendar" style={NewsStyles.buttonText} />
        </TouchableOpacity>
      </View>
    </View>
  );
};
