using UnityEditor;
using UnityEngine.SceneManagement;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace UnityToolbarExtender.Examples
{
	
	[InitializeOnLoad]
	public class SceneLockButtons
	{
		static SceneLockButtons()
		{
			ToolbarExtender.LeftToolbarGUI.Add(OnToolbarGUI);
		}

		static void OnToolbarGUI()
		{
			GUILayout.FlexibleSpace();

			if(GUILayout.Button(new GUIContent("Lock Scene", "Locks Current Open Scene"), ToolbarStyles.biggerButtonStyle))
			{
				SceneKeyMaker.LockScene();
			}

			if(GUILayout.Button(new GUIContent("UnLock Scene", "Unlocks Current Open Scene"), ToolbarStyles.biggerButtonStyle))
			{
				SceneKeyMaker.UnLockScene();
			}
		}
	}

	static class SceneKeyMaker
	{
		
		public static void LockScene()
		{
			var lockedSceneList = Resources.Load<LockedSceneList>("LockedSceneList");
			var currentScene =  EditorSceneManager.GetActiveScene();
			foreach (var sceneNames in lockedSceneList.LockedScenes)
			{
				if(sceneNames== currentScene.name)
                {
					Debug.LogError("The scene "+ currentScene + " was already locked");
					return;
                }
			}			
			Object activeSceneObject = AssetDatabase.LoadAssetAtPath<Object>(currentScene.path);
			SceneField temp=new SceneField(activeSceneObject, currentScene.name);

			lockedSceneList.LockedScenes.Add(temp);
			SetSave(currentScene);
			Debug.Log("LOCKED SCENE "+ currentScene.name);
		}		


		public static void UnLockScene()
        {			
			var lockedSceneList = Resources.Load<LockedSceneList>("LockedSceneList");
			var currentScene = EditorSceneManager.GetActiveScene();
			foreach (var sceneNames in lockedSceneList.LockedScenes)
			{
				if (sceneNames == currentScene.name)
				{
					lockedSceneList.LockedScenes.Remove(sceneNames);
					Debug.Log("UNLOCKED SCENE " + currentScene.name);
					SetSave(currentScene);
					return;
				}
			}
			
			Debug.LogError("The scene " + currentScene + " could not find in locked scene list");
		}


		//Some works in LOCK and some works in UNlock part.
		private static void SetSave(Scene scene)
		{
			Resources.UnloadUnusedAssets();
			//var temp = EditorSceneManager.GetSceneByName(scene.name);
            EditorSceneManager.MarkSceneDirty(scene);
            EditorSceneManager.SaveScene(scene);
            //EditorSceneManager.MarkAllScenesDirty();
            //EditorSceneManager.SaveOpenScenes();
            var lockedSceneList = Resources.Load<LockedSceneList>("LockedSceneList");
			EditorUtility.SetDirty(lockedSceneList);
		}
	}

	
}