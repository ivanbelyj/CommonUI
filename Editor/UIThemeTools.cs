#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEditor.ShortcutManagement;
using UnityEngine;

public static class UIThemeTools
{
    private const string MenuRoot = "Tools/UI/";
    private const string UndoName = "Refresh from Theme";

    [MenuItem(MenuRoot + "Refresh from Theme")]
    private static void RefreshCurrent()
    {
        var stage = PrefabStageUtility.GetCurrentPrefabStage();

        if (stage != null && stage.prefabContentsRoot != null)
        {
            int prefabCount = RefreshRecursive(new[] { stage.prefabContentsRoot });
            Debug.Log($"[UI] Refreshed {prefabCount} element(s) in prefab stage.");
            return;
        }

        var sceneElements = Object.FindObjectsByType<ThemedElement>(FindObjectsSortMode.None);
        int sceneCount = Refresh(sceneElements);
        Debug.Log($"[UI] Refreshed {sceneCount} element(s) in open scenes.");
    }

    [MenuItem("GameObject/UI/Refresh from Theme", false, 100)]
    private static void RefreshSelected()
    {
        var selected = Selection.gameObjects;
        if (selected == null || selected.Length == 0)
        {
            Debug.LogWarning("[UI] Nothing selected.");
            return;
        }

        int count = RefreshRecursive(selected);
        Debug.Log($"[UI] Refreshed {count} element(s) in selection.");
    }

    [Shortcut("Tools/UI/Refresh from Theme", KeyCode.R, ShortcutModifiers.Alt)]
    private static void RefreshFromThemeShortcut()
    {
        RefreshCurrent();
    }

    private static int RefreshRecursive(GameObject[] roots)
    {
        var seen = new HashSet<ThemedElement>();
        var flat = new List<ThemedElement>();

        foreach (var root in roots)
        {
            if (root == null) continue;
            foreach (var e in root.GetComponentsInChildren<ThemedElement>(true))
            {
                if (e != null && seen.Add(e))
                    flat.Add(e);
            }
        }

        return Refresh(flat);
    }

    private static int Refresh(IReadOnlyList<ThemedElement> elements)
    {
        if (elements.Count == 0) return 0;

        Undo.IncrementCurrentGroup();
        int group = Undo.GetCurrentGroup();
        Undo.SetCurrentGroupName(UndoName);

        foreach (var e in elements)
        {
            if (e == null) continue;
            e.RefreshFromTheme();
        }

        Undo.CollapseUndoOperations(group);

        return elements.Count;
    }
}
#endif