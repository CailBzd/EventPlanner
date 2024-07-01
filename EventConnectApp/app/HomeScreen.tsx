import React from 'react';
import { TouchableOpacity, Image } from 'react-native';
import { useTranslation } from 'react-i18next';
import { HomeStyles } from '@/styles/screens/homeStyles';
import { View, Text, ScrollView } from '@/components/ui'

export default function HomeScreen({ navigation }) {

  const { t } = useTranslation();

  return (
    <View style={HomeStyles.container}>
      <View style={HomeStyles.header}>
      </View>

      <ScrollView contentContainerStyle={HomeStyles.content}>

        <View style={HomeStyles.presentationContainer}>
          <Text style={HomeStyles.title}>{t("home:title")}</Text>
          <Text style={HomeStyles.subtitle}>{t("home:subtitle")}</Text>
          <Image
            style={HomeStyles.presentationImage}
            source={require("../assets/images/logo.jpg")}
            alt="image"
          />
          <Text style={HomeStyles.description}>
            {t("home:description")}
          </Text>


        </View>
      </ScrollView>

      <View style={HomeStyles.footer}>
        <TouchableOpacity style={HomeStyles.footerButton} onPress={() => navigation.navigate('LoginScreen')}>
          <Text style={HomeStyles.footerButtonText}>{t("home:signin")}</Text>
        </TouchableOpacity>
        <TouchableOpacity style={HomeStyles.footerButton} onPress={() => navigation.navigate('RegisterScreen')}>
          <Text style={HomeStyles.footerButtonText}>{t("home:signup")}</Text>
        </TouchableOpacity>
      </View>
    </View>
  );
}

