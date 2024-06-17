import i18n from 'i18next';
import { initReactI18next } from 'react-i18next';
import * as RNLocalize from 'react-native-localize';

const resources = {
    en: {
        home: {
            signin: "Sign In",
            signup: "Sign Up",
            title: "Welcome to EventConnect",
            subtitle: "Connect, Share, and Enjoy Events",
            description: "EventConnect est une plateforme où vous pouvez :\n\n• Découvrir de nouveaux événements\n• Partager vos expériences\n• Vous connecter avec d'autres personnes partageant vos intérêts\n• Fêter un anniversaire\}• Organiser un événement d'entreprise ou de quartier\n\nQue vous recherchiez des concerts, des ateliers ou des rencontres,\nEventConnect a quelque chose pour tout le monde."
        },
        login: {
            title: "Connexion",
            username: "Username",
            password: "Password",
            signin: "Sign In",
            forgotPassword: "Forgot password ?",
            signup: "Sign Up"
        },
        register: {
            title: "Sign In",
            mail: "e-mail",
            username: "Username",
            password: "Password",
            signin: "Sign In",
            signup: "Sign Up",
            confirmPassword: "Confirm Password"
        },
        news: {
            createEvent: "Create event",
        },
        general: {
            save: "Save",
            alert: {
                title: "Error",
                emptyMessage: "Informations empty",
                wrongPassword: "Passwords do not match",
                createAccountfailed: "Failed to create account",
                invalidData: "Invalid data",
                generalError: "An error occurred",
                passwordNotSecure: "Password is not secure",
                mailInvalid: "Mail is invalid"
            }
        },
    },
    fr: {
        home: {
            signin: "Se connecter",
            signup: "S'inscrire",
            title: "Bienvenue sur EventConnect",
            subtitle: "Connect, Share, and Enjoy Events",
            description: "EventConnect est une plateforme où vous pouvez :\n\n• Découvrir de nouveaux événements\n• Partager vos expériences\n• Vous connecter avec d'autres personnes partageant vos intérêts\n• Fêter un anniversaire\}• Organiser un événement d'entreprise ou de quartier\n\nQue vous recherchiez des concerts, des ateliers ou des rencontres,\nEventConnect a quelque chose pour tout le monde."
        },
        login: {
            title: "Connexion",
            username: "Nom d'utilisateur",
            password: "Mot de passe",
            signin: "Se connecter",
            forgotPassword: "Mot de passe oublié ?",
            signup: "S'inscrire"
        },
        register: {
            title: "S'inscrire",
            mail: "e-mail",
            username: "Nom d'utilisateur",
            password: "Mot de passe",
            signin: "Se connecter",
            signup: "S'inscrire",
            confirmPassword: "Confirmer le mot de passe",
        },
        news: {
            createEvent: "Créer un événement",
        },
        forgotPassword: {
            title: "Réinitialiser le mot de passe",
            enterEmail: "Veuillez entrer votre adresse e-mail",
            emailPlaceholder: "Adresse e-mail",
            submitButton: "Réinitialiser le mot de passe",
            loading: "Chargement...",
            successMessage: "Un e-mail de réinitialisation a été envoyé",
            errorMessage: "Une erreur s'est produite, veuillez réessayer"
        },
        general: {
            save: "Enregistrer",
            alert: {
                title: "Erreur",
                emptyMessage: "Informations incomplètes",
                wrongPassword: "Les mots de passe ne correspondent pas",
                createAccountfailed: "Erreur de la création du compte",
                invalidData: "Données invalides",
                generalError: "Une erreur s'est produite",
                passwordNotSecure: "Le mot de passe ne respecte pas les prérequis ( 8 caractères, 1 minuscule, 1 majuscule, 1 chiffre et un caractères spécial)",
                mailInvalid: "le mail n'est pas valide"
            }
        },
    },
    // Ajoutez d'autres langues ici
};

i18n
    .use(initReactI18next)
    .init({
        compatibilityJSON: 'v3',
        resources,
        // lng: RNLocalize.getLocales()[0].languageCode, // Détecte la langue par défaut du téléphone
        lng: "fr",
        fallbackLng: 'fr',
        interpolation: {
            escapeValue: false,
        },
        defaultNS: "app",
    });

export default i18n;
