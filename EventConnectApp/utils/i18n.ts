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
        profile: {
            headerTitle: "My profile",
            takePhoto: "Take Photo",
            fromLibrary: "Choose from Library",
            selectImage: "Select Image Source",
            logout: "Logout"
        },
        login: {
            headerTitle: "Login",
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
        forgotPassword: {
            title: "Forgot Password",
            email: "Email",
            send: "Send",
            emailPlaceholder: "Email",
            emailSent: "A reset link has been sent to your email.",
            failedToSendEmail: "Failed to send reset link.",
            anErrorOccurred: "An error occurred.",
            loading: "Loading...",
            submitButton: "Reset"
        },
        resetPassword: {
            title: "Reset Password",
            newPassword: "New Password",
            confirmPassword: "Confirm Password",
            reset: "Reset",
            passwordsNotMatch: "Passwords do not match.",
            passwordReset: "Password has been reset successfully.",
            failedToResetPassword: "Failed to reset password.",
            anErrorOccurred: "An error occurred.",
        },
        news: {
            createEvent: "Create event",
        },
        general: {
            back: "Back",
            save: "Save",
            cancel: "Cancel",
            success: "Success",
            error: "Error",
            ok: "OK",
            alert: {
                emptyMessage: "Informations empty",
                wrongPassword: "Passwords do not match",
                createAccountfailed: "Failed to create account",
                invalidData: "Invalid data",
                generalError: "An error occurred",
                passwordNotSecure: "Password is not secure",
                mailInvalid: "Invalid email address",
                accountCreated: "Account created successfully"
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
        profile: {
            headerTitle: "Mon profil",
            takePhoto: "Prendre une photo",
            fromLibrary: "Choisir dans la galerie",
            selectImage: "Sélectionner une image",
            logout: "Se déconnecter"
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
        forgotPassword: {
            title: "Mot de passe oublié",
            email: "E-mail",
            send: "Envoyer",
            emailPlaceholder: "E-mail",
            emailSent: "Un lien de réinitialisation a été envoyé à votre adresse e-mail.",
            failedToSendEmail: "Échec de l'envoi du lien de réinitialisation.",
            anErrorOccurred: "Une erreur est survenue.",
            loading: "Chargement...",
            submitButton: "Réinitialiser"
        },
        resetPassword: {
            title: "Réinitialiser le mot de passe",
            newPassword: "Nouveau mot de passe",
            confirmPassword: "Confirmer le mot de passe",
            reset: "Réinitialiser",
            passwordsNotMatch: "Les mots de passe ne correspondent pas.",
            passwordReset: "Le mot de passe a été réinitialisé avec succès.",
            failedToResetPassword: "Échec de la réinitialisation du mot de passe.",
            anErrorOccurred: "Une erreur est survenue.",
        },
        news: {
            createEvent: "Créer un événement",
        },

        general: {
            back: "Retour",
            save: "Enregistrer",
            cancel: "Annuler",
            success: "Succès",
            error: "Erreur",
            ok: "OK",
            alert: {
                emptyMessage: "Informations incomplètes",
                wrongPassword: "Les mots de passe ne correspondent pas",
                createAccountfailed: "Erreur de la création du compte",
                invalidData: "Données invalides",
                generalError: "Une erreur s'est produite",
                passwordNotSecure: "Le mot de passe ne respecte pas les prérequis ( 8 caractères, 1 minuscule, 1 majuscule, 1 chiffre et un caractères spécial)",
                mailInvalid: "Le mail n'est pas valide",
                accountCreated: "Compte créé avec succès"
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
