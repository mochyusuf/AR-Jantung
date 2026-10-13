using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public ARController aRController;
    public ARMarker[] aRMarker;
    public bool Detect;
    public bool DetectDetail;
    public GameObject buttonInfo;
    public int IndexDetectMarker;

    public GameObject[] Object;

    // Use this for initialization
    private void Start()
    {
        buttonInfo = GameManager.instance.mainMenu.buttonInfo;
        buttonInfo.SetActive(false);
        DetectDetail = false;
    }

    // Update is called once per frame
    private void Update()
    {
        aRMarker = aRController.gameObject.GetComponents<ARMarker>();
        Detect = false;

        for (int i = 0; i < aRMarker.Length; i++)
        {
            var temp_marker = Resources.Load(aRMarker[i].NFTDataName) as Texture2D;
            print(aRMarker[i].NFTDataName + "!" + aRMarker[i].Visible);
            if (aRMarker[i].Visible)
            {
                print("Detect");
                IndexDetectMarker = i;
                GameManager.instance.IndexDetectMarker = IndexDetectMarker;
                Detect = true;
                GameManager.instance.mainMenu.buttonInfo.SetActive(true);
            }
        }
        if (Detect)
        {
            DetectDetail = true;
            GameManager.instance.mainMenu.UI[14].SetActive(false);
            switch (IndexDetectMarker)
            {
                case 0:
                    Disable();
                    Object[0].SetActive(true);
                    break;

                case 1:
                    switch (GameManager.instance.IndexDetailObject)
                    {
                        case -1:
                            Disable();
                            Object[1].SetActive(true);
                            if (!GameManager.instance.mainMenu.UI[9].activeSelf)
                            {
                                GameManager.instance.mainMenu.UI[10].SetActive(true);
                                GameManager.instance.mainMenu.UI[11].SetActive(false);
                                GameManager.instance.mainMenu.UI[12].SetActive(false);
                                GameManager.instance.mainMenu.UI[13].SetActive(false);
                            }
                            else
                            {
                                GameManager.instance.mainMenu.UI[10].SetActive(false);
                                GameManager.instance.mainMenu.UI[11].SetActive(false);
                                GameManager.instance.mainMenu.UI[12].SetActive(false);
                                GameManager.instance.mainMenu.UI[13].SetActive(false);
                            }
                            break;

                        case 0:
                            Disable();
                            Object[4].SetActive(true);
                            if (!GameManager.instance.mainMenu.UI[9].activeSelf)
                            {
                                GameManager.instance.mainMenu.UI[10].SetActive(false);
                                GameManager.instance.mainMenu.UI[11].SetActive(false);
                                GameManager.instance.mainMenu.UI[12].SetActive(false);
                                GameManager.instance.mainMenu.UI[13].SetActive(true);
                            }
                            else
                            {
                                GameManager.instance.mainMenu.UI[10].SetActive(false);
                                GameManager.instance.mainMenu.UI[11].SetActive(false);
                                GameManager.instance.mainMenu.UI[12].SetActive(false);
                                GameManager.instance.mainMenu.UI[13].SetActive(false);
                            }
                            break;
                    }
                    break;

                case 2:
                    switch (GameManager.instance.IndexDetailObject)
                    {
                        case -1:
                            Disable();
                            Object[2].SetActive(true);
                            if (!GameManager.instance.mainMenu.UI[9].activeSelf)
                            {
                                GameManager.instance.mainMenu.UI[10].SetActive(false);
                                GameManager.instance.mainMenu.UI[11].SetActive(true);
                                GameManager.instance.mainMenu.UI[12].SetActive(false);
                                GameManager.instance.mainMenu.UI[13].SetActive(false);
                            }
                            else
                            {
                                GameManager.instance.mainMenu.UI[10].SetActive(false);
                                GameManager.instance.mainMenu.UI[11].SetActive(false);
                                GameManager.instance.mainMenu.UI[12].SetActive(false);
                                GameManager.instance.mainMenu.UI[13].SetActive(false);
                            }
                            break;

                        case 0:
                            Disable();
                            Object[5].SetActive(true);
                            if (!GameManager.instance.mainMenu.UI[9].activeSelf)
                            {
                                GameManager.instance.mainMenu.UI[10].SetActive(false);
                                GameManager.instance.mainMenu.UI[11].SetActive(false);
                                GameManager.instance.mainMenu.UI[12].SetActive(false);
                                GameManager.instance.mainMenu.UI[13].SetActive(true);
                            }
                            else
                            {
                                GameManager.instance.mainMenu.UI[10].SetActive(false);
                                GameManager.instance.mainMenu.UI[11].SetActive(false);
                                GameManager.instance.mainMenu.UI[12].SetActive(false);
                                GameManager.instance.mainMenu.UI[13].SetActive(false);
                            }
                            break;

                        case 1:
                            Disable();
                            Object[6].SetActive(true);
                            if (!GameManager.instance.mainMenu.UI[9].activeSelf)
                            {
                                GameManager.instance.mainMenu.UI[10].SetActive(false);
                                GameManager.instance.mainMenu.UI[11].SetActive(false);
                                GameManager.instance.mainMenu.UI[12].SetActive(false);
                                GameManager.instance.mainMenu.UI[13].SetActive(true);
                            }
                            else
                            {
                                GameManager.instance.mainMenu.UI[10].SetActive(false);
                                GameManager.instance.mainMenu.UI[11].SetActive(false);
                                GameManager.instance.mainMenu.UI[12].SetActive(false);
                                GameManager.instance.mainMenu.UI[13].SetActive(false);
                            }
                            break;
                    }
                    break;

                case 3:
                    switch (GameManager.instance.IndexDetailObject)
                    {
                        case -1:
                            Disable();
                            Object[3].SetActive(true);
                            if (!GameManager.instance.mainMenu.UI[9].activeSelf)
                            {
                                GameManager.instance.mainMenu.UI[10].SetActive(false);
                                GameManager.instance.mainMenu.UI[11].SetActive(false);
                                GameManager.instance.mainMenu.UI[12].SetActive(true);
                                GameManager.instance.mainMenu.UI[13].SetActive(false);
                            }
                            else
                            {
                                GameManager.instance.mainMenu.UI[10].SetActive(false);
                                GameManager.instance.mainMenu.UI[11].SetActive(false);
                                GameManager.instance.mainMenu.UI[12].SetActive(false);
                                GameManager.instance.mainMenu.UI[13].SetActive(false);
                            }
                            break;

                        case 0:
                            Disable();
                            Object[7].SetActive(true);
                            if (!GameManager.instance.mainMenu.UI[9].activeSelf)
                            {
                                GameManager.instance.mainMenu.UI[10].SetActive(false);
                                GameManager.instance.mainMenu.UI[11].SetActive(false);
                                GameManager.instance.mainMenu.UI[12].SetActive(false);
                                GameManager.instance.mainMenu.UI[13].SetActive(true);
                            }
                            else
                            {
                                GameManager.instance.mainMenu.UI[10].SetActive(false);
                                GameManager.instance.mainMenu.UI[11].SetActive(false);
                                GameManager.instance.mainMenu.UI[12].SetActive(false);
                                GameManager.instance.mainMenu.UI[13].SetActive(false);
                            }
                            break;

                        case 1:
                            Disable();
                            Object[8].SetActive(true);
                            if (!GameManager.instance.mainMenu.UI[9].activeSelf)
                            {
                                GameManager.instance.mainMenu.UI[10].SetActive(false);
                                GameManager.instance.mainMenu.UI[11].SetActive(false);
                                GameManager.instance.mainMenu.UI[12].SetActive(false);
                                GameManager.instance.mainMenu.UI[13].SetActive(true);
                            }
                            else
                            {
                                GameManager.instance.mainMenu.UI[10].SetActive(false);
                                GameManager.instance.mainMenu.UI[11].SetActive(false);
                                GameManager.instance.mainMenu.UI[12].SetActive(false);
                                GameManager.instance.mainMenu.UI[13].SetActive(false);
                            }
                            break;
                    }
                    break;
            }
        }
        else
        {
            DetectDetail = false;
            GameManager.instance.mainMenu.buttonInfo.SetActive(false);
            GameManager.instance.mainMenu.UI[8].SetActive(true);
            GameManager.instance.mainMenu.UI[9].SetActive(false);
            GameManager.instance.mainMenu.UI[10].SetActive(false);
            GameManager.instance.mainMenu.UI[11].SetActive(false);
            GameManager.instance.mainMenu.UI[12].SetActive(false);
            GameManager.instance.mainMenu.UI[13].SetActive(false);
            GameManager.instance.mainMenu.UI[14].SetActive(true);
        }
        if (DetectDetail == false)
        {
            GameManager.instance.IndexDetailObject = -1;
        }
    }

    public void Disable()
    {
        for (int i = 0; i < Object.Length; i++)
        {
            Object[i].SetActive(false);
        }
    }
}