using UnityEditor;

namespace Unity.InteractiveTutorials
{
    static class TutorialWindowMenuItem
    {
        [MenuItem("Tutorials/Open Tutorials Window (Internal)")]
        static void OpenTutorialWindow()
        {
            TutorialWindow.CreateWindow();
        }
    }
}
