using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
using UnityEditor.SceneManagement;
using System.Linq;

public class SceneChanger : EditorWindow
{
    const string BUTTON_STYLE_KEY = "SceneChanger.ButtonStyle";
    const string DEFAULT_BUTTON_STYLE = "button";
    const string MINI_BUTTON_STYLE = "minibutton";

    private string ButtonStyle
    {
        get
        {
            return EditorPrefs.GetString(BUTTON_STYLE_KEY, DEFAULT_BUTTON_STYLE);
        }
        set
        {
            EditorPrefs.SetString(BUTTON_STYLE_KEY, value);
        }
    }

    private static string[] sceneGuids;
    private static List<string> scenepaths = new List<string>();
    private Vector2 scrollPosition;
    private bool vertical;
    private bool mini;
    private static readonly string[] specialIncludes = { "AnySceneNameHere" };

    [MenuItem("Custom Tools/Scene Changer")]
    static void Init()
    {
        SceneChanger window = (SceneChanger)EditorWindow.GetWindow(typeof(SceneChanger));
        window.Show();

        sceneGuids = AssetDatabase.FindAssets("t:Scene");
        List<string> sceneNames = sceneGuids.Select(AssetDatabase.GUIDToAssetPath).ToList();

        foreach (string s in sceneNames) //find all scenes containing sandbox or in a path containing sandbox
        {
            if (specialIncludes.Any(s.Contains))
            {
                scenepaths.Add(s);
            }
        }

        foreach (EditorBuildSettingsScene s in EditorBuildSettings.scenes) //find all build scenes
        {
            scenepaths.Add(s.path);
        }

        scenepaths.Sort();

        window.minSize = new Vector2(0, 0);
    }



    public void OnGUI()
    {
        if (scenepaths.Count == 0)
        {
            Init();
        }

        scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition, "objectfieldthumb");

        if (position.width > position.height)
        {
            vertical = false;
            EditorGUILayout.BeginHorizontal();
        }
        else
        {
            vertical = true;
            EditorGUILayout.BeginVertical();
        }

        mini = ButtonStyle == MINI_BUTTON_STYLE;

        DoSettingsMenu();

        DoSceneButtons();

        if (vertical)
        {
            EditorGUILayout.EndVertical();
        }
        else
        {
            EditorGUILayout.EndHorizontal();
        }

        EditorGUILayout.EndScrollView();
    }

    private void DoSettingsMenu()
    {
        if (GUILayout.Button(EditorGUIUtility.FindTexture("_Popup"), EditorStyles.label))
        {
            GenericMenu gm = new GenericMenu();
            gm.AddItem(new GUIContent(mini ? "Normal Button Mode" : "Mini Button Mode"), false, () => ButtonStyle = mini ? DEFAULT_BUTTON_STYLE : MINI_BUTTON_STYLE);
            gm.ShowAsContext();
        }
    }

    private void DoSceneButtons()
    {
        foreach (string s in scenepaths)
        {
            string[] name = s.Split('/');
            string buttonLabel = name[name.Length - 1].Replace(".unity", "");

            EditorGUILayout.BeginVertical();

            EditorGUI.BeginChangeCheck();

            bool toggle = EditorSceneManager.GetSceneByPath(s).isLoaded; //should be whether scene is open
            toggle = GUILayout.Toggle(toggle, buttonLabel, ButtonStyle, GUILayout.ExpandWidth(true), GUILayout.ExpandHeight(!mini));
            DrawNiceLittleColoredLine(buttonLabel, vertical);

            if (EditorGUI.EndChangeCheck())
            {
                OpenCloseScene(s);
            }

            EditorGUILayout.EndVertical();
        }
    }

    void OpenCloseScene(string scenePath)
    {
        if (Event.current.modifiers == EventModifiers.Shift)
        {
            if (EditorSceneManager.GetSceneByPath(scenePath).isLoaded)
            {
                bool save = EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
                if (!save) //this is if you press cancel on the popupwindow
                {
                    return;
                }

                EditorSceneManager.CloseScene(EditorSceneManager.GetSceneByPath(scenePath), true);
            }
            else
            {
                EditorSceneManager.OpenScene(scenePath, OpenSceneMode.Additive); //in this case we don't care about the user saving since they're not closing the previous scene
            }
        }
        else
        {
            bool save = EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
            if (!save) //this is if you press cancel on the popupwindow
            {
                return;
            }

            EditorSceneManager.OpenScene(scenePath);
        }
    }

    void DrawNiceLittleColoredLine(string label, bool vertical)
    {
        float padding = mini ? 2.5f : 10f;
        Rect lastRect = GUILayoutUtility.GetLastRect();
        lastRect.x = vertical ? lastRect.xMax - padding : lastRect.x;
        lastRect.y = vertical ? lastRect.y : lastRect.yMax - padding;
        lastRect.width = vertical ? padding : lastRect.width;
        lastRect.height = vertical ? lastRect.height : padding;
        lastRect = lastRect.WithPadding(mini ? 0f : 2.5f);

        Color col = GetColorFromLabel(label);
        EditorGUI.DrawRect(lastRect, col);
    }

    private Color GetColorFromLabel(string label)
    {
        System.Random random = new System.Random(Animator.StringToHash(label + label)); //this gave better colors don't @ me.
        return new Color((random.Next(0, 255) / 255f), (random.Next(0, 255) / 255f), (random.Next(0, 255) / 255f));

    }
}