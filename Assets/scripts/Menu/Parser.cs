using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;

public class Parser
{
    public string fileName;
    List<string> contentCoord = new List<string>();

    public static List<int> dataX = new List<int>();
    public static List<int> dataY = new List<int>();
    public static List<int> dataZ = new List<int>();

    public void init(string file)
    {
        this.fileName = file;
    }

    public bool checkExtension()
    {
        string[] fileInfos = fileName.Split('.');
        if (fileInfos.Length != 2)
            return (false);
        if (fileInfos[1] != "mod1")
            return (false);
        return (true);
    }
    
    public bool ReadFile()
    {
        StreamReader inp_stm = new StreamReader(fileName);
        while (!inp_stm.EndOfStream)
        {
            string inp_ln = inp_stm.ReadLine();
            string[] coords = inp_ln.Split(' ');
            for (int i = 0; i < coords.Length; i++)
            {
                contentCoord.Add(coords[i]);
            }
        }
        inp_stm.Close();
        if (!checkCoord())
            return (false);
        return (true);
    }

    bool checkCoord()
    {
        foreach (string coord in contentCoord)
        {
            string[] infos = coord.Split(',');
            if (infos.Length != 3)
                return (false);
            infos[0] = infos[0].Replace('(', ' ');
            int nb = parseInt(infos[0]);
            if (nb == -1)
                return (false);
            dataX.Add(nb);

            nb = parseInt(infos[1]);
            if (nb == -1)
                return (false);
            dataY.Add(nb);

            infos[2] = infos[2].Replace(')', ' ');
            nb = parseInt(infos[2]);
            if (nb == -1)
                return (false);
            dataZ.Add(nb);
        }
        return (true);
    }

    int parseInt(string nb)
    {
        int number;
        if (int.TryParse(nb, out number))
            return (number);
        return (-1);
    }
}