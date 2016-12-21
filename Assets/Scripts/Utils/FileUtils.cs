using UnityEngine;
using System.IO;

public class FileUtils
{
    public static string readFile(TextAsset textAsset)
    {
        return textAsset.text;
    }

    public static void writeFile(string fileName, string data)
    {
        StreamWriter fileWriter = File.CreateText(fileName);

        fileWriter.Write(data);
        fileWriter.Close();
    }
}