//#define DEBUG_PRINTS

using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Unity.InteractiveTutorials;
using UnityEditor;
using UnityEngine;

namespace Unity.NAME.Editor
{
    /// <summary>
    /// Translates the strings in the same assemby where the project's PO files resides.
    /// </summary>
    [InitializeOnLoad]
    public static class Translator
    {
        static Translator()
        {
            // TODO Editor Localization bug: translation not working when InitializeOnLoad(Methods) are called
            // when the Editor is started (works when assemby reload happens after a script modification).
            // Must defer the translation calls to the first EditorApplication update.
            EditorApplication.update += DeferredTranslate;
        }

        static void DeferredTranslate()
        {
            EditorApplication.update -= DeferredTranslate;
            Translate();
        }

        [MenuItem("Tutorials/Translate Current Project")]
        public static void Translate()
        {
            Debug.Log(Localization.Tr("Show Tutorials"));

            foreach (var container in FindAssets<TutorialContainer>())
            {
                TranslateObject(container);

                foreach (var section in container.Sections)
                {
                    TranslateObject(section);
                }
            }

            foreach (var welcomePg in FindAssets<TutorialWelcomePage>())
            {
                TranslateObject(welcomePg);

                foreach (var paragraph in welcomePg.paragraphs)
                {
                    TranslateObject(paragraph);
                }
            }

            foreach (var tutorial in FindAssets<Tutorial>())
                TranslateObject(tutorial);

            foreach (var pg in FindAssets<TutorialPage>())
            {
                TranslateObject(pg);

                foreach (var paragraph in pg.paragraphs)
                {
                    TranslateObject(paragraph);
                }
            }
        }

        static void TranslateObject(object obj)
        {
            const BindingFlags bindedTypes = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;
            var localizableStringType = typeof(LocalizableString);

            obj.GetType().GetProperties(bindedTypes)
                .Where(pi => pi.PropertyType == localizableStringType)
                .ToList()
                .ForEach(pi =>
                {
                    var str = pi.GetValue(obj) as LocalizableString;
                    str.Translated = Localization.Tr(str.Untranslated);
#if DEBUG_PRINTS
                    Debug.Log($"was '{str.Untranslated}' is '{str.Translated}'");
#endif
                    pi.SetValue(obj, str);
                });

            obj.GetType().GetFields(bindedTypes)
                .Where(fi => fi.FieldType == localizableStringType)
                .ToList()
                .ForEach(fi =>
                {
                    var str = fi.GetValue(obj) as LocalizableString;
                    str.Translated = Localization.Tr(str.Untranslated);
#if DEBUG_PRINTS
                    Debug.Log($"was '{str.Untranslated}' is '{str.Translated}'");
#endif
                    fi.SetValue(obj, str);
                });
        }

        static IEnumerable<T> FindAssets<T>() where T : Object =>
            AssetDatabase.FindAssets($"t:{typeof(T).FullName}")
                .Select(AssetDatabase.GUIDToAssetPath)
                .Select(AssetDatabase.LoadAssetAtPath<T>);
    }
}
