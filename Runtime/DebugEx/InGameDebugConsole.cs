﻿using System;
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

        const float SPACE = 10f;
        const float BOX_HEIGHT = 300;
        const float LABEL_HEIGHT = 50;
        const float ENTER_BTN_WIDTH = 240f;

        const int MAX_LOGS = 30;

        const string FORMAT = "<color={0}>{1}</color>";


        public void Log(ELogType type, string log)
        {
            _strBuilder.Clear();

            _strBuilder.AppendFormat(FORMAT,
                LOG_COLORS[(int)type],
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

            float x = 0;
            float y = 0;

#if UNITY_IOS
            x = Screen.width > Screen.height ? Screen.safeArea.x : SPACE;
            y = Screen.width < Screen.height ? Screen.safeArea.y : 0;
            
#else
            x = Screen.width > Screen.height ? SPACE * 15 : SPACE * 2;
            y = Screen.width < Screen.height ? SPACE * 15 : 0;
#endif

            // logs
            GUI.Box(new Rect(x, y, Screen.width-x-x, BOX_HEIGHT), "");

            Rect viewport = new Rect(x, y, Screen.width*1.5f, LABEL_HEIGHT * _logs.Count);

            _scroll = GUI.BeginScrollView(new Rect(x, y+5, Screen.width-x-x, BOX_HEIGHT), _scroll, viewport);

            int i = 0;
            foreach(var log in _logs)
            {
                Rect rect = new Rect(x, LABEL_HEIGHT * i + y, viewport.width, LABEL_HEIGHT);
                GUI.Label(rect, log);
                
                i++;
            }

            GUI.EndScrollView();

            y += (BOX_HEIGHT + SPACE);

            // input
            _userInput = GUI.TextField(new Rect(x, y + 5f, Screen.width - x - x - SPACE - ENTER_BTN_WIDTH, 50f), _userInput);

            if (GUI.Button(new Rect(Screen.width - x - ENTER_BTN_WIDTH, y, ENTER_BTN_WIDTH, 60), "ENTER"))
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
