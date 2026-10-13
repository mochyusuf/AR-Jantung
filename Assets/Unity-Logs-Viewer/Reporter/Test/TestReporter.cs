#if UNITY_CHANGE1 || UNITY_CHANGE2 || UNITY_CHANGE3
#warning UNITY_CHANGE has been set manually
#elif UNITY_4_0 || UNITY_4_1 || UNITY_4_2 || UNITY_4_3 || UNITY_4_4 || UNITY_4_5 || UNITY_4_6 || UNITY_4_7
#define UNITY_CHANGE1
#elif UNITY_5_0 || UNITY_5_1 || UNITY_5_2
#define UNITY_CHANGE2
#else
#define UNITY_CHANGE3
#endif

using System.Threading;
using UnityEngine;

#if UNITY_CHANGE3

#endif

public class TestReporter : MonoBehaviour
{
    public int logTestCount = 100;
    public int threadLogTestCount = 100;
    public bool logEverySecond = true;
    private int currentLogTestCount;
    private Reporter reporter;
    private GUIStyle style;
    private Rect rect1;
    private Rect rect2;
    private Rect rect3;
    private Rect rect4;
    private Rect rect5;
    private Rect rect6;

    private Thread thread;

    private void Start()
    {
        Application.runInBackground = true;

        reporter = FindObjectOfType(typeof(Reporter)) as Reporter;
        reporter.show = false;
        style = new GUIStyle();
        style.alignment = TextAnchor.MiddleCenter;
        style.normal.textColor = Color.white;
        style.wordWrap = true;

        rect1 = new Rect(Screen.width / 2 - 120, Screen.height / 2 - 225, 240, 50);
        rect2 = new Rect(Screen.width / 2 - 120, Screen.height / 2 - 175, 240, 100);
        rect3 = new Rect(Screen.width / 2 - 120, Screen.height / 2 - 50, 240, 50);
        rect4 = new Rect(Screen.width / 2 - 120, Screen.height / 2, 240, 50);
        rect5 = new Rect(Screen.width / 2 - 120, Screen.height / 2 + 50, 240, 50);
        rect6 = new Rect(Screen.width / 2 - 120, Screen.height / 2 + 100, 240, 50);

        thread = new Thread(new ThreadStart(threadLogTest));
        thread.Start();
    }

    private void OnDestroy()
    {
        thread.Abort();
    }

    private void threadLogTest()
    {
    }

    private float elapsed;

    private void Update()
    {
        int drawn = 0;
    }

    private void OnGUI()
    {
        if (reporter && !reporter.show)
        {
        }
    }
}