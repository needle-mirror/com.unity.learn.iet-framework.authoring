using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

using static Unity.InteractiveTutorials.POFileUtils;

namespace Unity.InteractiveTutorials.Authoring.Editor
{
    static class InternalMenuItem
    {
        [MenuItem(TutorialWindowMenuItem.MenuPath + "Restart Editor")]
        static void RestartEditor() => UserStartupCode.RestartEditor();

        // TODO not working as intented, doesn't show the Readme. Fix.
        //[MenuItem(TutorialWindowMenuItem.MenuPath + TutorialWindowMenuItem.Item + " (No Layout Change)")]
        static void OpenTutorialWindow()
        {
            TutorialWindow.CreateWindow();
        }

        [MenuItem(TutorialWindowMenuItem.MenuPath + "Extract Localizable Strings for the Project (Internal)...")]
        static void ExtractLocalizableStrings()
        {
            string poFolderPath = EditorUtility.OpenFolderPanel(
                Localization.Tr("Choose Folder for the Translation Files"),
                Application.dataPath,
                string.Empty
            );
            if (poFolderPath.IsNullOrEmpty())
                return;

            if (SupportedLanguages.Values.Any(code => File.Exists($"{poFolderPath}/{code}.po")))
            {
                var title = Localization.Tr("Localization");
                var msg = Localization.Tr("The folder contains translation files already, they will be overwritten.");
                var ok = Localization.Tr("Continue");
                var cancel = Localization.Tr("Cancel");
                if (!EditorUtility.DisplayDialog(title, msg, ok, cancel))
                    return;
            }

            var entries = new List<POEntry>();

            foreach (var container in FindAssets<TutorialContainer>())
            {
                entries.AddRange(ExtractFieldsAndPropertiesForLocalization(container));
                foreach (var section in container.Sections)
                {
                    entries.AddRange(ExtractFieldsAndPropertiesForLocalization(section));
                }
            }

            foreach (var welcomePg in FindAssets<TutorialWelcomePage>())
            {
                entries.AddRange(ExtractFieldsAndPropertiesForLocalization(welcomePg));

                foreach (var button in welcomePg.Buttons)
                {
                    entries.AddRange(ExtractFieldsAndPropertiesForLocalization(button));
                }
            }

            foreach (var tutorial in FindAssets<Tutorial>())
                entries.AddRange(ExtractFieldsAndPropertiesForLocalization(tutorial));

            foreach (var pg in FindAssets<TutorialPage>())
            {
                entries.AddRange(ExtractFieldsAndPropertiesForLocalization(pg));

                foreach (var paragraph in pg.paragraphs)
                {
                    entries.AddRange(ExtractFieldsAndPropertiesForLocalization(paragraph));
                }
            }

            // Use Reference-UntranslatedString pair as a key to get rid of duplicate entries.
            var uniqueEntries = entries
                .GroupBy(entry => new { entry.Reference, entry.UntranslatedString })
                .Select(group => group.FirstOrDefault());

            foreach (var code in SupportedLanguages.Values)
                WritePOFile(Application.productName, Application.version, code, uniqueEntries, $"{poFolderPath}/{code}.po");
        }

        [MenuItem(TutorialWindowMenuItem.MenuPath + "Open Welcome Dialog (Internal)")]
        static void OpenWelcomeDialog()
        {
            var welcomePage = TutorialProjectSettings.instance.WelcomePage;
            if (welcomePage != null)
                TutorialModalWindow.TryToShow(welcomePage, () => {});
            else
                Debug.LogError("No TutorialProjectSettings.WelcomePage set.");
        }

        [MenuItem(TutorialWindowMenuItem.MenuPath + "Run First Launch Experience (Internal)")]
        static void ExecuteFirstLaunchExperience()
        {
            UserStartupCode.RunStartupCode();
        }

        [MenuItem(TutorialWindowMenuItem.MenuPath + "Clear InitCodeMarker (Internal)", isValidateFunction: true)]
        static bool Validate_ClearInitCodeMarker()
        {
            return File.Exists(UserStartupCode.initFileMarkerPath);
        }

        [MenuItem(TutorialWindowMenuItem.MenuPath + "Clear InitCodeMarker (Internal)", isValidateFunction: false)]
        static void ClearInitCodeMarker()
        {
            File.Delete(UserStartupCode.initFileMarkerPath);
        }

