using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.HID;
using UnityEngine.UI;

namespace Skills
{
    public class SkillTreePanelButtons : MonoBehaviour
    {
        [SerializeField] private Button learn;
        [SerializeField] private Button forget;
        [SerializeField] private Button addScore;
        [SerializeField] private Button forgetEverything;
        [SerializeField] private TMP_Text textScore;

        public event Action Learn;
        public event Action Forget;
        public event Action AddScore;
        public event Action ForgetEverything;

        private void Start()
        {
            learn.onClick.AddListener(() => { Learn?.Invoke(); });
            forget.onClick.AddListener(() => { Forget?.Invoke(); });
            addScore.onClick.AddListener(() => { AddScore?.Invoke(); });
            forgetEverything.onClick.AddListener(() => { ForgetEverything?.Invoke(); });
        }

        public void Learning(bool isLearn)
        {
            learn.enabled = isLearn;
            var imageColor = isLearn ? learn.image.color = Color.green : learn.image.color = Color.gray;
        }
        public void Forgetting(bool isForget)
        {
            forget.enabled = isForget;
            var imageColor = isForget ? forget.image.color = Color.yellow : forget.image.color = Color.gray;
        }

        public void UpdateScore(int score)
        {
            textScore.text = $"Score: {score}";
        }
    }
}