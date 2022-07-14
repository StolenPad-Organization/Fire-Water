#if UNITY_EDITOR
namespace Stolenpad.Utils
{
    using UnityEditor;
    using UnityEngine;

    [CustomEditor(typeof(ScreenshotCapture))]
    public class ScreenshotCapture_CustomInspector : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            ScreenshotCapture screenshotCapture = (ScreenshotCapture)target;
            if (GUILayout.Button("Multiple Screenshot Caputre"))
                screenshotCapture.MultipleScreenshot();
            if (GUILayout.Button("Single Screenshot Caputre"))
                screenshotCapture.SingleScreenshot();


            string howToUse = "HOW TO USE:  \n " +
                "1- Set All the below needed Resolution in the 'Game' window 'Aspect': \n" +
                "- IPhone 12 Pro Portrait, Resolution : 1284 x 2778 \n" +
                "- IPhone 8 Portrait, Resolution : 1242 x 2208 \n" +
                "- IPad Pro Portraint, resolution 2048 x 2732 \n" +
                "2- Drag and Drop the Prefab 'ScreenshotCapturePrefab' into your first scene \n" +
                "3- Select the desired Aspect in the 'Game' window \n" +
                "4- To Take a screenshot, select the 'ScreenshotCapturePrefab', and in the inspector chose a file name and click 'Screenshot Capture' \n" +
                "5- The Image will be Saved in the Root Project Folder" +
                "TIP: IT WORKS WHEN THE GAME IS PAUSED.";
            EditorGUILayout.HelpBox(howToUse, MessageType.Info);
        }
    }
}
#endif