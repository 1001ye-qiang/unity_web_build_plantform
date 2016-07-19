using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections;
using System.Collections.Generic;
public class CommandBuild
{
    public static void BuildiOS()
    {
        string buildInfo = File.ReadAllText("./BuildInfo.txt");
        string[] infos = buildInfo.Split('|');
        Debug.Log(string.Format("bundleIdentifier:{0}", infos[0]));
        Debug.Log(string.Format("bundleVersion:{0}", infos[1]));
        Debug.Log(string.Format("defines:{0}", infos[2]));
        PlayerSettings.iPhoneBundleIdentifier = infos[0];
        PlayerSettings.bundleVersion = infos[1];
        PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.iPhone, infos[2]);
        string[] levels = FindEnabledEditorScenes();
        string Paths = "iOS";
        BuildPipeline.BuildPlayer(levels, Paths, BuildTarget.iPhone, BuildOptions.None);
    }

    public static void BuildAndroid()
    {
        string buildInfo = File.ReadAllText("./BuildInfo.txt");
        string[] infos = buildInfo.Split('|');
        Debug.Log(string.Format("bundleIdentifier:{0}", infos[0]));
        Debug.Log(string.Format("bundleVersion:{0}", infos[1]));
        Debug.Log(string.Format("bundleVersionCode:{0}", infos[2]));
        Debug.Log(string.Format("defines:{0}", infos[3]));
        Debug.Log(string.Format("keystoreName:{0}", infos[4]));
        Debug.Log(string.Format("keystorePass:{0}", infos[5]));
        Debug.Log(string.Format("keyaliasName:{0}", infos[6]));
        Debug.Log(string.Format("keyaliasPass:{0}", infos[7]));
        PlayerSettings.iPhoneBundleIdentifier = infos[0];
        PlayerSettings.bundleVersion = infos[1];
        PlayerSettings.Android.bundleVersionCode = int.Parse(infos[2]);
        PlayerSettings.SetScriptingDefineSymbolsForGroup(BuildTargetGroup.Android, infos[3]);
        PlayerSettings.Android.keystoreName = infos[4];
        PlayerSettings.Android.keystorePass = infos[5];
        PlayerSettings.Android.keyaliasName = infos[6];
        PlayerSettings.Android.keyaliasPass = infos[7];
        string[] levels = FindEnabledEditorScenes();
        string Paths = infos[0] + ".apk";
        BuildPipeline.BuildPlayer(levels, Paths, BuildTarget.Android, BuildOptions.None);
    }
    public static void PackAssetBundle()
    {
        string[] args = System.Environment.GetCommandLineArgs();
        string inpath = args[7];
        string outpath = args[8];
        string platform = args[9];
        List<Object> files = new List<Object>();
        Caching.CleanCache();
        if (inpath.Contains("."))
        {
            files.Add(AssetDatabase.LoadAssetAtPath(cut2AssetFolder(inpath), typeof(Object)));
        }
        else
        {
            string[] filePaths = Directory.GetFiles(inpath, "*", SearchOption.AllDirectories);
            for (int i = 0; i < filePaths.Length; ++i)
            {
                string filePath = cut2AssetFolder(filePaths[i]);
                files.Add(AssetDatabase.LoadAssetAtPath(filePath, typeof(Object)));
            }
        }

        if (!outpath.Contains("."))
        {
            string tName = inpath.Substring(inpath.LastIndexOf("/"));
            if (tName == "/")
                tName = inpath.Substring(inpath.Remove(inpath.LastIndexOf("/")).LastIndexOf("/")).Replace("/", "");
            if (tName.Contains("."))
                tName = tName.Remove(tName.IndexOf("."));
            outpath = string.Format("{0}/{1}{2}", outpath, tName, ".assetbundle");
        }
        if (files.Count != 0)
        {
            BuildTarget tar = platform == "ios" ? BuildTarget.iPhone : (platform == "android" ? BuildTarget.Android : BuildTarget.WP8Player);
            BuildPipeline.BuildAssetBundle(null, files.ToArray(), outpath, BuildAssetBundleOptions.CollectDependencies, tar);
        }
        else
        {
            throw new FileLoadException("Folder is empty!  Path: " + inpath);
        }
    }

    private static string cut2AssetFolder(string src)
    {
        return src.Substring(src.IndexOf("Assets"));
    }

    private static string[] FindEnabledEditorScenes()
    {
        List<string> EditorScenes = new List<string>();
        foreach (EditorBuildSettingsScene scene in EditorBuildSettings.scenes)
        {
            if (!scene.enabled) continue;
            EditorScenes.Add(scene.path);
        }
        return EditorScenes.ToArray();
    }
}