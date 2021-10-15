
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float levelLoadDelay = 1f;
    [SerializeField] AudioClip crashSFX;
    [SerializeField] AudioClip successSFX;

    AudioSource audioSource;

    bool isTransitioning = false;
    
    void Start()
    {
        audioSource = GetComponent<AudioSource>();    
    }

    void OnCollisionEnter(Collision other)
    {
        if (isTransitioning) return;
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
        isTransitioning = true;
        audioSource.PlayOneShot(successSFX);

        // todo add particle effect upon collision
        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel", levelLoadDelay);
    }

    void ProcessCrashSequence()
    {
        isTransitioning = true;
        audioSource.PlayOneShot(crashSFX);
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
