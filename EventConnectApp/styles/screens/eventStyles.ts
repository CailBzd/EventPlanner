import { StyleSheet } from 'react-native';
import { Colors } from './globalStyles';

export const EventStyles = StyleSheet.create({
  container: {
    flex: 1,
    padding: 20,
    backgroundColor: Colors.light.background,
  },
  title: {
    fontSize: 24,
    fontWeight: 'bold',
    marginBottom: 15,
    color: Colors.primaryColor,
  },
  details: {
    fontSize: 18,
    marginBottom: 10,
    color: Colors.light.text,
  },
  content: {
    fontSize: 16,
    marginBottom: 20,
    color: Colors.light.text,
  },
  buttonContainer: {
    alignItems: 'center',
  },
  button: {
    backgroundColor: Colors.primaryColor,
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
