using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
    }

    public void PlayGame()
    {
        string file = OptionMenu.FileName;
        if (string.IsNullOrEmpty(file))
            return;
        if (!checkFile(file))
            return;
        NormalizeData();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    void NormalizeData()
    {
        Normalizer normalizer = new Normalizer();
        normalizer.NormalizeData();
    }

    bool checkFile(string fileName)
    {
        Parser parser = new Parser();
        parser.init(fileName);
        if (!parser.checkExtension())
            return (false);
        if (!parser.ReadFile())
            return (false);
        return (true);
    }

    public void LeaveGame()
    {
        Application.Quit();
    }
}
