using System;
using UnityEngine;
using UnityEngine.InputSystem.HID;
using UnityEngine.UI;

namespace Skills
{
    public class PanelButtonsView : MonoBehaviour
    {
        [SerializeField] private Button learn;
        [SerializeField] private Button forget;
        [SerializeField] private Button addScore;
        [SerializeField] private Button forgetEverything;

        public event Action Learn;
        public event Action Forget;
        public event Action AddScore;
        public event Action ForgetEverything;

        private void Start()
        {
            learn.enabled = false;
            
            learn.onClick.AddListener(() => { Learn?.Invoke(); });
            forget.onClick.AddListener(() => { Forget?.Invoke(); });
            addScore.onClick.AddListener(() => { AddScore?.Invoke(); });
            forgetEverything.onClick.AddListener(() => { ForgetEverything?.Invoke(); });
        }

        public void Learning(bool isLearn)
        {
            learn.enabled = isLearn;
        }
    }
}