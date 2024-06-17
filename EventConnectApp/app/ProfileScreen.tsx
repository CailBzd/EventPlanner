import React, { useState, useEffect } from 'react';
import { View, Text, StyleSheet, TouchableOpacity, Image, Button, Alert, TextInput, Modal } from 'react-native';
import * as ImagePicker from 'expo-image-picker';
import AsyncStorage from '@react-native-async-storage/async-storage';
import { User } from '../types/User';
import { userService } from '@/services/userService';
import { useTranslation } from 'react-i18next';

export default function ProfileScreen() {
  const [user, setUser] = useState<User | null>(null);
  const [userId, setUserId] = useState<string>('');
  const [token, setToken] = useState<string>('');
  const [profileImageBase64, setProfileImageBase64] = useState<string | null | undefined>(null);
  const [modalVisible, setModalVisible] = useState(false);

  const { t } = useTranslation();

  useEffect(() => {
    const loadUserData = async () => {
      const storedUserId = await AsyncStorage.getItem('userId');
      const storedToken = await AsyncStorage.getItem('userToken');
      if (storedUserId && storedToken) {
        setUserId(storedUserId);
        setToken(storedToken);
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

  const pickImage = async () => {
    const result = await ImagePicker.launchImageLibraryAsync({
      mediaTypes: ImagePicker.MediaTypeOptions.Images,
      allowsEditing: true,
      aspect: [4, 3],
      quality: 1,
      base64: true,  // Permet de récupérer l'image en base64
    });

    if (!result.canceled) {
      const base64Image = result.assets[0].base64;
      setProfileImageBase64(base64Image);
      // Vous pouvez également mettre à jour l'objet utilisateur si nécessaire
      if (user) {
        setUser({ ...user, profilePicture: base64Image });
      }
    }

    setModalVisible(false);  // Ferme la modal après sélection
  };

  const takePhoto = async () => {
    const result = await ImagePicker.launchCameraAsync({
      allowsEditing: true,
      aspect: [4, 3],
      quality: 1,
      base64: true,  // Permet de récupérer l'image en base64
    });

    if (!result.canceled) {
      const base64Image = result.assets[0].base64;
      setProfileImageBase64(base64Image);
      // Vous pouvez également mettre à jour l'objet utilisateur si nécessaire
      if (user) {
        setUser({ ...user, profilePicture: base64Image });
      }
    }

    setModalVisible(false);  // Ferme la modal après sélection
  };

  const handleSave = async () => {
    try {
      if (!user) return;

      const updateUserCommand = {
        id: userId,
        userName: user.userName,
        email: user.email,
        biography: user.biography,
        profilePicture: profileImageBase64 ? profileImageBase64 : null,
        city: user.city,
        postalCode: user.postalCode,
        country: user.country,
        dateOfBirth: user.dateOfBirth,
        phoneNumber: user.phoneNumber,
      };

      const response = await userService.updateUser(updateUserCommand, token);

      if (response.status === 204) {
        console.error(response.statusText);
        Alert.alert('Success', 'Profile updated successfully');
      } else {
        Alert.alert('Error', 'Failed to update profile');
      }
    } catch (error) {
      Alert.alert('Error', 'An error occurred while updating profile');
      console.error('Error caught in handleSave:', error);
    }
  };

  const avatarSource = user?.profilePicture
    ? { uri: `data:image/png;base64,${user.profilePicture}` }
    : require('../assets/images/logo.jpg');  // Assurez-vous d'avoir une image par défaut dans votre dossier assets

  if (!user) {
    return <Text>Loading...</Text>;
  }

  return (
    <View style={styles.container}>
      <View style={styles.avatarContainer}>
        <TouchableOpacity onPress={() => setModalVisible(true)}>
          <Image source={avatarSource} style={styles.avatar} />
        </TouchableOpacity>
      </View>
      <TextInput
        style={styles.input}
        placeholder="Name"
        value={user.userName}
        onChangeText={(text) => setUser({ ...user, userName: text })}
      />
      <TextInput
        style={styles.input}
        placeholder="Email"
        value={user.email}
        onChangeText={(text) => setUser({ ...user, email: text })}
      />
      <TextInput
        style={styles.input}
        placeholder="Biography"
        value={user.biography}
        onChangeText={(text) => setUser({ ...user, biography: text })}
        multiline
      />
      <TextInput
        style={styles.input}
        placeholder="City"
        value={user.city}
        onChangeText={(text) => setUser({ ...user, city: text })}
      />
      <TextInput
        style={styles.input}
        placeholder="Postal Code"
        value={user.postalCode}
        onChangeText={(text) => setUser({ ...user, postalCode: text })}
      />
      <TextInput
        style={styles.input}
        placeholder="Country"
        value={user.country}
        onChangeText={(text) => setUser({ ...user, country: text })}
      />
      <TextInput
        style={styles.input}
        placeholder="Phone Number"
        value={user.phoneNumber}
        onChangeText={(text) => setUser({ ...user, phoneNumber: text })}
      />

      <Modal
        animationType="slide"
        transparent={true}
        visible={modalVisible}
        onRequestClose={() => setModalVisible(false)}
      >
        <View style={styles.modalContainer}>
          <View style={styles.modalView}>
            <Text style={styles.modalText}>Select Image Source</Text>
            <TouchableOpacity onPress={pickImage} style={styles.modalButton}>
              <Text style={styles.modalButtonText}>Choose from Library</Text>
            </TouchableOpacity>
            <TouchableOpacity onPress={takePhoto} style={styles.modalButton}>
              <Text style={styles.modalButtonText}>Take Photo</Text>
            </TouchableOpacity>
            <TouchableOpacity onPress={() => setModalVisible(false)} style={styles.modalButton}>
              <Text style={styles.modalButtonText}>Cancel</Text>
            </TouchableOpacity>
          </View>
        </View>
      </Modal>

      <TouchableOpacity style={styles.button} onPress={handleSave}>
        <Text style={styles.buttonText}>{t('save')}</Text>
      </TouchableOpacity>
    </View>
  );
};

const styles = StyleSheet.create({
  container: {
    flex: 1,
    padding: 20,
    backgroundColor: '#fff',
  },
  button: {
    width: '100%',
    height: 50,
    backgroundColor: '#3F9296',
    borderRadius: 5,
    justifyContent: 'center',
    alignItems: 'center',
    marginBottom: 20,
  },
  buttonText: {
    color: '#fff',
    fontSize: 18,
    fontWeight: 'bold',
  },
  title: {
    fontSize: 24,
    fontWeight: 'bold',
    color: '#fff',
  },
  saveButton: {
    fontSize: 16,
    fontWeight: 'bold',
    color: '#fff',
  },
  input: {
    width: '100%',
    height: 50,
    borderColor: '#ddd',
    borderWidth: 1,
    borderRadius: 5,
    paddingHorizontal: 10,
    marginBottom: 15,
  },
  avatar: {
    width: 100,
    height: 100,
    borderRadius: 50,
  },

  avatarContainer: {
    flexDirection: 'row',
    justifyContent: 'center',
    marginTop: 10,
    marginBottom: 20,
  },
  iconContainer: {
    flexDirection: 'row',
    marginLeft: 20,
  },
  iconButton: {
    marginHorizontal: 10,
  },
  modalContainer: {
    flex: 1,
    justifyContent: "center",
    alignItems: "center",
    backgroundColor: "rgba(0, 0, 0, 0.5)"
  },
  modalView: {
    margin: 20,
    backgroundColor: "white",
    borderRadius: 20,
    padding: 35,
    alignItems: "center",
    shadowColor: "#000",
    shadowOffset: {
      width: 0,
      height: 2
    },
    shadowOpacity: 0.25,
    shadowRadius: 4,
    elevation: 5
  },
  modalText: {
    marginBottom: 15,
    textAlign: "center",
    fontSize: 18
  },
  modalButton: {
    borderRadius: 10,
    padding: 10,
    elevation: 2,
    marginVertical: 5
  },
  modalButtonText: {
    borderRadius: 10,
    padding: 10,
    elevation: 2,
    marginVertical: 5
  },
  buttonClose: {
    backgroundColor: "#3F9296"
  },
  icon: {
    width: 40,
    height: 40,
  },
});


