

using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;
using System.IO;

public class PostBuildProcessor
{
    [PostProcessBuildAttribute(1)]
    public static void OnPostprocessBuild(BuildTarget target, string pathToBuiltProject)
    {
        //Debug.Log("Build path:"+pathToBuiltProject);

        //var folderPath = pathToBuiltProject.Replace("Zomwick.exe", "");
        //var x=Directory.GetDirectoryRoot(pathToBuiltProject);

        //var path = "Zomwick_Data/Scripts/Save_Load/SavedFiles/";
        //path= folderPath + path;
        //var folder = Directory.CreateDirectory(path);
        //Directory.Delete(folderPath + "Zomwick_BurstDebugInformation_DoNotShip",true);
    }
}
