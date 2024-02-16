import i18n from "i18next";
import { initReactI18next } from "react-i18next";
import LanguageDetector from "i18next-browser-languagedetector";
import en from "./en.json";

i18n
  .use(LanguageDetector)
  .use(initReactI18next)
  .init({
    fallbackLng: "en",
    debug: false,
    detection: {
      order: ["queryString", "cookie"],
      cache: ["cookie"],
    },
    resources: { en },
    interpolation: {
      escapeValue: false,
    },
  });

export default i18n;

// for change lng
// const { t, i18n } = useTranslation();

// const changeLanguage = (language: string) => {
//   i18n.changeLanguage(language);
// };
