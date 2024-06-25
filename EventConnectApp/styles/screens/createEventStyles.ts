import { StyleSheet } from 'react-native';

export const CreateEventStyles = StyleSheet.create({
    container: {
        flex: 1,
        backgroundColor: '#fff',
    },
    header: {
        flexDirection: 'row',
        justifyContent: 'space-between',
        alignItems: 'center',
        padding: 20,
        paddingTop: 40,
        marginBottom: 10,
        backgroundColor: '#3F9296',
    },
    title: {
        fontSize: 24,
        color: '#fff',
    },
    imageContainer: {
        alignItems: 'center',  // Center horizontally
        marginBottom: 10,
    },
    presentationImage: {
        width: 100,
        height: 100,
        borderRadius: 50,
    },
    form: {
        flex: 1,
    },
    label: {
        fontSize: 16,
        marginBottom: 8,
        marginHorizontal: 20,
    },
    input: {
        marginHorizontal: 20,
        height: 40,
        borderColor: '#ddd',
        borderWidth: 1,
        borderRadius: 5,
        paddingHorizontal: 10,
        marginBottom: 16,
    },
    dateButton: {
        height: 40,
        borderColor: '#ddd',
        borderWidth: 1,
        borderRadius: 5,
        justifyContent: 'center',
        alignItems: 'center',
        marginBottom: 16,
        marginHorizontal: 20,
    },
    dateButtonText: {
        fontSize: 16,
    },
    button: {
        height: 50,
        backgroundColor: '#3F9296',
        borderRadius: 5,
        justifyContent: 'center',
        alignItems: 'center',
        marginBottom: 20,
        marginHorizontal: 20,
    },
    buttonText: {
        color: '#fff',
        fontSize: 18,
        fontWeight: 'bold',
    },
});
