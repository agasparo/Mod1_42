using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class OptionMenu : MonoBehaviour
{
    public static string FileName = "";
    public Text FileShow;

    public void SelectFile()
    {
        FileName = EditorUtility.OpenFilePanel("Load scan file", "", "mod1");
        FileShow.text = FileName;
    } 
}
//IsNullOrEmpty