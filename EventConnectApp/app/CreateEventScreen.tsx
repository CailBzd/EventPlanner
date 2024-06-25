import React, { useState } from 'react';
import { View, Text, TextInput, TouchableOpacity, Alert, Image, ScrollView } from 'react-native';
import { useTranslation } from 'react-i18next';
import { useNavigation } from '@react-navigation/native';
import { NativeStackNavigationProp } from '@react-navigation/native-stack';
import DateTimePicker from '@react-native-community/datetimepicker';
import AsyncStorage from '@react-native-async-storage/async-storage';
import { eventService } from '@/services/eventService';
import dayjs from 'dayjs';
import { CreateEventStyles } from '@/styles/screens/createEventStyles';

type CreateEventScreenNavigationProp = NativeStackNavigationProp<RootStackParamList, 'CreateEventScreen'>;

export default function CreateEventScreen() {
    const { t } = useTranslation();
    const navigation = useNavigation<CreateEventScreenNavigationProp>();

    const [title, setTitle] = useState('');
    const [description, setDescription] = useState('');
    const [location, setLocation] = useState('');
    const [startDate, setStartDate] = useState(new Date());
    const [endDate, setEndDate] = useState(new Date());
    const [showStartPicker, setShowStartPicker] = useState(false);
    const [showEndPicker, setShowEndPicker] = useState(false);

    const handleCreateEvent = async () => {
        try {
            const token = await AsyncStorage.getItem('userToken');
            if (!token) throw new Error('No token found');

            const newEvent = {
                title,
                description,
                location,
                startDate,
                endDate,
            };

            const response = await eventService.createEvent(newEvent, token);

            if (response.status === 201) {
                Alert.alert(t('success'), t('event:createdSuccessfully'));
                navigation.goBack();
            } else {
                Alert.alert(t('error'), t('event:createFailed'));
            }
        } catch (error) {
            console.error('Error creating event:', error);
            Alert.alert(t('error'), t('event:createFailed'));
        }
    };

    return (
        <ScrollView contentContainerStyle={CreateEventStyles.container}>
            <View style={CreateEventStyles.header}>
                <Text style={CreateEventStyles.title}>{t('event:createEvent')}</Text>
            </View>

            <View style={CreateEventStyles.imageContainer}>
                <Image source={require('@/assets/images/logo.jpg')} style={CreateEventStyles.presentationImage} />
            </View>

            <View style={CreateEventStyles.form}>
                <Text style={CreateEventStyles.label}>{t('event:title')}</Text>
                <TextInput
                    style={CreateEventStyles.input}
                    value={title}
                    onChangeText={setTitle}
                />

                <Text style={CreateEventStyles.label}>{t('event:description')}</Text>
                <TextInput
                    style={CreateEventStyles.input}
                    value={description}
                    onChangeText={setDescription}
                    multiline
                    numberOfLines={4}
                />

                <Text style={CreateEventStyles.label}>{t('event:location')}</Text>
                <TextInput
                    style={CreateEventStyles.input}
                    value={location}
                    onChangeText={setLocation}
                />

                <Text style={CreateEventStyles.label}>{t('event:startDate')}</Text>
                <TouchableOpacity style={CreateEventStyles.dateButton} onPress={() => setShowStartPicker(true)}>
                    <Text style={CreateEventStyles.dateButtonText}>{dayjs(startDate).format('DD/MM/YYYY')}</Text>
                </TouchableOpacity>
                {showStartPicker && (
                    <DateTimePicker
                        value={startDate}
                        mode="date"
                        display="default"
                        onChange={(event, date) => {
                            setShowStartPicker(false);
                            if (date) setStartDate(date);
                        }}
                    />
                )}

                <Text style={CreateEventStyles.label}>{t('event:endDate')}</Text>
                <TouchableOpacity style={CreateEventStyles.dateButton} onPress={() => setShowEndPicker(true)}>
                    <Text style={CreateEventStyles.dateButtonText}>{dayjs(endDate).format('DD/MM/YYYY')}</Text>
                </TouchableOpacity>
                {showEndPicker && (
                    <DateTimePicker
                        value={endDate}
                        mode="date"
                        display="default"
                        onChange={(event, date) => {
                            setShowEndPicker(false);
                            if (date) setEndDate(date);
                        }}
                    />
                )}

                <TouchableOpacity style={CreateEventStyles.button} onPress={handleCreateEvent}>
                    <Text style={CreateEventStyles.buttonText}>{t('event:createEvent')}</Text>
                </TouchableOpacity>
            </View>
        </ScrollView>
    );
}
