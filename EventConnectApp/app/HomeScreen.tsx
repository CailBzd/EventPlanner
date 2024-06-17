import React from 'react';
import { StyleSheet, Text, View, Image, TouchableOpacity, ScrollView } from 'react-native';
import { useTranslation } from 'react-i18next';

export default function HomeScreen({ navigation }) {
  
  const { t } = useTranslation();

  return (
    <View style={styles.container}>
      <View style={styles.header}>
      </View>

      <ScrollView contentContainerStyle={styles.content}>

        <View style={styles.presentationContainer}>
          <Text style={styles.title}>{t("home:title")}</Text>
          <Text style={styles.subtitle}>{t("home:subtitle")}</Text>
          <Image source={require('@/assets/images/logo.jpg')} style={styles.presentationImage} />
          <Text style={styles.description}>
          {t("home:description")}
          </Text>


        </View>
      </ScrollView>

      <View style={styles.footer}>
        <TouchableOpacity style={styles.footerButton} onPress={() => navigation.navigate('LoginScreen')}>
          <Text style={styles.footerButtonText}>{t("home:signin")}</Text>
        </TouchableOpacity>
        <TouchableOpacity style={styles.footerButton} onPress={() => navigation.navigate('RegisterScreen')}>
          <Text style={styles.footerButtonText}>{t("home:signup")}</Text>
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
