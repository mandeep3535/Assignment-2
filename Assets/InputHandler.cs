using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InputHandler : MonoBehaviour
{
    public colorChanging colorChanging;
    public DataLogger dataLogger;
    public Camera _mainCamera;
    // Start is called before the first frame update
    public GameObject[] targets; // Array of targets
    private int currentTargetIndex = 0; // Index of the current target
    private void Awake()
    {
        _mainCamera = Camera.main;

        // Debug log to check number of assigned targets
        if (targets == null || targets.Length == 0)
        {
            Debug.LogError("Targets array is not assigned or empty. Please assign target objects in the inspector.");
        }
        else
        {
            Debug.Log($"Number of targets assigned: {targets.Length}");
            SetInitialTarget();
        }

    }



    private void Update()
    {
        dataLogger.time += Time.deltaTime;
    }
    public void OnClick(InputAction.CallbackContext context)
    {
        if (!context.started)
        {
            return;
        }

        if (targets.Length == 0)
        {
            Debug.LogError("Targets array is empty. Please assign target objects in the inspector.");
            return;
        }
        var rayHit = Physics2D.GetRayIntersection(_mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue()));
        if (!rayHit.collider)
        {
            Debug.Log("No collider hit");
            GameObject background = GameObject.Find("background"); // Assuming the background object is named "Background"
            if (background != null)
            {
                colorChanging.changeColor(background);
            }
            else
            {
                Debug.LogWarning("Background object not found");
            }
        }

        else
        {
            if (rayHit.collider.gameObject == targets[currentTargetIndex])
            {
                dataLogger.Finaltime = dataLogger.time;
            dataLogger.time = 0;
            colorChanging.changeColor(rayHit.collider.gameObject);

            Debug.Log(rayHit.collider.gameObject.name);
                if (currentTargetIndex == targets.Length - 1)
                {
                    // Handle the transition to the next scene
                    //SceneManager.LoadScene("SampleScene 1");
                    // SceneManager.LoadScene("SampleScene 2");
                    // SceneManager.LoadScene("SampleScene 3");
                    // SceneManager.LoadScene("SampleScene 4");// Replace with the name of the next scene
                    LoadNextScene();
                }

                // Move to the next target
                currentTargetIndex = (currentTargetIndex + 1) % targets.Length;
                SetNextTarget();
            }
            else
            {
                Debug.Log("Wrong target selected");
                // Handle incorrect selection logic if needed
            }
        }


    }
    void SetNextTarget()
    {
        // Reset color for all targets
        foreach (var target in targets)
        {
            colorChanging.resetColor(target);
        }

        // Check if targets array is not empty
        if (targets.Length > 0)
        {
            // Color the next target
            colorChanging.colorNextTarget(targets[currentTargetIndex]);
        }
        else
        {
            Debug.LogError("Targets array is empty. Cannot set the next target.");
        }
    }
    void SetInitialTarget()
    {
        if (targets.Length > 0)
        {
            // Color the first target initially
            colorChanging.colorNextTarget(targets[currentTargetIndex]);
        }
        else
        {
            Debug.LogError("Targets array is empty. Cannot set the initial target.");
        }
    
    }
    void LoadNextScene()
    {
        // Assuming you want to load a scene based on index
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

        // You may want to wrap around if you go past the last scene
        if (nextSceneIndex >= SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0; // Wrap around to the first scene
        }

        SceneManager.LoadScene(nextSceneIndex);
    }
}

