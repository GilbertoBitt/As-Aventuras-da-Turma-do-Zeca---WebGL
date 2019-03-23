using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;


[CreateAssetMenu(fileName = "LogHandler", menuName = "LogHandlers")]
public class LogManager : SerializedScriptableObject {
    private readonly StringFast _stringHandler = new StringFast();
    public Dictionary<string, int> LogCache = new Dictionary<string, int>();
    public string colorHex;
    public Color _color;

    public void LogShow(string logText) {
        Debug.Log(logText);
        HandlerLogCache(logText);
    }

    public void LogShow(string logText,string hexColor) {
        _stringHandler.Clear();
        _stringHandler.Append("<color=hexColor>").Append(logText).Append("</color>");
        Debug.Log(_stringHandler.ToString());
        HandlerLogCache(logText);
    }

    public string ReturnTextWithColor(string logText, string hexColor) {
        _stringHandler.Clear();
        _stringHandler.Append("<color=hexColor>").Append(logText).Append("</color>");
        HandlerLogCache(logText);
        return _stringHandler.ToString();
        //ebug.Log(logText);
    }

    private void HandlerLogCache(string logText) {
        if (LogCache.ContainsKey(logText)) {
            LogCache[logText]++;
        }
    }

    [Button("Clear Log Cache")]
    public void ClearCache() {
        LogCache.Clear();
    }

    [Button("Color Show Value")]
    public void ColorPicker() {
        Vector4 color4 = _color;
        Debug.Log("Color: " + color4);
    }
}

