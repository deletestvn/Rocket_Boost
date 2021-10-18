
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float levelLoadDelay = 1f;
    [SerializeField] AudioClip crashSFX;
    [SerializeField] AudioClip successSFX;

    [SerializeField] ParticleSystem crashVFX;
    [SerializeField] ParticleSystem successVFX;

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
        // Trigger sound effect
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(successSFX);

        // Trigger visual effect
        successVFX.Play();

        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel", levelLoadDelay);
    }

    void ProcessCrashSequence()
    {
        // Trigger sound effect
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(crashSFX);

        // Trigger visual effect
        crashVFX.Play();

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
