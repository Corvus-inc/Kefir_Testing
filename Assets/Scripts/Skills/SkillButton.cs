using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.HID;
using UnityEngine.UI;

namespace Skills
{
    public class SkillButton
    {
        public event Action<PlayerSkills.SkillType> IsSelected;
        
        public PlayerSkills.SkillType Type { get; }
        public Transform TransformSkill { get; }

        private readonly SelectedSkill _selectedSkill;

        public SkillButton(Transform transformSkill, SkillModel model)
        {
            TransformSkill = transformSkill;
            Type = model.Type;
            _selectedSkill = new SelectedSkill();

            TransformSkill.Find("Button").GetComponentInChildren<TMP_Text>().text = model.Type.ToString();
            _selectedSkill.SelectedImg = TransformSkill.Find("SelectedImg").GetComponent<Image>();
            _selectedSkill.SelectedTMP = _selectedSkill.SelectedImg.GetComponentInChildren<TMP_Text>();
            OffSelected();

            var btn = TransformSkill.GetComponentInChildren<Button>();
            if (btn != null)
            {
                btn.onClick.AddListener(TaskOnClick);
            }
            else
            {
                TransformSkill.AddComponent<Button>().onClick.AddListener(TaskOnClick);
            }

            void TaskOnClick()
            {
                Debug.Log(Type);
                IsSelected?.Invoke(Type);
            }
        }

        public void OnSelected(int score)
        {
            _selectedSkill.SelectedImg.enabled = true;
            _selectedSkill.SelectedTMP.enabled = true;
            _selectedSkill.SelectedTMP.text = score.ToString();
        }

        public void OffSelected()
        {
            _selectedSkill.SelectedImg.enabled = false;
            _selectedSkill.SelectedTMP.enabled = false;
        }

        public void DrawLine(Transform endLine, Transform rootLine, GameObject preset)
        {
            var line = GameObject.Instantiate(preset, rootLine);
            var rectTransform = line.gameObject.GetComponent<RectTransform>();
            var disButtons = Vector3.Distance(TransformSkill.position, endLine.position);
            
            //set position and rotation for GameObject on direction
            rectTransform.sizeDelta = new Vector2(10, disButtons); 
            rectTransform.position =  Vector3.Lerp(TransformSkill.position, endLine.position, 0.5f);
            var angle = Vector3.Angle(endLine.position - TransformSkill.position, Vector3.up);
            angle = Mathf.Sign(Vector3.Cross(endLine.position - TransformSkill.position, Vector3.up).z) * -angle;
            rectTransform.rotation = Quaternion.Euler(0,0,angle);
        }
        
        private class SelectedSkill
        {
            public Image SelectedImg { get; set; }
            public TMP_Text SelectedTMP{ get; set; }
        }
    }
}