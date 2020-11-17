using NUnit.Framework;
using System;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace Unity.Tutorials.Authoring.Editor.Tests
{
    public class TutorialAssetUtilTests
    {
        const string k_LocalizationAssetsPath = "Assets/Localization";

        [SetUp]
        public void SetUp()
        {
            Assert.That(File.Exists(BuildPath), Is.False, "Existing file at path " + BuildPath);
            Assert.That(Directory.Exists(BuildPath), Is.False, "Existing directory at path " + BuildPath);

            TutorialAssetsUtil.CreateLocalizationAssets();
        }

        [TearDown]
        public void TearDown()
        {
            if (File.Exists(BuildPath))
                File.Delete(BuildPath);

            if (Directory.Exists(BuildPath))
                Directory.Delete(BuildPath, true);

            AssetDatabase.DeleteAsset(k_LocalizationAssetsPath);
        }

        static string BuildPath
        {
            get
            {
                // NOTE Use "Builds" subfolder in order to make this test pass locally when
                // using Windows & Visual Studio
                const string buildName = "Builds/BuildPlayerTests_Build";
                if (Application.platform == RuntimePlatform.OSXEditor)
                    return buildName + ".app";
                return buildName;
            }
        }

        static BuildTarget BuildTarget
        {
            get
            {
                switch (Application.platform)
                {
                    case RuntimePlatform.OSXEditor:
                        return BuildTarget.StandaloneOSX;
                    case RuntimePlatform.WindowsEditor:
                        return BuildTarget.StandaloneWindows;
                    case RuntimePlatform.LinuxEditor:
                        // NOTE Universal & 32-bit Linux support dropped after 2018 LTS
                        return BuildTarget.StandaloneLinux64;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        [Test]
        public void BuildPlayerWithLocalizationAssets_Succeeds()
        {
            BuildPipeline.BuildPlayer(
                new BuildPlayerOptions
                {
                    // Borrow the test scene from Tutorial Framework
                    scenes = new[] { "Packages/com.unity.learn.iet-framework/Tests/Editor/EmptyTestScene.unity" },
                    locationPathName = BuildPath,
                    targetGroup = BuildTargetGroup.Standalone,
                    target = BuildTarget,
                    options = BuildOptions.StrictMode,
                }
            );
        }
    }
}
