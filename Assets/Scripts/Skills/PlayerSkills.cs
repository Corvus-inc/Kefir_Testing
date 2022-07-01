using System;
using System.Collections.Generic;
using System.Linq;

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
            LieDown,
            SitDown,
            Sleep,
            Ready,
            Salsa,
            Wave,
            HipHop
        }

        #region PreinstalSkills

        public List<SkillModel> _listModels = new List<SkillModel>()
        {
            new SkillModel()
            {
                Type = SkillType.None, RequiredTypes = new SkillType[] { SkillType.Move }, Name = "None",
                RequiredCost = 20
            },

            new SkillModel() { Type = SkillType.Move, Name = "Move", RequiredCost = 0 },
            new SkillModel()
            {
                Type = SkillType.Run, RequiredTypes = new SkillType[] { SkillType.Move }, Name = "Run", RequiredCost = 3
            },
            new SkillModel()
            {
                Type = SkillType.Jump, RequiredTypes = new SkillType[] { SkillType.Run }, Name = "Jump",
                RequiredCost = 4
            },

            new SkillModel()
            {
                Type = SkillType.LieDown, RequiredTypes = new SkillType[] { SkillType.Move }, Name = "LieDown",
                RequiredCost = 2
            },
            new SkillModel()
            {
                Type = SkillType.SitDown, RequiredTypes = new SkillType[] { SkillType.Move }, Name = "Sit",
                RequiredCost = 2
            },
            new SkillModel()
            {
                Type = SkillType.Sleep, RequiredTypes = new SkillType[] { SkillType.SitDown, SkillType.LieDown },
                Name = "Sleep",
                RequiredCost = 4
            },

            new SkillModel()
            {
                Type = SkillType.Ready, RequiredTypes = new SkillType[] { SkillType.Move }, Name = "Ready",
                RequiredCost = 1
            },
            new SkillModel()
            {
                Type = SkillType.Salsa, RequiredTypes = new SkillType[] { SkillType.Ready }, Name = "Salsa",
                RequiredCost = 2
            },
            new SkillModel()
            {
                Type = SkillType.Wave, RequiredTypes = new SkillType[] { SkillType.Ready }, Name = "Wave",
                RequiredCost = 2
            },
            new SkillModel()
            {
                Type = SkillType.HipHop, RequiredTypes = new SkillType[] { SkillType.Salsa, SkillType.Wave },
                Name = "HipHop",
                RequiredCost = 4
            },
        };

        #endregion


        private List<SkillType> _unlockedSkillTypeList;
        private Dictionary<SkillType, SkillModel> _dictionaryModels;

        //todo move to player class
        public int Score { get; private set; } = 2;

        public void AddScore()
        {
            Score++;
        }

        public PlayerSkills()
        {
            _unlockedSkillTypeList = new List<SkillType>();
            _dictionaryModels = new Dictionary<SkillType, SkillModel>();

            foreach (var model in _listModels)
            {
                _dictionaryModels?.Add(model.Type, model);
            }
        }

        public bool IsSkillUnlocked(SkillType skillType)
        {
            return _unlockedSkillTypeList.Contains(skillType);
        }

        public SkillModel GetModel(SkillType skillType)
        {
            return _dictionaryModels[skillType];
        }

        public bool TryUnlockSkill(SkillType skillType)
        {
            if (!CanSkillUnlock(skillType)) return false;
            
            UnlockSkill(skillType);
            return true;

        }

        public bool TryLockSkill(SkillType skillType)
        {
            if (!CanSkillLocked(skillType)) return false;
            
            LockSkill(skillType);
            return true;

        }

        public void LockAllSkill()
        {
            foreach (var skillType in _unlockedSkillTypeList)
            {
                var modelSkill = GetModel(skillType);
                Score += modelSkill.RequiredCost;
            }

            _unlockedSkillTypeList.Clear();
        }


        public bool CanSkillUnlock(SkillType skillType)
        {
            var modelSkill = GetModel(skillType);
            var requiredTypes = modelSkill.RequiredTypes;

            if (Score < modelSkill.RequiredCost)
            {
                return false;
            }

            if (requiredTypes == null) return true;
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

            return true;
        }

        public bool CanSkillLocked(SkillType skillType)
        {
            return !(from model in _dictionaryModels 
                let requiredTypes = model.Value.RequiredTypes 
                where requiredTypes != null 
                where requiredTypes.Where(type => type == skillType).Any(type => IsSkillUnlocked(model.Key)) 
                select model).Any() && IsSkillUnlocked(skillType);
        }

        private void LockSkill(SkillType skillType)
        {
            GetModel(skillType).IsOpened = false;
            Score += GetModel(skillType).RequiredCost;
            _unlockedSkillTypeList.Remove(skillType);
        }

        private void UnlockSkill(SkillType skillType)
        {
            if (IsSkillUnlocked(skillType)) return;

            GetModel(skillType).IsOpened = true;
            Score -= GetModel(skillType).RequiredCost;
            _unlockedSkillTypeList.Add(skillType);
        }
        
    }
}