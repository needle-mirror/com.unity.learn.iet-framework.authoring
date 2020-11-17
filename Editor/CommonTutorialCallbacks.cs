using System.Linq;
using UnityEditor;
using UnityEngine;
using Unity.Tutorials.Core.Editor;

namespace Unity.Tutorials.Authoring.Editor
{
    /// <summary>
    /// A set of common tutorial callbacks that Tutorial creators can use.
    /// Callback functions need to be public instance methods.
    /// </summary>
    // Enable the following line temporarily if you need to create an instance of this.
    //[CreateAssetMenu(fileName = "CommonTutorialCallbacksHandler", menuName = "Tutorials/CommonTutorialCallbacksHandler")]
    public class CommonTutorialCallbacks : ScriptableObject
    {
        /// <summary>
        /// Mutes or unmutes the editor audio.
        /// </summary>
        /// <param name="mute">Will the editor audio be muted?</param>
        public void SetAudioMasterMute(bool mute)
        {
            EditorUtility.audioMasterMute = mute;
        }

        /// <summary>
        /// Highlights a folder/asset in the Project window.
        /// </summary>
        /// <param name="folderPath">All paths are relative to the project folder, examples:
        /// - "Assets/Hello.png"
        /// - "Packages/com.unity.somepackage/Hello.png"
        /// </param>
        public void PingFolderOrAsset(string folderPath)
        {
            // Null/empty/invalid paths are handled without problems
            EditorGUIUtility.PingObject(AssetDatabase.LoadAssetAtPath<Object>(folderPath));
        }

        /// <summary>
        /// Highlights a folder or the first asset in it, in the Project window.
        /// </summary>
        /// <param name="folderPath">All paths are relative to the project folder, examples:
        /// - "Assets/Hello.png"
        /// - "Packages/com.unity.somepackage/Hello.png"
        /// </param>
        public void PingFolderOrFirstAsset(string folderPath)
        {
            PingFolderOrAsset(GetFirstAssetPathInFolder(folderPath, true));
        }

        static string GetFirstAssetPathInFolder(string folder, bool includeFolders)
        {
            try
            {
                if (includeFolders)
                {
                    string path = GetFirstValidAssetPath(System.IO.Directory.GetDirectories(folder));
                    if (path != null)
                    {
                        return path;
                    }
                }

                return GetFirstValidAssetPath(System.IO.Directory.GetFiles(folder));
            }
            catch
            {
                return null;
            }
        }

        static string GetFirstValidAssetPath(string[] paths) =>
            paths.Where(path => AssetDatabase.AssetPathToGUID(path).IsNotNullOrEmpty()).FirstOrDefault();
    }
}
