using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;

namespace Skills
{
    public class PlayerSkills
    {
        public enum SkillType
        {
            None,
            Move,
            Run,
            Jump,
        }
        
        #region PreinstalSkills
        
        public List<SkillModel> _listModels = new List<SkillModel>()
        { 
            new SkillModel(){Type = SkillType.Move, Name = "Move", Id = 2, RequiredCost = 1},
            new SkillModel(){Type = SkillType.None, Name = "None", Id = 2, RequiredCost = 1},
            new SkillModel(){Type = SkillType.Run, Name = "Run", Id = 0, RequiredCost = 1},
            new SkillModel(){Type = SkillType.Jump, Name = "Jump", Id = 1, RequiredCost = 1}
        };
        
        #endregion
        
        public event Action<SkillType> ChangeSkill;


        private List<SkillType> _unlockedSkillTypeList;

        public PlayerSkills()
        {
            _unlockedSkillTypeList = new List<SkillType>();
        }

        public bool TryUnlockSkill(SkillType skillType)
        {
            UnlockSkill(skillType);

            return IsSkillUnlocked(skillType);
        }

        public void LockSkill(SkillType skillType)
        {
            _unlockedSkillTypeList.Remove(skillType);
        }

        public void LockAllSkill()
        {
            _unlockedSkillTypeList.Clear();
        }
        
        private void UnlockSkill(SkillType skillType)
        {
            if (!IsSkillUnlocked(skillType))
            {
                _unlockedSkillTypeList.Add(skillType);
                ChangeSkill?.Invoke(skillType);
            }
        }

        public bool IsSkillUnlocked(SkillType skillType)
        {
            return _unlockedSkillTypeList.Contains(skillType);
        }
    }
    /*
     * SkillView - кнопка с подпиской на ивент инвок
     * SkillModel - Айдишники имя тип
     * SkillTreeController - реакция на инвоки от вью, 
     * PanelButtonsView - остальные кнопки
     * 
     */
}