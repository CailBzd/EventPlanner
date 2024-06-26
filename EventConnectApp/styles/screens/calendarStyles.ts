import { StyleSheet } from 'react-native';
import { Colors } from './globalStyles';

export const CalendarStyles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: Colors.light.background,
    padding: 10,
  },
  calendar: {
    borderRadius: 10,
    overflow: 'hidden',
  },
  header: {
    flexDirection: 'row',
    justifyContent: 'space-between',
    alignItems: 'center',
    padding: 20,
    backgroundColor: Colors.primaryColor,
  },
  title: {
    fontSize: 24,
    color: '#fff',
  },
});
