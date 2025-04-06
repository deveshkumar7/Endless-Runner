using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroyOnLoad : MonoBehaviour
{
    public string[] scenesToPersist; // Names of the scenes 

    void Start()
    {
        // Check if the current scene is in the list of scenes 
        bool sceneToPersist = false;
        foreach (string sceneName in scenesToPersist)
        {
            if (SceneManager.GetActiveScene().name == sceneName)
            {
                sceneToPersist = true;
                break;
            }
        }

        // If the current scene is not in the list of scenes , destroy the object
        if (!sceneToPersist)
        {
            Destroy(gameObject);
            return;
        }

        // Check if there are other instances of this script attached to GameObjects in the scene
        DontDestroyOnLoad[] otherInstances = Object.FindObjectsOfType<DontDestroyOnLoad>();
        foreach (DontDestroyOnLoad instance in otherInstances)
        {
            if (instance != this && instance.name == gameObject.name)
            {
                Destroy(gameObject);
                return;
            }
        }

        // Ensure the object persists across scene changes
        DontDestroyOnLoad(gameObject);
    }
}
