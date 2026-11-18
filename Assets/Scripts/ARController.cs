using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Vuforia
{
    public class ARController : MonoBehaviour
    {
        public bool Detected;
        public bool DetectDetail;
        public GameObject buttonInfo;
        public int IndexDetectMarker;

        public GameObject[] Object;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            buttonInfo = GameManager.instance.mainMenu.buttonInfo;
            buttonInfo.SetActive(false);
            DetectDetail = false;

            NotDetect();
        }

        // Update is called once per frame
        void Update()
        {
            //if (DetectDetail == false)
            //{
            //    GameManager.instance.IndexDetailObject = -1;
            //}
        }

        public void Detect(string name)
        {
            print(name);
            Detected = true;
            DetectDetail = true;
            GameManager.instance.mainMenu.UI[14].SetActive(false);
            GameManager.instance.mainMenu.buttonInfo.SetActive(true);
            switch (name)
            {
                case "anemia":
                    IndexDetectMarker = 0;
                    Disable();
                    Object[0].SetActive(true);
                    break;

                case "jantung":
                    IndexDetectMarker = 1;
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

                case "otak":
                    IndexDetectMarker = 2;
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

                case "varises":
                    IndexDetectMarker = 3;
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

                default:
                    break;
            }
            GameManager.instance.IndexDetectMarker = IndexDetectMarker;
        }

        public void NotDetect()
        {
            Disable();
            DetectDetail = false;
            GameManager.instance.IndexDetailObject = -1;
            GameManager.instance.mainMenu.buttonInfo.SetActive(false);
            GameManager.instance.mainMenu.UI[8].SetActive(true);
            GameManager.instance.mainMenu.UI[9].SetActive(false);
            GameManager.instance.mainMenu.UI[10].SetActive(false);
            GameManager.instance.mainMenu.UI[11].SetActive(false);
            GameManager.instance.mainMenu.UI[12].SetActive(false);
            GameManager.instance.mainMenu.UI[13].SetActive(false);
            GameManager.instance.mainMenu.UI[14].SetActive(true);
        }

        public void Disable()
        {
            for (int i = 0; i < Object.Length; i++)
            {
                Object[i].SetActive(false);
            }
        }
    }
}