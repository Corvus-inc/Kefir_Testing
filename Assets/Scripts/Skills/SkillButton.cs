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
        private Transform _transform;
        public PlayerSkills.SkillType _type;
        public event Action<PlayerSkills.SkillType> IsSelected;

        public SkillButton(Transform transform, SkillModel model)
        {
            _transform = transform;
            _type = model.Type;

            _transform.GetComponentInChildren<TMP_Text>().text = model.Type.ToString();
            var btn =  _transform.GetComponentInChildren<Button>();
            if (btn != null)
            {
                
                btn.onClick.AddListener(TaskOnClick);
            }
            else
            {
                _transform.AddComponent<Button>().onClick.AddListener(TaskOnClick);
            }


            void TaskOnClick(){
                Debug.Log (_type);
                IsSelected?.Invoke(_type);
            }
        }
    }
}