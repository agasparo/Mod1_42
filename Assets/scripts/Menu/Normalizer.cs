using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Normalizer
{
    public static float[,] MapData = new float[101, 101];
    float multiplier = 25f;

    public void NormalizeData()
    {
        int max = Parser.dataX.Max();
        int min = Parser.dataX.Min();

        int maxy = Parser.dataY.Max();
        int miny = Parser.dataY.Min();

        int maxz = Parser.dataZ.Max();
        int minz = Parser.dataZ.Min();

        for (int i = 0; i < Parser.dataX.Count; i++)
        {
            Parser.dataX[i] = (int)Mathf.Round(Normalized(Parser.dataX[i], min, max) * multiplier + 25);
            float height = Mathf.Round(Normalized(Parser.dataY[i], miny, maxy) * 0.5f);
            Parser.dataZ[i] = (int)Mathf.Round(Normalized(Parser.dataZ[i], minz, maxz) * multiplier + 25);
            if (Parser.dataX[i] < 0)
                Parser.dataX[i] = 25;
            if (Parser.dataZ[i] < 0)
                Parser.dataZ[i] = 25;
            if (height < 0)
                height = .25f;
            MapData[Parser.dataX[i], Parser.dataZ[i]] = height;
        }
    }

    float Normalized(int value, int min, int max)
    {

        return ((float)((float)value - (float)min) / ((float)max - (float)min));
    }
}
