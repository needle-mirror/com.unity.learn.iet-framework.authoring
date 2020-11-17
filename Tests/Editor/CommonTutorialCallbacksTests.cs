using NUnit.Framework;
using UnityEngine;
using UnityEditor;

namespace Unity.Tutorials.Authoring.Editor.Tests
{
    public class CommonTutorialCallbacksTests
    {
        CommonTutorialCallbacks m_Callbacks;

        [SetUp]
        public void SetUp()
        {
            m_Callbacks = ScriptableObject.CreateInstance<CommonTutorialCallbacks>();
        }

        [TearDown]
        public void TearDown()
        {
            Object.DestroyImmediate(m_Callbacks);
        }

        [Test]
        [TestCase(true)]
        [TestCase(false)]
        public void SetAudioMasterMute(bool mute)
        {
            m_Callbacks.SetAudioMasterMute(mute);
            Assert.AreEqual(EditorUtility.audioMasterMute, mute);
        }

        // NOTE Confirming that the asset was actually pinged is not trivial
        // (it's not added to Selection, for example) so we settle for testing
        // that all kind of input is handled gracefully.
        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase("       ")]
        [TestCase("Assets/DoesNotExist.asset")]
        [TestCase("Packages/com.unity.learn.iet-framework.authoring/Readme.md")]
        public void PingFolderOrAsset_DoesNotThrowOnAnyInput(string path)
        {
            m_Callbacks.PingFolderOrAsset(path);
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase("       ")]
        [TestCase("Assets/DoesNotExist.asset")]
        [TestCase("Packages/com.unity.learn.iet-framework.authoring/Readme.md")]
        public void PingFolderOrFirstAsset_DoesNotThrowOnAnyInput(string path)
        {
            m_Callbacks.PingFolderOrFirstAsset(path);
        }
    }
}