        // These resolutions cover 87 % of our new macOS and Windows users (spring 2020)
        // <resolution> -- <ratio>
        // 1366 x 768   ‭-- 1.778645833333333‬
        // 1440 x 900   -- 1.6
        // 1600 x 900   ‭-- 1.777777777777778‬
        // 1680 x 1050  -- 1.6
        // 1280 x 800   -- 1.6
        // 1920 x 1080  -- ‭1.777777777777778‬
        // 2560 x 1440  -- ‭1.777777777777778‬
        // 3840 x 2160  -- ‭1.777777777777778‬
        // Looking at the ratios, we could maybe drop the many of these and keep only single one of each ration.
        [MenuItem(TutorialWindowMenuItem.MenuPath +  "Layout/Window Size/1366 x 768")]
        static void SetWindowSize_1366x768() => SetMainWindowSize(1366, 768);
        [MenuItem(TutorialWindowMenuItem.MenuPath + "Layout/Window Size/1440 x 900")]
        static void SetWindowSize_1440x900() => SetMainWindowSize(1440, 900);
        [MenuItem(TutorialWindowMenuItem.MenuPath + "Layout/Window Size/1600 x 900")]
        static void SetWindowSize_1600x900() => SetMainWindowSize(1600, 900);
        [MenuItem(TutorialWindowMenuItem.MenuPath + "Layout/Window Size/1680 x 1050")]
        static void SetWindowSize_1680x1050() => SetMainWindowSize(1680, 1050);
        [MenuItem(TutorialWindowMenuItem.MenuPath + "Layout/Window Size/1280 x 800")]
        static void SetWindowSize_1280x800() => SetMainWindowSize(1280, 800);
        [MenuItem(TutorialWindowMenuItem.MenuPath + "Layout/Window Size/1920 x 1080")]
        static void SetWindowSize_1920x1080() => SetMainWindowSize(1920, 1080);
        [MenuItem(TutorialWindowMenuItem.MenuPath + "Layout/Window Size/2560 x 1440")]
        static void SetWindowSize_2560x1440() => SetMainWindowSize(2560, 1440);
        [MenuItem(TutorialWindowMenuItem.MenuPath + "Layout/Window Size/3840 x 2160")]
        static void SetMainWindowSize_3840x2160() => SetMainWindowSize(3840, 2160);
        [MenuItem(TutorialWindowMenuItem.MenuPath + "Layout/Window Size/Arbitrary...")]
        static void SetMainWindowSize_Arbitrary() => SetWindowSizeDialog.ShowAsUtility();

        internal static void SetMainWindowSize(int w, int h)
        {
            var pos = EditorWindowUtils.GetEditorMainWindowPos();
            pos.width = w;
            pos.height = h;
            EditorWindowUtils.SetEditorMainWindowPos(pos);
        }

        static IEnumerable<T> FindAssets<T>() where T : Object =>
            AssetDatabase.FindAssets($"t:{typeof(T).FullName}")
                .Select(AssetDatabase.GUIDToAssetPath)
                .Select(AssetDatabase.LoadAssetAtPath<T>);

        static IEnumerable<POEntry> ExtractFieldsAndPropertiesForLocalization(object obj)
        {
            const BindingFlags bindedTypes = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;
            var localizableStringType = typeof(LocalizableString);

            var localizableProps = obj.GetType().GetProperties(bindedTypes)
                .Where(pi => pi.PropertyType == localizableStringType)
                .Select(pi => new POEntry
                {
                    Reference = $"{obj.GetType().Name}.{pi.Name}",
                    UntranslatedString = (pi.GetValue(obj) as LocalizableString).Untranslated
                });

            var localizableFields = obj.GetType().GetFields(bindedTypes)
                .Where(fi => fi.FieldType == localizableStringType)
                .Select(fi => new POEntry
                {
                    Reference = $"{obj.GetType().Name}.{fi.Name}",
                    UntranslatedString = (fi.GetValue(obj) as LocalizableString).Untranslated
                });

            return localizableProps.Concat(localizableFields);
        }
    }

    class SetWindowSizeDialog : EditorWindow
    {
        int m_MainWinWidth, m_MainWinHeight;

        public static void ShowAsUtility()
        {
            var wnd = CreateInstance<SetWindowSizeDialog>();
            EditorWindowUtils.CenterOnMainWindow(wnd);
            wnd.ShowUtility();
        }

        void OnEnable()
        {
            titleContent.text = Localization.Tr("Set Window Size");
            minSize = maxSize = new Vector2(300, 100);
        }

        void OnGUI()
        {
            m_MainWinWidth = EditorGUILayout.IntField("Width", m_MainWinWidth);
            // the min. width and height for main window size found by trial-and-error
            m_MainWinWidth = Mathf.Max(875, m_MainWinWidth);
            m_MainWinHeight = EditorGUILayout.IntField("Height", m_MainWinHeight);
            m_MainWinHeight = Mathf.Max(542, m_MainWinHeight);

            GUILayout.FlexibleSpace();
            if (GUILayout.Button(Localization.Tr("Set")))
            {
                InternalMenuItem.SetMainWindowSize(m_MainWinWidth, m_MainWinHeight);
            }

            GUILayout.FlexibleSpace();
        }
    }
}
