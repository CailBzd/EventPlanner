import React, { useState, useEffect } from 'react';
import { View, Text, TouchableOpacity, Image, Alert, TextInput, Modal } from 'react-native';
import * as ImagePicker from 'expo-image-picker';
import AsyncStorage from '@react-native-async-storage/async-storage';
import { User } from '../types/User';
import { userService } from '@/services/userService';
import { useTranslation } from 'react-i18next';
import { ProfileStyles } from '@/styles/screens/profileStyles';
import { authService } from '@/services/authService';
import { useNavigation } from '@react-navigation/native';
import { NativeStackNavigationProp } from '@react-navigation/native-stack';
import LoadingScreen from '@/components/LoadingView';

type LoginScreenNavigationProp = NativeStackNavigationProp<RootStackParamList, 'LoginScreen'>;

export default function ProfileScreen() {
  const [user, setUser] = useState<User | null>(null);
  const [userId, setUserId] = useState<string>('');
  const [token, setToken] = useState<string | null>();
  const [profileImageBase64, setProfileImageBase64] = useState<string | null | undefined>(null);
  const [modalVisible, setModalVisible] = useState(false);
  const navigation = useNavigation<LoginScreenNavigationProp>();


  const { t } = useTranslation();

  // useEffect(() => {
  //   const loadUserData = async () => {
  //     const userData = await AsyncStorage.getItem('user');
  //     console.log("loadUserData");
  //     console.log(userData);
  //     if (userData) {
  //       setUser(JSON.parse(userData));
  //     }
  //   };

  //   loadUserData();
  // }, []);

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

          await AsyncStorage.removeItem('userToken');
          await AsyncStorage.removeItem('userId');
          await AsyncStorage.removeItem('userPicture');

          navigation.reset({
            index: 0,
            routes: [{ name: 'HomeScreen' }],
          });
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

      const userData = await AsyncStorage.getItem('user');
      setToken(userData)
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

  const handleLogout = async () => {
    const token = await AsyncStorage.getItem('userToken');
    if (token) {
      try {
        const response = await authService.logout(token);

        if (response.status === 200) {
          await AsyncStorage.removeItem('userToken');
          await AsyncStorage.removeItem('userId');
          await AsyncStorage.removeItem('userPicture');

          navigation.reset({
            index: 0,
            routes: [{ name: 'HomeScreen' }],
          });
        } else {
          Alert.alert(t('error1'), t('logout:failed'));
        }
      } catch (error) {
        Alert.alert(t('error2'), t('logout:anErrorOccurred'));
        console.error('Error during logout:', error);
      }
    }
  };


  const avatarSource = user?.profilePicture
    ? { uri: `data:image/png;base64,${user.profilePicture}` }
    : require('../assets/images/logo.jpg');  // Assurez-vous d'avoir une image par défaut dans votre dossier assets

  if (!user) {
    return <LoadingScreen />;
  }

  return (
    <View style={ProfileStyles.container}>
      <View style={ProfileStyles.avatarContainer}>
        <TouchableOpacity onPress={() => setModalVisible(true)}>
          <Image source={avatarSource} style={ProfileStyles.avatar} />
        </TouchableOpacity>
      </View>
      <TextInput
        style={ProfileStyles.input}
        placeholder="Name"
        value={user.userName}
        onChangeText={(text) => setUser({ ...user, userName: text })}
      />
      <TextInput
        style={ProfileStyles.input}
        placeholder="Email"
        value={user.email}
        onChangeText={(text) => setUser({ ...user, email: text })}
      />
      <TextInput
        style={ProfileStyles.input}
        placeholder="Biography"
        value={user.biography}
        onChangeText={(text) => setUser({ ...user, biography: text })}
        multiline
      />
      <TextInput
        style={ProfileStyles.input}
        placeholder="City"
        value={user.city}
        onChangeText={(text) => setUser({ ...user, city: text })}
      />
      <TextInput
        style={ProfileStyles.input}
        placeholder="Postal Code"
        value={user.postalCode}
        onChangeText={(text) => setUser({ ...user, postalCode: text })}
      />
      <TextInput
        style={ProfileStyles.input}
        placeholder="Country"
        value={user.country}
        onChangeText={(text) => setUser({ ...user, country: text })}
      />
      <TextInput
        style={ProfileStyles.input}
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
        <View style={ProfileStyles.modalContainer}>
          <View style={ProfileStyles.modalView}>
            <Text style={ProfileStyles.modalText}>{t('profile:selectImage')}</Text>
            <TouchableOpacity onPress={pickImage} style={ProfileStyles.modalButton}>
              <Text style={ProfileStyles.modalButtonText}>{t('profile:fromLibrary')}</Text>
            </TouchableOpacity>
            <TouchableOpacity onPress={takePhoto} style={ProfileStyles.modalButton}>
              <Text style={ProfileStyles.modalButtonText}>{t('profile:takePhoto')}Take Photo</Text>
            </TouchableOpacity>
            <TouchableOpacity onPress={() => setModalVisible(false)} style={ProfileStyles.modalButton}>
              <Text style={ProfileStyles.modalButtonText}>{t('general:cancel')}</Text>
            </TouchableOpacity>
          </View>
        </View>
      </Modal>

      <TouchableOpacity style={ProfileStyles.button} onPress={handleSave}>
        <Text style={ProfileStyles.buttonText}>{t('general:save')}</Text>
      </TouchableOpacity>
      <TouchableOpacity onPress={handleLogout}>
        <Text style={ProfileStyles.link}>{t('profile:logout')}</Text>
      </TouchableOpacity>
    </View>
  );
};
