using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    private Player_Input _playerInput;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        _playerInput = new Player_Input();

        _playerInput.Player.Restart.performed += RestartLevel;
    }

    private void OnEnable()
    {
        _playerInput.Enable();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        _playerInput.Disable();
        SceneManager.sceneLoaded -= OnSceneLoaded;

    }

    public void RestartLevel(InputAction.CallbackContext context)
    {
        if(SceneManager.GetActiveScene().buildIndex == 1)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GameOver()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            if (PlayerPrefs.GetFloat("checkpointPosX") != 0)
            {
                GameObject.FindWithTag("Player").transform.position = new Vector2(PlayerPrefs.GetFloat("checkpointPosX"), PlayerPrefs.GetFloat("checkpointPosY"));
            }
        }
    }
}
