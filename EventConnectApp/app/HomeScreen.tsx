import React from 'react';
import { StyleSheet, Text, View, Image, TouchableOpacity, ScrollView } from 'react-native';

export default function HomeScreen({ navigation }) {
  return (
    <View style={styles.container}>
      <View style={styles.header}>
      </View>

      <ScrollView contentContainerStyle={styles.content}>

        <View style={styles.presentationContainer}>
          <Text style={styles.title}>Bienvenue sur EventConnect</Text>
          <Text style={styles.subtitle}>Connect, Share, and Enjoy Events</Text>
          <Image source={require('@/assets/images/logo.jpg')} style={styles.presentationImage} />
          <Text style={styles.description}>
            EventConnect est une plateforme où vous pouvez :
            {"\n\n"}• Découvrir de nouveaux événements
            {"\n"}• Partager vos expériences
            {"\n"}• Vous connecter avec d'autres personnes partageant vos intérêts
            {"\n"}• Fêter un anniversaire
            {"\n"}• Organiser un événement d'entreprise ou de quartier
            {"\n"}Que vous recherchiez des concerts, des ateliers ou des rencontres,
            {"\n\n"}EventConnect a quelque chose pour tout le monde.
          </Text>


        </View>
      </ScrollView>

      <View style={styles.footer}>
        <TouchableOpacity style={styles.footerButton} onPress={() => navigation.navigate('LoginScreen')}>
          <Text style={styles.footerButtonText}>Login</Text>
        </TouchableOpacity>
        <TouchableOpacity style={styles.footerButton} onPress={() => navigation.navigate('RegisterScreen')}>
          <Text style={styles.footerButtonText}>Sign Up</Text>
        </TouchableOpacity>
      </View>
    </View>
  );
}

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: '#fff',
  },
  content: {
    flexGrow: 1,
    padding: 20,
  },
  presentationContainer: {
    alignItems: 'center',
  },
  title: {
    fontSize: 28,
    fontWeight: 'bold',
    marginBottom: 10,
  },
  subtitle: {
    fontSize: 18,
    color: '#606770',
    marginBottom: 20,
  },
  presentationImage: {
    width: '100%',
    height: 300,
    borderRadius: 10,
    marginBottom: 20,
  },
  description: {
    fontSize: 16,
    color: '#606770',
    textAlign: 'center',
    marginBottom: 20,
  },
  getStartedButton: {
    paddingVertical: 10,
    paddingHorizontal: 20,
    backgroundColor: '#3F9296',
    borderRadius: 5,
  },
  getStartedButtonText: {
    color: '#fff',
    fontSize: 16,
    fontWeight: 'bold',
  },
  footer: {
    flexDirection: 'row',
    justifyContent: 'space-around',
    paddingVertical: 10,
    backgroundColor: '#3F9296',
  },
  footerButton: {
    paddingVertical: 10,
    paddingHorizontal: 20,
    backgroundColor: '#fff',
    borderRadius: 5,
  },
  footerButtonText: {
    color: '#3F9296',
    fontSize: 16,
    fontWeight: 'bold',
  },
  header: {
    height: 60,
    backgroundColor: '#3F9296',
    flexDirection: 'row',
    justifyContent: 'space-between',
    alignItems: 'center',
    paddingHorizontal: 10,
  },
});
