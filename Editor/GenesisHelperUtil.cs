using UnityEditor;
using UnityEngine;
using Unity.Tutorials.Editor;

namespace Unity.Tutorials.Authoring.Editor
{
    using static Localization;

    class GenesisHelperUtils
    {
        [MenuItem(MenuItems.AuthoringMenuPath + "Debug/Progress Tracking/Print All Statuses")]
        static void PrintAllStatuses()
        {
            GenesisHelper.PrintAllTutorials();
        }

        [MenuItem(MenuItems.AuthoringMenuPath + "Debug/Progress Tracking/Clear All Statuses")]
        static void ClearAllStatuses()
        {
            if (EditorUtility.DisplayDialog(
                string.Empty,
                Tr("Do you want to clear progress of every tutorial?"),
                Tr("Clear"),
                Tr("Cancel")))
            {
                GenesisHelper.GetAllTutorials((r) =>
                {
                    foreach (GenesisHelper.TutorialProgressStatus status in r)
                    {
                        GenesisHelper.LogTutorialStatusUpdate(status.lessonId, " ");
                    }
                    Debug.Log("Lesson statuses cleared");
                    // Refresh the window, if it's open.
                    var wnd = TutorialWindow.Instance;
                    if (wnd)
                    {
                        wnd.MarkAllTutorialsUncompleted();
                    }
                });
            }
        }
    }
}
