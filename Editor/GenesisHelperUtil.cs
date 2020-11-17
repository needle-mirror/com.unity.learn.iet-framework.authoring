using System.Linq;
using UnityEditor;
using UnityEngine;
using Unity.Tutorials.Core.Editor;

namespace Unity.Tutorials.Authoring.Editor
{
    class GenesisHelperUtils
    {
        [MenuItem("Tutorials/Genesis/Print all statuses")]
        static void PrintAllStatuses()
        {
            GenesisHelper.PrintAllTutorials();
        }

        [MenuItem("Tutorials/Genesis/Clear all statuses")]
        static void ClearAllStatuses()
        {
            if (EditorUtility.DisplayDialog("", "Do you want to clear progress of every tutorial?", "Clear", "Cancel"))
            {
                GenesisHelper.GetAllTutorials((r) =>
                {
                    var ids = r.Select(a => a.lessonId);
                    foreach (var id in ids)
                    {
                        GenesisHelper.LogTutorialStatusUpdate(id, " ");
                    }
                    Debug.Log("Lesson statuses cleared");
                });

                // Refresh the window, if it's open.
                var wnd = TutorialManager.GetTutorialWindow();
                if (wnd)
                {
                    wnd.MarkAllTutorialsUncompleted();
                }
            }
        }
    }
}
