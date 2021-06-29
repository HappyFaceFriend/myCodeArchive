using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text.RegularExpressions;
using System.Text;
using System;

class FileUtils
{
    static string LINE_SPLIT = @"\r\n|\n\r|\n|\r";

    public static string[] SplitLines(string text)
    {
        return Regex.Split(text, LINE_SPLIT);
    }
    public static string ReadFile(string filePath)
    {
        string path = Application.dataPath + "/" + filePath;
#if UNITY_EDITOR
        if (!File.Exists(path)) Debug.LogWarning("Can't find file : " + path);
#endif
        string data = File.ReadAllText(path, System.Text.Encoding.UTF8);
        
        // utf-8 인코딩
        byte[] bytesForEncoding = Encoding.UTF8.GetBytes(data);
        string encodedString = Convert.ToBase64String(bytesForEncoding);

        // utf-8 디코딩
        byte[] decodedBytes = Convert.FromBase64String(encodedString);
        string decodedString = Encoding.UTF8.GetString(decodedBytes);
        return decodedString;
    }
}
