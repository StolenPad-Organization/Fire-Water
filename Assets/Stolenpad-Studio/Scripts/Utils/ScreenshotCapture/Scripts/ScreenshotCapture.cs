#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using UnityEditor;
using UnityEditorInternal;
using System;

public class ScreenshotCapture : Stolenpad.Utils.SingletonMB<ScreenshotCapture>
{
    [SerializeField] string fileName = "File Name";
    public void MultipleScreenshot()
    {
        StartCoroutine(ScreenShotRoutine());
    }

    public void SingleScreenshot()
    {
        ScreenCapture.CaptureScreenshot(fileName + ".png");
        Debug.Log("Screen Shot Taken " + "fileName");
    }

    IEnumerator ScreenShotRoutine()
    {
        for (int i = 0; i < 3; i++)
        {
            SetResolution(GetCount() - (i + 1));
            yield return null;
            switch (i)
            {
                case 0:
                    ScreenCapture.CaptureScreenshot("IPad - " + fileName + ".png");
                    Debug.Log("Screen Shot Taken " + " IPad");
                    break;
                case 1:
                    ScreenCapture.CaptureScreenshot("IPhone 8 - " + fileName + ".png");
                    Debug.Log("Screen Shot Taken " + " IPhone 8");
                    break;
                case 2:
                    ScreenCapture.CaptureScreenshot("IPhone 12 - " + fileName + ".png");
                    Debug.Log("Screen Shot Taken " + " IPhone 12");
                    break;
                default:
                    break;
            }
            yield return null;
        }
    }

    int GetCount()
    {
        Type gameViewSizes = typeof(Editor).Assembly.GetType("UnityEditor.GameViewSizes");
        Type generic = typeof(ScriptableSingleton<>).MakeGenericType(gameViewSizes);
        MethodInfo getGroup = gameViewSizes.GetMethod("GetGroup");
        object instance = generic.GetProperty("instance").GetValue(null, null);
        PropertyInfo currentGroupType = instance.GetType().GetProperty("currentGroupType");
        GameViewSizeGroupType groupType = (GameViewSizeGroupType)(int)currentGroupType.GetValue(instance, null);
        object group = getGroup.Invoke(instance, new object[] { (int)groupType });
        MethodInfo getBuiltinCount = group.GetType().GetMethod("GetBuiltinCount");
        MethodInfo getCustomCount = group.GetType().GetMethod("GetCustomCount");
        return (int)getBuiltinCount.Invoke(group, null) + (int)getCustomCount.Invoke(group, null);
    }

    void SetResolution(int index)
    {
        Type gameView = typeof(Editor).Assembly.GetType("UnityEditor.GameView");
        PropertyInfo selectedSizeIndex = gameView.GetProperty("selectedSizeIndex", BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
        EditorWindow window = EditorWindow.GetWindow(gameView);
        selectedSizeIndex.SetValue(window, index, null);
    }
}
#endif