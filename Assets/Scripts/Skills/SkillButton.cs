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
        public PlayerSkills.SkillType Type;
        public event Action<PlayerSkills.SkillType> IsSelected;


        private Transform _transform;
        private SelectedSkill _selectedSkill;

        public SkillButton(Transform transform, SkillModel model)
        {
            _transform = transform;
            Type = model.Type;
            _selectedSkill = new SelectedSkill();

            _transform.Find("Button").GetComponentInChildren<TMP_Text>().text = model.Type.ToString();
            _selectedSkill.selectedImg = _transform.Find("SelectedImg").GetComponent<Image>();
            _selectedSkill.selectedTMP = _selectedSkill.selectedImg.GetComponentInChildren<TMP_Text>();
            OffSelected();

            var btn = _transform.GetComponentInChildren<Button>();
            if (btn != null)
            {
                btn.onClick.AddListener(TaskOnClick);
            }
            else
            {
                _transform.AddComponent<Button>().onClick.AddListener(TaskOnClick);
            }

            void TaskOnClick()
            {
                Debug.Log(Type);
                IsSelected?.Invoke(Type);
            }
        }

        public void OnSelected(int score)
        {
            _selectedSkill.selectedImg.enabled = true;
            _selectedSkill.selectedTMP.enabled = true;
            _selectedSkill.selectedTMP.text = score.ToString();
        }

        public void OffSelected()
        {
            _selectedSkill.selectedImg.enabled = false;
            _selectedSkill.selectedTMP.enabled = false;
        }

        public void DrawLine(Transform endLine, Transform rootLine, GameObject preset)
        {
            var go = GameObject.Instantiate(preset, rootLine);

            var rectTransform = go.gameObject.GetComponent<RectTransform>();
            
            var disButtons = Vector3.Distance(_transform.position, endLine.position);
            
            rectTransform.sizeDelta = new Vector2(10, disButtons); 
            rectTransform.position =  Vector3.Lerp(_transform.position, endLine.position, 0.5f);
            var angle = Vector3.Angle(endLine.position - _transform.position, Vector3.up);
            angle = Mathf.Sign(Vector3.Cross(endLine.position - _transform.position, Vector3.up).z) * -angle;
            rectTransform.rotation = Quaternion.Euler(0,0,angle);
        }

        public Transform GetTransform()
        {
            return _transform;
        }

        private class SelectedSkill
        {
            public Image selectedImg;
            public TMP_Text selectedTMP;
        }
    }
}