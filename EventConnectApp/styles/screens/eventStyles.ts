import { StyleSheet } from 'react-native';
import { Colors } from './globalStyles';

export const EventStyles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: Colors.light.background,
  },
  title: {
    fontSize: 24,
    fontWeight: 'bold',
    marginBottom: 15,
    color: Colors.light.text,
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
  header: {
    flexDirection: 'row',
    justifyContent: 'space-between',
    alignItems: 'center',
    padding: 20,
    paddingTop: 40,
    marginBottom: 10,
    backgroundColor: Colors.primaryColor,
  },
  details: {
    fontSize: 18,
    margin: 10,
  },
  fieldContainer: {
    flexDirection: 'row',
    justifyContent: 'space-between',
    alignItems: 'center',
    margin: 10,
  },
  titleInput: {
    fontSize: 24,
    fontWeight: 'bold',
    marginBottom: 15,
    color: Colors.light.text,
    borderBottomWidth: 1,
    borderBottomColor: '#ddd',
  },
  content: {
    fontSize: 16,
    marginBottom: 20,
  },
  buttonContainer: {
    alignItems: 'center',
    padding: 20,
    marginVertical: 10
  },
  button: {
    backgroundColor: Colors.primaryColor,
    padding: 15,
    borderRadius: 5,
    width: '100%',
    alignItems: 'center',
    marginVertical: 5,
  },
  input: {
    height: 40,
    borderColor: '#ddd',
    borderWidth: 1,
    borderRadius: 5,
    paddingHorizontal: 10,
    marginBottom: 16,
    marginHorizontal: 20,
  },
  buttonText: {
    color: Colors.light.background,
    fontSize: 16,
    fontWeight: 'bold',
  },
  deleteButton: {
    backgroundColor: 'red',
    padding: 15,
    borderRadius: 5,
    width: '50%',
    alignItems: 'center',
  },
  image: {
    width: '100%',
    height: 200,
    borderRadius: 10,
    marginBottom: 20,
  },
  iconContainer: {
    flexDirection: 'row',
    justifyContent: 'space-around',
    marginTop: 20,
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
});
