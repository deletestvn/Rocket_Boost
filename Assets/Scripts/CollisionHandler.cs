
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float levelLoadDelay = 1f;
    void OnCollisionEnter(Collision other)
    {
        switch (other.gameObject.tag)
        {
            case "Respawn":
                Debug.Log("Rocket respawned!");
                break;
            case "Finish":
                ProcessSuccessSequence();
                break;
            default:
                ProcessCrashSequence();
                break;
        }
    }

    void ProcessSuccessSequence()
    {
        // todo add SFX upon collision
        // todo add particle effect upon collision
        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel", levelLoadDelay);
    }

    void ProcessCrashSequence()
    {
        // todo add SFX upon collision
        // todo add particle effect upon collision
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", levelLoadDelay);
    }

    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int maxSceneIndex = SceneManager.sceneCountInBuildSettings - 1;
        int nextSceneIndex = currentSceneIndex < maxSceneIndex ? currentSceneIndex + 1 : 0;
        SceneManager.LoadScene(nextSceneIndex);
    }
}
