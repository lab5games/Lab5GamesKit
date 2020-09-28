using UnityEngine;
using System;
using System.Collections.Generic;

namespace Lab5Games
{
    public class InGameDebugConsole : MonoBehaviour
    {
        private string _input;
        private GUISkin _guiSkin;

        private Vector2 _scroll;
        private Queue<string> _logQueue = new Queue<string>(MAX_LOGS);

        public bool showConsole { get; set; }

        const int MAX_LOGS = 30;

        public static void Log(string log)
        {
            instance.AddLog(log);
        }

        private void AddLog(string log)
        {
            showConsole = true;

            if(_logQueue.Count >= MAX_LOGS)
            {
                _logQueue.Dequeue();
            }

            _logQueue.Enqueue(log);
        }

        private void DrawConsole()
        {
            GUI.skin = _guiSkin;

            float y = 0f;

            // logs
            GUI.Box(new Rect(0, y, Screen.width, 300), "");

            Rect viewport = new Rect(0, y, Screen.width - 30, 50 * _logQueue.Count);

            _scroll = GUI.BeginScrollView(new Rect(0, y + 5f, Screen.width, 280), _scroll, viewport);

            int i = 0;
            foreach(var log in _logQueue)
            {
                Rect rect = new Rect(5, 50 * i, viewport.width - 100, 50);
                GUI.Label(rect, log);

                i++;
            }

            GUI.EndScrollView();

            y += 310;

            // input
            _input = GUI.TextField(new Rect(5f, y + 5f, Screen.width - 270f, 50f), _input);

            if(GUI.Button(new Rect(Screen.width - 250, y, 240, 60), "ENTER"))
            {
                OnReturn();
            }
        }

        private void OnReturn()
        {
            if (string.IsNullOrEmpty(_input))
                return;

            if(_input.Substring(0, 1) == "-")
            {
                OnCommand();   
            }

            _input = "";
        }

        private void OnCommand()
        {
            string cmd = _input.ToLower();

            if(cmd == "-clear")
            {
                _logQueue.Clear();
            }
            else if(cmd == "-close")
            {
                showConsole = false;
            }
        }

        private static InGameDebugConsole _instance = null;

        public static InGameDebugConsole instance
        {
            get
            {
                if(_instance == null)
                {
                    _instance = FindObjectOfType<InGameDebugConsole>();
                }

                if(_instance == null)
                {
                    GameObject go = new GameObject("InGameDebugConsole");
                    _instance = go.AddComponent<InGameDebugConsole>();
                }

                return _instance;
            }
        }


        private void Awake()
        {
            if (_instance == null)
                _instance = this;

            DontDestroyOnLoad(gameObject);
        }

        private void Start()
        {
            _guiSkin = Resources.Load<GUISkin>("InGameDebugConsole");
        }

        private void OnGUI()
        {
            if (showConsole)
            {
                DrawConsole();
            }
        }

        private void OnDestroy()
        {
            _instance = null;

            _logQueue = null;
        }
    }
}
