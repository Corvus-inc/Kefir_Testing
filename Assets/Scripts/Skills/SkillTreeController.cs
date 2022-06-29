using System;
using System.Linq;
using StarterAssets;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;

namespace Skills
{
    public class SkillTreeController
    {
        private SkillTreeView _treeView;
        private PlayerSkills _playerSkills;
        private PanelButtonsView _buttonsView;
        private ThirdPersonController _personController;

        private SkillModel _selectedSkill;

        public SkillTreeController(PanelButtonsView buttonsView, SkillTreeView treeView)
        {
            _buttonsView = buttonsView;
            _treeView = treeView;
            _playerSkills = new PlayerSkills();

            treeView.SetPlayerSkills(GetPlayerSkills());
            
            _buttonsView.UpdateScore(_playerSkills.Score);

            foreach (var skillButton in _treeView.SkillButtonList)
            {
                skillButton.IsSelected += type =>
                {
                    _selectedSkill = _playerSkills._listModels.First(_ => _.Type == type);
                    
                    _buttonsView.UpdateScore(_playerSkills.Score);
                    OpenForgetting(_selectedSkill.Type);
                    OpenLearning(_selectedSkill.Type);
                };
            }

            _buttonsView.Learn += () =>
            {
                if (_playerSkills.TryUnlockSkill(_selectedSkill.Type))
                {
                    SetSkill(_selectedSkill.Type);
                    
                    _buttonsView.UpdateScore(_playerSkills.Score);
                    OpenForgetting(_selectedSkill.Type);
                    OpenLearning(_selectedSkill.Type);
                    
                    Debug.Log("Learn!");
                }
            };

            _buttonsView.Forget += () =>
            {
                if (_playerSkills.TryLockSkill(_selectedSkill.Type))
                {
                    Debug.Log("Forget!!");
                }
                else
                {
                    Debug.Log("Dont Forget!!");
                }
                
                _buttonsView.UpdateScore(_playerSkills.Score);
                OpenForgetting(_selectedSkill.Type);
                OpenLearning(_selectedSkill.Type);
            };
            
            _buttonsView.ForgetEverything += () =>
            {
                Debug.Log("Evrething!");
                _playerSkills.LockAllSkill();
                
                _buttonsView.UpdateScore(_playerSkills.Score);
                OpenForgetting(_selectedSkill.Type);
                OpenLearning(_selectedSkill.Type);
            };
            _buttonsView.AddScore += () =>
            {
                _playerSkills.AddScore();
                _buttonsView.UpdateScore(_playerSkills.Score);
                Debug.Log("Add Score+1!");
            };
        }

        public PlayerSkills GetPlayerSkills()
        {
            return _playerSkills;
        }

        private void OpenForgetting(PlayerSkills.SkillType skillType)
        {
            if (_playerSkills.CanSkillLocked(skillType))
            {
                _buttonsView.Forgetting(true);
            }
            else
            {
                _buttonsView.Forgetting(false);
            }
        }

        private void OpenLearning(PlayerSkills.SkillType skillType)
        {
            if (_playerSkills.CanSkillUnlock(skillType))
            {
                _buttonsView.Learning(!_playerSkills.IsSkillUnlocked(skillType));
            }
            else
            {
                _buttonsView.Learning(false);
            }
        }

        private void SetSkill(PlayerSkills.SkillType skillType)
        {
            switch (skillType)
            {
                case PlayerSkills.SkillType.None:
                    Debug.Log(skillType.ToString() + " opened");
                    break;
                case PlayerSkills.SkillType.Jump:
                    Debug.Log(skillType.ToString() + " opened");
                    break;
                case PlayerSkills.SkillType.Move:
                    Debug.Log(skillType.ToString() + " opened");
                    // ChangeMove();
                    break;
                case PlayerSkills.SkillType.Run:
                    Debug.Log(skillType.ToString() + " opened");
                    break;
                case PlayerSkills.SkillType.SitDown:
                    Debug.Log(skillType.ToString() + " opened");
                    break;
                case PlayerSkills.SkillType.Sleep:
                    Debug.Log(skillType.ToString() + " opened");
                    break;
                case PlayerSkills.SkillType.LieDown:
                    Debug.Log(skillType.ToString() + " opened");
                    break;
                case PlayerSkills.SkillType.Ready:
                    Debug.Log(skillType.ToString() + " opened");
                    break;
                case PlayerSkills.SkillType.Salsa:
                    Debug.Log(skillType.ToString() + " opened");
                    break;
                case PlayerSkills.SkillType.Wave:
                    Debug.Log(skillType.ToString() + " opened");
                    break;
                case PlayerSkills.SkillType.HipHop:
                    Debug.Log(skillType.ToString() + " opened");
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(skillType), skillType, null);
            }
        }

        private void ChangeMove()
        {
            _personController.SetMove(_playerSkills.IsSkillUnlocked(PlayerSkills.SkillType.Move));
        }
    }
}