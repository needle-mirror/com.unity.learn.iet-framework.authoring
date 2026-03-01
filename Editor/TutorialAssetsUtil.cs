using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using Unity.Tutorials.Editor;
using Unity.Tutorials.Editor.Paragraphs;
using PackageInfo = UnityEditor.PackageManager.PackageInfo;
using static Unity.Tutorials.Editor.TutorialEditorUtils;

namespace Unity.Tutorials.Authoring.Editor
{
    internal static class TutorialAssetsUtil
    {
        private const string k_Menu = "Assets/Create/Tutorials/";
        private const string k_PresetSubmenu = k_Menu + "Presets/";

        // Menu priorities
        private const int k_WelcomePagePriority = 0;
        private const int k_ProjectSettingsPriority = 1;

        private const int k_TutorialContainerPriority = 101;
        private const int k_TutorialPriority = 102;

        private const int k_StylesPriority = 200;
        private const int k_LocalizationPriority = 203;

        private const int k_ProjectPresetPriority = 300;

        #region Create Menu

        [MenuItem(k_Menu + "Welcome Page", priority = k_WelcomePagePriority)]
        private static void CreateTutorialWelcomePage()
        {
            TutorialWelcomePage newWelcomePage = SetupTutorialWelcomePage();
            SaveAssetAndRename("NewWelcomePage.asset", newWelcomePage);
        }

        private static TutorialWelcomePage SetupTutorialWelcomePage()
        {
            TutorialWelcomePage asset = ScriptableObject.CreateInstance<TutorialWelcomePage>();
            asset.m_WindowTitle = "Window Title";
            asset.m_Title = "This is the title";
            asset.m_Description = "This is the description.";
            asset.m_Buttons = new[] { TutorialWelcomePage.CreateCloseButton(asset) };
            return asset;
        }

        [MenuItem(k_Menu + "Project Settings", priority = k_ProjectSettingsPriority)]
        private static void CreateTutorialProjectSettings()
        {
            TutorialProjectSettings newSettings = ScriptableObject.CreateInstance<TutorialProjectSettings>();
            SaveAssetAndRename("TutorialProjectSettings.asset", newSettings);
            TutorialProjectSettings.Instance = newSettings;
        }

        [MenuItem(k_Menu + "Tutorial Container", priority = k_TutorialContainerPriority)]
        private static void CreateTutorialContainer() => CreateAssetAndRename<TutorialContainer>("TutorialContainer.asset");

        [MenuItem(k_Menu + "Tutorial", priority = k_TutorialPriority)]
        private static void CreateEmptyTutorial() => CreateAssetAndRename<Tutorial>("NewTutorial.asset");

        [MenuItem(k_Menu + "Tutorial Styles", priority = k_StylesPriority, secondaryPriority = 0f)]
        private static void CreateTutorialStyles() => CreateAssetAndRename<TutorialStyles>("NewTutorialStyles.asset");

        [MenuItem(k_Menu + "Localization Assets", priority = k_LocalizationPriority)]
        private static void CreateLocalizationAssets() => CreateLocalizationAssets($"{GetActiveFolderPath()}/Localization");

        internal static void CreateLocalizationAssets(string path)
        {
            try
            {
                UserStartupCode.DirectoryCopy(
                    $"{PackageInfo.FindForAssembly(Assembly.GetExecutingAssembly()).assetPath}/.LocalizationAssets",
                    path
                );
                AssetDatabase.Refresh();
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }

        [MenuItem(k_PresetSubmenu + "Complete Project", priority = k_ProjectPresetPriority)]
        private static void CreatePremadeTutorialProject()
        {
            // Welcome page
            TutorialWelcomePage newWelcomePage = SetupTutorialWelcomePage();
            CreateAsset(newWelcomePage, "WelcomeWindow");

            // Styles
            TutorialStyles newTutorialStyles = ScriptableObject.CreateInstance<TutorialStyles>();
            CreateAsset(newTutorialStyles, "TutorialStyles");

            // Tutorial
            Tutorial newTutorial = ScriptableObject.CreateInstance<Tutorial>();
            newTutorial.TutorialTitle = "Tutorial 1";
            CreateAsset(newTutorial, "Tutorial_1");

            // Page 1
            TutorialPage page1 = TutorialEditor.CreateNewPageAsSubAsset(newTutorial);
            AssetDatabase.SaveAssets(); // Needed, so that the TutorialPage sub-asset is not null
            page1.Title = "Title of page 1";
            newTutorial.AddPage(page1);

            NarrativeParagraph newNarrative = page1.AddParagraph<NarrativeParagraph>();
            newNarrative.Text = "Example of a narrative paragraph for page 1.";

            InstructionsParagraph newInstructions = page1.AddParagraph<InstructionsParagraph>();
            newInstructions.Text = "1. First step\n2. Second step.";

            // Page 2
            TutorialPage page2 = TutorialEditor.CreateNewPageAsSubAsset(newTutorial);
            AssetDatabase.SaveAssets(); // Needed, so that the TutorialPage sub-asset is not null
            page2.Title = "Title of page 2";
            newTutorial.AddPage(page2);

            newNarrative = page2.AddParagraph<NarrativeParagraph>();
            newNarrative.Text = "This would be the narrative of page 2.";

            // Container
            TutorialContainer newContainer = ScriptableObject.CreateInstance<TutorialContainer>();
            newContainer.Sections = new[]
            {
                new TutorialContainer.Section { Heading = "Tutorial 1", Tutorial = newTutorial }
            };
            CreateAsset(newContainer, "TutorialContainer");

            // Project Settings
            TutorialProjectSettings newProjectSettings = ScriptableObject.CreateInstance<TutorialProjectSettings>();
            newProjectSettings.WelcomePage = newWelcomePage;
            newProjectSettings.TutorialStyle = newTutorialStyles;
            CreateAsset(newProjectSettings, "TutorialProjectSettings");

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        private static void CreateAsset<T>(T newAsset, string desiredName) where T : ScriptableObject
        {
            string activeFolderPath = GetActiveFolderPath();
            string newName = GetUniqueAssetName(activeFolderPath, desiredName);
            AssetDatabase.CreateAsset(newAsset, $"{activeFolderPath}/{newName}");
        }

        private static void CreateAssetAndRename<T>(string assetName) where T : ScriptableObject
        {
            SaveAssetAndRename(assetName, ScriptableObject.CreateInstance<T>());
        }

        private static void SaveAssetAndRename(string assetName, UnityEngine.Object asset)
        {
            ProjectWindowUtil.CreateAsset(asset, $"{GetActiveFolderPath()}/{assetName}");
        }

        #endregion

        #region Top Menus

        [MenuItem(MenuItems.AuthoringMenuPath + "Layout/Save Current Layout to Asset...")]
        private static void SaveCurrentLayoutToAsset()
        {
            string path = EditorUtility.SaveFilePanelInProject("Save Layout", "layout.wlt", "wlt", "Choose the location to save the layout");
            if (path.Length != 0)
            {
                WindowLayoutProxy.SaveWindowLayout(path);
                AssetDatabase.Refresh();
            }
        }

        [MenuItem(MenuItems.AuthoringMenuPath + "Layout/Load Layout...")]
        private static void LoadLayout()
        {
            string path = EditorUtility.OpenFilePanelWithFilters("Open Layout", "", new[] { "Layout files", "wlt,dwlt" });
            if (path.Length != 0)
            {
                TutorialModel.LoadWindowLayout(path);
            }
        }

        #endregion
    }
}
