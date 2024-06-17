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
            description: "EventConnect est une plateforme où vous pouvez :\n\n• Découvrir de nouveaux événements\n• Partager vos expériences\n• Vous connecter avec d'autres personnes partageant vos intérêts\n• Fêter un anniversaire\}• Organiser un événement d'entreprise ou de quartier\n\nQue vous recherchiez des concerts, des ateliers ou des rencontres,EventConnect a quelque chose pour tout le monde."
        },
        login: {
            title: "Connexion",
            username: "Username",
            password: "Password",
            signin: "Sign In",
            forgotPassword: "Forgot password ?",
            signup: "Sign Up"
        },
        save: "Save",
    },
    fr: {
        home: {
            signin: "Se connecter",
            signup: "S'inscrire",
            title: "Bienvenue sur EventConnect",
            subtitle: "Connect, Share, and Enjoy Events",
            description: "EventConnect est une plateforme où vous pouvez :\n\n• Découvrir de nouveaux événements\n• Partager vos expériences\n• Vous connecter avec d'autres personnes partageant vos intérêts\n• Fêter un anniversaire\}• Organiser un événement d'entreprise ou de quartier\n\nQue vous recherchiez des concerts, des ateliers ou des rencontres,EventConnect a quelque chose pour tout le monde."
        },
        login: {
            title: "Connexion",
            username: "Nom d'utilisateur",
            password: "Mot de passe",
            signin: "Se connecter",
            forgotPassword: "Mot de passe oublié ?",
            signup: "S'inscrire"
        },
        save: "Sauvegarder",
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
        debug: true,
    });

export default i18n;
