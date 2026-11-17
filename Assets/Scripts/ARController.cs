using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Vuforia
{
    public class ARController : MonoBehaviour
    {

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {

        }
        public void Detect(string name)
        {
            switch (name)
            {
                case "anemia":
                    break;

                case "otak":
                    break;

                case "jantung":
                    break;

                case "varises":
                    break;

                default:
                    break;
            }
        }

        public void NotDetect()
        {

        }
    }
}