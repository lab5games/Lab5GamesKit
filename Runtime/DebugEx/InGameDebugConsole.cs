using System;
using System.Text;
using System.Collections.Generic;
using UnityEngine;

namespace Lab5Games
{
    public class InGameDebugConsole : MonoBehaviour, ILogHandler
    {
        public static bool consoleEnabled = true;
        public static bool showConsole = false;

        private string _userInput;
        private GUISkin _guiSkin;
        private Vector2 _scroll;

        private StringBuilder _strBuilder = new StringBuilder();
        private Queue<string> _logs = new Queue<string>(MAX_LOGS);

        readonly string[] LOG_COLORS = new string[]
        {
            "#FF0066FF",    // Error
            "#FFFF66FF",    // Warning
            "#66FFFFFF",    // Trace
            "#FFFFCCFF"     // Log
        };

        const int MAX_LOGS = 30;

        const string FORMAT = "<color={0}>[{1}] {2}</color>";


        public void Log(ELogType type, string log)
        {
            _strBuilder.Clear();

            _strBuilder.AppendFormat(FORMAT,
                LOG_COLORS[(int)type],
                DateTime.Now.ToString("HH:mm:ss"),
                log);


            AddLog(_strBuilder.ToString());

#if UNITY_EDITOR
            UnityEngine.Debug.Log(_strBuilder.ToString());
#endif
        }


        private void AddLog(string log)
        {
            showConsole = true;

            if(_logs.Count >= MAX_LOGS)
            {
                _logs.Dequeue();
            }

            _logs.Enqueue(log);
        }

        private void OnReturn()
        {
            if (string.IsNullOrEmpty(_userInput))
                return;

            if (_userInput.Substring(0, 1) == "-")
            {
                OnCommand(_userInput.Remove(0, 1).ToLower());
            }

            _userInput = "";
        }

        private void OnCommand(string cmd)
        {
            switch (cmd)
            {
                case "clear": _logs.Clear(); break;
                case "close": showConsole = false; break;
                case "disable": consoleEnabled = false; break;
            }

        }

        private void DrawConsole()
        {
            GUI.skin = _guiSkin;

            float y = 0f;

            // logs
            GUI.Box(new Rect(0, y, Screen.width, 300), "");

            Rect viewport = new Rect(0, y, Screen.width - 30, 50 * _logs.Count);

            _scroll = GUI.BeginScrollView(new Rect(0, y + 5f, Screen.width, 280), _scroll, viewport);

            int i = 0;
            foreach (var log in _logs)
            {
                Rect rect = new Rect(5, 50 * i, viewport.width - 100, 50);
                GUI.Label(rect, log);

                i++;
            }

            GUI.EndScrollView();

            y += 310;

            // input
            _userInput = GUI.TextField(new Rect(5f, y + 5f, Screen.width - 270f, 50f), _userInput);

            if (GUI.Button(new Rect(Screen.width - 250, y, 240, 60), "ENTER"))
            {
                OnReturn();
            }
        }

        public static InGameDebugConsole CreateInstance()
        {
            
            InGameDebugConsole instance = null;

            instance = FindObjectOfType<InGameDebugConsole>();

            if(instance == null)
            {
                GameObject go = new GameObject("InGameDebugHandler");
                instance = go.AddComponent<InGameDebugConsole>();
            }

            return instance;
        }

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        private void Start()
        {
            _strBuilder = new StringBuilder();
            _logs = new Queue<string>(MAX_LOGS);
            _guiSkin = Resources.Load<GUISkin>("InGameDebugConsole");
        }

        private void OnGUI()
        {
            if(consoleEnabled && showConsole)
            {
                DrawConsole();
            }
        }
    }
}
