using System.Collections.Generic;
using UnityEngine;

public class TouchTests : MonoBehaviour
{   
    [SerializeField]
    private bool showDebugLog = false;

    protected int touchCount = 0;

    static GUIStyle style;
    // Start is called before the first frame update

    public static string debugString = string.Empty;

    public static List<string> debugInfo;

    void Start()
    {
        style = new GUIStyle();
        style.fontSize = 22;
        style.normal.textColor = Color.black;

        debugInfo = new List<string>();
    }

    void OnGUI()
    {
        if(showDebugLog) {
            GUI.Label(new Rect(10, 10, 150, 100), "TouchCount: " + touchCount, style);
            PrintDebugText();
        }
        
    }

    public static void SetDebugString(string debugText) {
        debugInfo = new List<string>();
        debugInfo.Add(debugText);
    }

    public static void AddDebugString(string debugText) {
        debugInfo.Add(debugText);
    }

    public static void PrintDebugText() {
        for(int i = 0; i < debugInfo.Count; i++) {
            string debugString = debugInfo[i];
            GUI.Label(new Rect(10, 30 + i * 20, 150, 100), debugString, style);
        }
        
    }
}
