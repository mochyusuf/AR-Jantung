using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public string ActiveScene;

    public static GameManager instance = null;
    public MainMenu mainMenu;
    public int IndexDetectMarker;
    public int IndexDetailObject;
    public Controller controller;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    // Use this for initialization
    private void Start()
    {
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnLevelFinishedLoading;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnLevelFinishedLoading;
    }

    private void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        Time.timeScale = 1;
        ActiveScene = scene.name;
		if (ActiveScene == "AR") {
			mainMenu.Disable ();
			mainMenu.UI [8].SetActive (true);
			controller = GameObject.FindWithTag ("Controller").GetComponent<Controller> ();
			IndexDetailObject = -1;
		} else if (ActiveScene == "MainMenu") {
			mainMenu.Disable ();
			mainMenu.UI [0].SetActive (true);

			mainMenu.index_petunjuk = 0;
			mainMenu.index_materi = 0;
			mainMenu.index_question = 0;
		} else {
			mainMenu.Disable ();
		}
    }

    // Update is called once per frame
    private void Update()
    {
    }
}