﻿using System.Collections.Generic;
using UnityEngine;

namespace Skills
{
    public class SkillTreeView : MonoBehaviour
    {
        [SerializeField] private Transform[] transformPositions;
        public List<SkillButton> SkillButtonList { get; set; }

        private PlayerSkills _playerSkills;

        public void SetPlayerSkills(PlayerSkills playerSkills)
        {
            _playerSkills = playerSkills;

            SkillButtonList = new List<SkillButton>();


            for (int i = 0; i < _playerSkills._listModels.Count; i++)
            {
                var skill = new SkillButton(transformPositions[i], playerSkills._listModels[i]);
                SkillButtonList.Add(skill);
            }
        }
    }
}