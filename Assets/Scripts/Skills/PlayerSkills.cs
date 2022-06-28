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
            new SkillModel() { Type = SkillType.Move, Name = "Move", RequiredCost = 1 },
            new SkillModel() { Type = SkillType.None, Name = "None", RequiredCost = 2 },
            new SkillModel()
            {
                Type = SkillType.Run, RequiredTypes = new SkillType[] { SkillType.Move }, Name = "Run", RequiredCost = 3
            },
            new SkillModel()
            {
                Type = SkillType.Jump, RequiredTypes = new SkillType[] { SkillType.Run }, Name = "Jump",
                RequiredCost = 4
            }
        };

        #endregion

        public event Action<SkillType> ChangeSkill;


        private List<SkillType> _unlockedSkillTypeList;
        
        //todo move to player class
        private int _score = 2;
        public int Score => _score;

        public void AddScore()
        {
            _score++;
        }

        public PlayerSkills()
        {
            _unlockedSkillTypeList = new List<SkillType>();
        }

        public bool IsSkillUnlocked(SkillType skillType)
        {
            return _unlockedSkillTypeList.Contains(skillType);
        }

        public bool TryUnlockSkill(SkillType skillType)
        {
            if (CanSkillUnlock(skillType))
            {
                UnlockSkill(skillType);
                return true;
            }

            return false;
        }

        public bool TryLockSkill(SkillType skillType)
        {
            if (CanSkillLocked(skillType))
            {
                LockSkill(skillType);
                return true;
            }

            return false;
        }

        public void LockAllSkill()
        {
            foreach (var skillType in _unlockedSkillTypeList)
            {
                var modelSkill = _listModels.First(_ => _.Type == skillType);
                _score += modelSkill.RequiredCost;

            }
            _unlockedSkillTypeList.Clear();
        }


        public bool CanSkillUnlock(SkillType skillType)
        {
            var modelSkill = _listModels.First(_ => _.Type == skillType);
            var requiredTypes = modelSkill.RequiredTypes;

            if (_score < modelSkill.RequiredCost)
            {
                return false;
            }

            if (requiredTypes != null)
            {
                foreach (var type in requiredTypes)
                {
                    if (IsSkillUnlocked(type))
                    {
                    }
                    else
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public bool CanSkillLocked(SkillType skillType)
        {
            foreach (var model in _listModels)
            {
                if (model.RequiredTypes != null)
                {
                    foreach (var type in model.RequiredTypes)
                    {
                        if (type == skillType)
                        {
                            if (IsSkillUnlocked(model.Type))
                            {
                                return false;
                            }
                        }
                    }
                }
            }

            if (IsSkillUnlocked(skillType))
            {
                return true;
            }
            
            return false;
        }

        private void LockSkill(SkillType skillType)
        {
            _score += _listModels.First(_ => _.Type == skillType).RequiredCost;
            _unlockedSkillTypeList.Remove(skillType);
        }

        private void UnlockSkill(SkillType skillType)
        {
            if (IsSkillUnlocked(skillType)) return;
            
            _score -= _listModels.First(_ => _.Type == skillType).RequiredCost;
            _unlockedSkillTypeList.Add(skillType);
            ChangeSkill?.Invoke(skillType);
        }
    }
}