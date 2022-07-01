using System;
using System.Linq;
using StarterAssets;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;

namespace Skills
{
    public class SkillTreeController
    {
        private const PlayerSkills.SkillType BaseSkill = PlayerSkills.SkillType.Move;
        
        private readonly PlayerSkills _playerSkills;
        private readonly PanelButtonsView _buttonsView;
        
        private SkillTree _tree;
        private SkillButton _lastBtn;
        private SkillButton _selectedBtn;
        private SkillModel _selectedSkill;
        private ThirdPersonController _personController;

        public SkillTreeController(PanelButtonsView buttonsView, SkillTree tree)
        {
            _buttonsView = buttonsView;
            _tree = tree;
            _playerSkills = new PlayerSkills();
            
            tree.SetPlayerSkills(GetPlayerSkills());
            
            _selectedSkill = _playerSkills.GetModel(BaseSkill);
            _selectedBtn = _tree.SkillButtonList.First(_ => _.Type == _selectedSkill.Type);
            _selectedBtn.OnSelected(_selectedSkill.RequiredCost);
            if (_playerSkills.TryUnlockSkill(_selectedSkill.Type)) SetSkill(_selectedSkill.Type);
                

            UpdateButtons();

            foreach (var skillButton in _tree.SkillButtonList)
            {
                skillButton.IsSelected += type =>
                {
                    _lastBtn = _selectedBtn;
                    _selectedBtn = skillButton;
                    _lastBtn.OffSelected();

                    _selectedSkill = _playerSkills.GetModel(type);
                    _selectedBtn.OnSelected(_selectedSkill.RequiredCost);

                    UpdateButtons();
                };
            }

            _buttonsView.Learn += () =>
            {
                if (_playerSkills.TryUnlockSkill(_selectedSkill.Type))
                {
                    SetSkill(_selectedSkill.Type);

                    UpdateButtons();

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

                UpdateButtons();
            };

            _buttonsView.ForgetEverything += () =>
            {
                Debug.Log("Evrething!");
                _playerSkills.LockAllSkill();

                UpdateButtons();
            };
            _buttonsView.AddScore += () =>
            {
                _playerSkills.AddScore();

                UpdateButtons();
                
                Debug.Log("Add Score+1!");
            };
        }

        public PlayerSkills GetPlayerSkills()
        {
            return _playerSkills;
        }

        private void UpdateButtons()
        {
            _buttonsView.UpdateScore(_playerSkills.Score);
            IsLearning(_selectedSkill.Type);
            IsForgetting(_selectedSkill.Type);
        }

        private void IsForgetting(PlayerSkills.SkillType skillType)
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

        private void IsLearning(PlayerSkills.SkillType skillType)
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