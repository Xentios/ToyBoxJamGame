using UnityEngine;
using System;
using System.Collections.Generic;

public class RequirementsChecker : MonoBehaviour
{
    private void Start()
    {
        // Get all the game objects in the scene
        GameObject[] gameObjects = FindObjectsOfType<GameObject>();

        // Create a list to store any missing required components
        List<string> missingComponents = new();

        // Check each game object for missing required components
        foreach (GameObject go in gameObjects)
        {
            Component[] components = go.GetComponents<Component>();
            foreach (Component component in components)
            {
                Type componentType = component.GetType();
                RequireComponent[] requireComponents = (RequireComponent[]) componentType.GetCustomAttributes(typeof(RequireComponent), true);
                foreach (RequireComponent requireComponent in requireComponents)
                {
                    Type[] requiredTypes = new Type[] { requireComponent.m_Type0, requireComponent.m_Type1, requireComponent.m_Type2 };
                    foreach (Type requiredType in requiredTypes)
                    {
                        if (requiredType != null && !go.GetComponent(requiredType))
                        {
                            string message = string.Format("GameObject '{0}' is missing required component '{1}'", go.name, requiredType.Name);
                            if (!missingComponents.Contains(message))
                            {
                                missingComponents.Add(message);
                            }
                        }
                    }
                }
            }
        }

        // Print any missing required components to the console
        if (missingComponents.Count > 0)
        {
            Debug.LogError("The following required components are missing in the scene:");
            
            foreach (string message in missingComponents)
            {
                Debug.LogError(message);
            }
        }
        else
        {
            Debug.Log("All required components are present in the scene.");
        }
    }
}
