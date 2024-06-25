import React, { useEffect, useState } from 'react';
import { View, Text, TouchableOpacity } from 'react-native';
import { useRouter, useLocalSearchParams } from 'expo-router';
import { Event } from '@/types/Event';
import dayjs from "dayjs";
import { eventService } from '@/services/eventService';
import { EventStyles } from '@/styles/screens/eventStyles';
import AsyncStorage from '@react-native-async-storage/async-storage';

export default function EventDetailScreen() {
  const router = useRouter();
  const { eventId } = useLocalSearchParams();

  const [event, setEvent] = useState<Event | undefined>();
  const [token, setToken] = useState<string | null | undefined>();

  useEffect(() => {
    const loadEvent = async () => {
      if (eventId) {
        const _token = await AsyncStorage.getItem('userToken');
        setToken(_token);

        eventService.getEventById(eventId as string, token as string)
          .then(response => {
            const eventDetail = response.data;
            setEvent(eventDetail);
          })
          .catch(error => {
            console.error('Error fetching event:', error);
          });
      }

    };

    loadEvent();
  }, []);

  if (!event) {
    return (
      <View style={EventStyles.container}>
        <Text>Loading...</Text>
      </View>
    );
  }

  return (
    <View style={EventStyles.container}>
      <Text style={EventStyles.title}>{event.title}</Text>
      <Text style={EventStyles.details}>Date: {dayjs(event.startDate).format('DD/MM/YYYY')}</Text>
      {/* <Text style={EventStyles.details}>Participants: {event.participants.length}</Text> */}
      <Text style={EventStyles.details}>Location: {event.location}</Text>
      <Text style={EventStyles.content}>{event.description}</Text>

      <View style={EventStyles.buttonContainer}>
        <TouchableOpacity style={EventStyles.button} onPress={() => router.back()}>
          <Text style={EventStyles.buttonText}>Back</Text>
        </TouchableOpacity>
      </View>
    </View>
  );
};
