import React, { useEffect, useState } from 'react';
import { View, Text, TouchableOpacity, Alert, Image, Modal, TextInput, ScrollView } from 'react-native';
import { useRouter, useLocalSearchParams } from 'expo-router';
import { Event } from '@/types/Event';
import dayjs from 'dayjs';
import { eventService } from '@/services/eventService';
import { EventStyles } from '@/styles/screens/eventStyles';
import AsyncStorage from '@react-native-async-storage/async-storage';
import * as ImagePicker from 'expo-image-picker';
import Icon from 'react-native-vector-icons/AntDesign';
import LoadingScreen from '@/components/LoadingView';
import { useTranslation } from 'react-i18next';
import { Colors } from 'react-native/Libraries/NewAppScreen';

export default function EventScreen() {
  const router = useRouter();
  const { eventId } = useLocalSearchParams();
  const [modalVisible, setModalVisible] = useState(false);
  const [event, setEvent] = useState<Event | undefined>();
  const [token, setToken] = useState<string | null | undefined>();
  const [imageBase64, setImageBase64] = useState<string | null | undefined>(null);
  const [editingField, setEditingField] = useState<string | null>(null);
  const { t } = useTranslation();

  useEffect(() => {
    const loadEvent = async () => {
      if (eventId) {
        console.log('loadEvent');
        console.log(eventId);
        const _token = await AsyncStorage.getItem('userToken');
        setToken(_token);

        eventService.getEventById(eventId as string, _token as string)
          .then((response) => {
            const eventDetail = response.data;
            console.log(eventDetail);
            setEvent(eventDetail);
          })
          .catch((error) => {
            console.error('Error fetching event:', error);
          });
      }
    };

    loadEvent();
  }, [eventId]);

  const handleDeleteEvent = () => {
    Alert.alert(
      'Confirmation',
      'Are you sure you want to delete this event?',
      [
        { text: 'Cancel', style: 'cancel' },
        {
          text: 'Delete', style: 'destructive', onPress: async () => {
            if (eventId && token) {
              try {
                await eventService.deleteEvent(eventId as string, token as string);
                Alert.alert('Success', 'Event deleted successfully');
                router.back();
              } catch (error) {
                console.error('Error deleting event:', error);
                Alert.alert('Error', 'Failed to delete event');
              }
            }
          },
        },
      ],
      { cancelable: true }
    );
  };

  const pickImage = async () => {
    const result = await ImagePicker.launchImageLibraryAsync({
      mediaTypes: ImagePicker.MediaTypeOptions.Images,
      allowsEditing: true,
      aspect: [4, 3],
      quality: 1,
      base64: true,
    });

    if (!result.canceled) {
      const base64Image = result.assets[0].base64;
      setImageBase64(base64Image);
      if (event) {
        setEvent({ ...event, image: base64Image });
      }
    }

    setModalVisible(false);
  };

  const takePhoto = async () => {
    const result = await ImagePicker.launchCameraAsync({
      allowsEditing: true,
      aspect: [4, 3],
      quality: 1,
      base64: true,
    });

    if (!result.canceled) {
      const base64Image = result.assets[0].base64;
      setImageBase64(base64Image);
      if (event) {
        setEvent({ ...event, image: base64Image });
      }
    }

    setModalVisible(false);
  };

  const handleSaveChanges = async () => {
    if (eventId && token && event) {
      try {
        const updatedEvent = {
          ...event,
          image: imageBase64,
        };
        await eventService.updateEvent(eventId as string, updatedEvent, token as string);
        Alert.alert('Success', 'Event updated successfully');
        router.back();
      } catch (error) {
        console.error('Error updating event:', error);
        Alert.alert('Error', 'Failed to update event');
      }
    }
  };

  const handleFieldChange = (field: string, value: string) => {
    if (event) {
      setEvent({ ...event, [field]: value });
    }
  };

  if (!event) {
    return (
      <View style={EventStyles.container}>
        <LoadingScreen />
      </View>
    );
  }

  const imageSource = event?.image
    ? { uri: `data:image/png;base64,${event.image}` }
    : require('../assets/images/logo.jpg');

  return (
    <View style={EventStyles.container}>
      <ScrollView>
        <View style={EventStyles.header}>
          {editingField === 'title' ? (
            <TextInput
              style={EventStyles.titleInput}
              value={event.title}
              onChangeText={(text) => handleFieldChange('title', text)}
              onBlur={() => setEditingField(null)}
            />
          ) : (
            <View style={EventStyles.fieldContainer}>
              <Text style={EventStyles.title}>{event.title}</Text>
              <TouchableOpacity onPress={() => setEditingField('title')}>
                <Icon name="edit" size={20} color={Colors.primaryColor} />
              </TouchableOpacity>
            </View>
          )}
        </View>

        <View style={EventStyles.avatarContainer}>
          <TouchableOpacity onPress={() => setModalVisible(true)}>
            <Image source={imageSource} style={EventStyles.avatar} />
          </TouchableOpacity>
        </View>

        <Modal
          animationType="slide"
          transparent={true}
          visible={modalVisible}
          onRequestClose={() => setModalVisible(false)}
        >
          <View style={EventStyles.modalContainer}>
            <View style={EventStyles.modalView}>
              <Text style={EventStyles.modalText}>{t('event:selectImage')}</Text>
              <TouchableOpacity onPress={pickImage} style={EventStyles.modalButton}>
                <Text style={EventStyles.modalButtonText}>{t('event:fromLibrary')}</Text>
              </TouchableOpacity>
              <TouchableOpacity onPress={takePhoto} style={EventStyles.modalButton}>
                <Text style={EventStyles.modalButtonText}>{t('event:takePhoto')}</Text>
              </TouchableOpacity>
              <TouchableOpacity onPress={() => setModalVisible(false)} style={EventStyles.modalButton}>
                <Text style={EventStyles.modalButtonText}>{t('general:cancel')}</Text>
              </TouchableOpacity>
            </View>
          </View>
        </Modal>

        <View>
          {editingField === 'location' ? (
            <TextInput
              style={EventStyles.input}
              value={event.location}
              onChangeText={(text) => handleFieldChange('location', text)}
              onBlur={() => setEditingField(null)}
            />
          ) : (
            <View style={EventStyles.fieldContainer}>
              <Text style={EventStyles.details}>{event.location}</Text>
              <TouchableOpacity onPress={() => setEditingField('location')}>
                <Icon name="edit" size={20} color={Colors.primaryColor} />
              </TouchableOpacity>
            </View>
          )}

          {editingField === 'description' ? (
            <TextInput
              style={EventStyles.input}
              value={event.description}
              onChangeText={(text) => handleFieldChange('description', text)}
              onBlur={() => setEditingField(null)}
              multiline
            />
          ) : (
            <View style={EventStyles.fieldContainer}>
              <Text style={EventStyles.details}>{event.description}</Text>
              <TouchableOpacity onPress={() => setEditingField('description')}>
                <Icon name="edit" size={20} color={Colors.primaryColor} />
              </TouchableOpacity>
            </View>
          )}

          <Text style={EventStyles.details}>{t('event:startDate')}: {dayjs(event.startDate).format('DD/MM/YYYY')}</Text>
          <Text style={EventStyles.details}>{t('event:endDate')}: {dayjs(event.endDate).format('DD/MM/YYYY')}</Text>
        </View>
      </ScrollView>

      <View style={EventStyles.buttonContainer}>
        <TouchableOpacity style={EventStyles.button} onPress={() => router.back()}>
          <Text style={EventStyles.buttonText}>{t('general:back')}</Text>
        </TouchableOpacity>
        <TouchableOpacity style={EventStyles.button} onPress={handleSaveChanges}>
          <Text style={EventStyles.buttonText}>{t('general:save')}</Text>
        </TouchableOpacity>
        <TouchableOpacity style={EventStyles.deleteButton} onPress={handleDeleteEvent}>
          <Text style={EventStyles.buttonText}>{t('general:delete')}</Text>
        </TouchableOpacity>
      </View>
    </View>
  );
}
