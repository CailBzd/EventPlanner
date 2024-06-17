import React, { useEffect, useState } from 'react';
import { View, Text, StyleSheet, TouchableOpacity } from 'react-native';
import { useRouter, useSearchParams } from 'expo-router';

const data = [
  { id: '1', title: 'Event 1', date: '2023-06-15', participants: 50, location: 'Paris', content: 'Details about event 1' },
  { id: '2', title: 'Event 2', date: '2023-07-20', participants: 75, location: 'London', content: 'Details about event 2' },
  { id: '3', title: 'Event 3', date: '2023-08-25', participants: 100, location: 'New York', content: 'Details about event 3' },
  // Add more events here
];

const EventDetailScreen = () => {
  const router = useRouter();
  const { eventId } = useSearchParams();
  
  const [event, setEvent] = useState(null);

  useEffect(() => {
    const eventDetail = data.find(item => item.id === eventId);
    setEvent(eventDetail);
  }, [eventId]);

  if (!event) {
    return (
      <View style={styles.container}>
        <Text>Loading...</Text>
      </View>
    );
  }

  return (
    <View style={styles.container}>
      <Text style={styles.title}>{event.title}</Text>
      <Text style={styles.details}>Date: {event.date}</Text>
      <Text style={styles.details}>Participants: {event.participants}</Text>
      <Text style={styles.details}>Location: {event.location}</Text>
      <Text style={styles.content}>{event.content}</Text>
      
      <View style={styles.buttonContainer}>
        <TouchableOpacity style={styles.button} onPress={() => router.back()}>
          <Text style={styles.buttonText}>Back</Text>
        </TouchableOpacity>
      </View>
    </View>
  );
};

const styles = StyleSheet.create({
  container: {
    flex: 1,
    padding: 20,
    backgroundColor: '#fff',
  },
  title: {
    fontSize: 24,
    fontWeight: 'bold',
    marginBottom: 15,
  },
  details: {
    fontSize: 18,
    marginBottom: 10,
  },
  content: {
    fontSize: 16,
    marginBottom: 20,
  },
  buttonContainer: {
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

export default EventDetailScreen;
