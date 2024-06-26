import { StyleSheet } from 'react-native';
import { Colors } from './globalStyles';

export const NewsStyles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: '#fff',
  },
  header: {
    flexDirection: 'row',
    justifyContent: 'space-between',
    alignItems: 'center',
    padding: 20,
    backgroundColor: '#3F9296',
  },
  title: {
    fontSize: 24,
    color: '#fff',
  },
  avatar: {
    width: 40,
    height: 40,
    borderRadius: 20,
  },
  eventItem: {
    flexDirection: 'row',
    justifyContent: 'space-between',
    padding: 20,
    borderBottomWidth: 1,
    borderBottomColor: '#ddd',
  },
  eventTextContainer: {
    flex: 1,
    paddingRight: 10,
  },
  eventTitle: {
    fontSize: 18,
    fontWeight: 'bold',
    marginBottom: 5,
  },
  eventImage: {
    width: 60,
    height: 60,
    borderRadius: 5,
  },
  buttonContainer: {
    flexDirection: 'row',
    justifyContent: 'space-between',
    padding: 20,
    alignItems: 'center',
  },
  button: {
    backgroundColor: '#3F9296',
    padding: 15,
    borderRadius: 5,
    alignItems: 'center',
    width: '65%',
  },
  buttonText: {
    color: '#fff',
    fontSize: 18,
    fontWeight: 'bold',
  },
  calendarButton: {
    backgroundColor: '#3F9296',
    padding: 15,
    borderRadius: 5,
    alignItems: 'center',
    width: '30%',
  },
});
