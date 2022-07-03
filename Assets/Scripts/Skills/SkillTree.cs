using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Skills
{
    public class SkillTree : MonoBehaviour
    {
        [SerializeField] private Transform[] transformPositions;
        [SerializeField] private Transform rootLines;
        [FormerlySerializedAs("prefab")] [SerializeField] private GameObject prefabLine;
        
        public List<SkillTreeButton> SkillButtonList { get; private set; }

        private PlayerSkills _playerSkills;

        public void SetPlayerSkills(PlayerSkills playerSkills)
        {
            _playerSkills = playerSkills;

            SkillButtonList = new List<SkillTreeButton>();

            for (int i = 0; i < _playerSkills._listModels.Count; i++)
            {
                var skill = new SkillTreeButton(transformPositions[i], playerSkills._listModels[i]);

                SkillButtonList.Add(skill);
            }
//todo 
            foreach (var skill in SkillButtonList)
            {
                var types = _playerSkills._listModels.Find(_ => _.Type == skill.Type).RequiredTypes;
                if (types != null)
                {
                    foreach (var type in types)
                    {
                        skill.DrawLine(SkillButtonList.Find(_=> _.Type == type).TransformSkill, rootLines, prefabLine);
                    }
                }
            }
        }
    }
}