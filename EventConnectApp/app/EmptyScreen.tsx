import React from 'react';
import { View, Text, StyleSheet } from 'react-native';
import { useTranslation } from 'react-i18next';
import Icon from 'react-native-vector-icons/FontAwesome';

export default function EmptyScreen() {
  const { t } = useTranslation();

  return (
    <View style={styles.container}>
      <Icon name="calendar" size={100} color="#888" style={styles.icon} />
      <Text style={styles.text}>{t("news:noEvents")}</Text>
    </View>
  );
}

const styles = StyleSheet.create({
  container: {
    flex: 1,
    justifyContent: 'center',
    alignItems: 'center',
  },
  icon: {
    marginBottom: 20,
  },
  text: {
    fontSize: 18,
    color: '#888',
    textAlign: 'center',
  },
});
