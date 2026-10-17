using System.Linq;
using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEngine;

/// <summary>
/// Command-line entry point for WebGL builds (unity build --execute-method WebBuilder.Build).
/// Builds every scene enabled in Build Settings to the path given by -buildOutput.
/// Does NOT touch player settings; compression/template come from the project as configured.
/// Exists because the CLI has no built-in WebGL build without a method or build profile.
/// Author: Damyan
/// </summary>
public static class WebBuilder
{
    public static void Build()
    {
        string output = GetArg("-buildOutput") ?? "Build/Web";
        string[] scenes = EditorBuildSettings.scenes.Where(s => s.enabled).Select(s => s.path).ToArray();

        BuildReport report = BuildPipeline.BuildPlayer(scenes, output, BuildTarget.WebGL, BuildOptions.None);
        Debug.Log($"[WebBuilder] {report.summary.result} -> {output} ({report.summary.totalSize} bytes)");

        EditorApplication.Exit(report.summary.result == BuildResult.Succeeded ? 0 : 1);
    }

    private static string GetArg(string name)
    {
        string[] args = System.Environment.GetCommandLineArgs();
        int i = System.Array.IndexOf(args, name);
        return i >= 0 && i + 1 < args.Length ? args[i + 1] : null;
    }
}
