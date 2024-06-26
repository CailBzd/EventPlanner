import React, { useState, useEffect } from 'react';
import { View, Text } from 'react-native';
import { Calendar } from 'react-native-calendars';  // Vous aurez peut-être besoin d'installer ce package avec `npm install react-native-calendars`
import { eventService } from '@/services/eventService';
import AsyncStorage from '@react-native-async-storage/async-storage';
import dayjs from 'dayjs';
import { useTranslation } from 'react-i18next';
import { CalendarStyles } from '@/styles/screens/calendarStyles';
import { NativeStackNavigationProp } from '@react-navigation/native-stack';
import { Event } from '@/types/Event';
import LoadingScreen from '@/components/LoadingView';

type CreateEventScreenNavigationProp = NativeStackNavigationProp<RootStackParamList, 'CalendarScreen'>;

export default function CalendarScreen() {
    const [events, setEvents] = useState<Event[] | undefined>();
    const [loading, setLoading] = useState(true);
    const { t } = useTranslation();

    useEffect(() => {
        const loadEvents = async () => {
            try {
                const token = await AsyncStorage.getItem('userToken');
                if (!token) throw new Error('No token found');

                const response = await eventService.getEvents(token);
                setEvents(response.data);
            } catch (error) {
                console.error('Error fetching events:', error);
            } finally {
                setLoading(false);
            }
        };

        loadEvents();
    }, []);

    const markedDates = events?.reduce((acc, event: Event) => {
        const date = dayjs(event.startDate).format('YYYY-MM-DD');
        acc[date] = { marked: true };
        return acc;
    }, {});

    if (loading) {
        return <LoadingScreen />;
    }

    return (
        <View style={CalendarStyles.container}>
            <Calendar
                markedDates={markedDates}
                onDayPress={(day) => {
                    // Action when a date is pressed, like showing events on that day
                    console.log(day);
                }}
            />
        </View>
    );
}
