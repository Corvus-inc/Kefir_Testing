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

        //todo move to player class
        public int Score { get; private set; } = 2;

        public void AddScore()
        {
            Score++;
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
                Score += modelSkill.RequiredCost;
            }

            _unlockedSkillTypeList.Clear();
        }


        public bool CanSkillUnlock(SkillType skillType)
        {
            var modelSkill = _listModels.First(_ => _.Type == skillType);
            var requiredTypes = modelSkill.RequiredTypes;

            if (Score < modelSkill.RequiredCost)
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
            Score += _listModels.First(_ => _.Type == skillType).RequiredCost;
            _unlockedSkillTypeList.Remove(skillType);
        }

        private void UnlockSkill(SkillType skillType)
        {
            if (IsSkillUnlocked(skillType)) return;

            Score -= _listModels.First(_ => _.Type == skillType).RequiredCost;
            _unlockedSkillTypeList.Add(skillType);
        }
    }
}