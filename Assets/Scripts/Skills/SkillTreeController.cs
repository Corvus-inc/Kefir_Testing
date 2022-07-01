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

        private SkillButton _selectedBtn;
        private SkillModel _selectedSkill;
        private ThirdPersonController _personController;

        public SkillTreeController(PanelButtonsView buttonsView, SkillTree tree)
        {
            _buttonsView = buttonsView;
            _playerSkills = new PlayerSkills();
            
            tree.SetPlayerSkills(_playerSkills);
            
            //Init base skill
            _selectedSkill = _playerSkills.GetModel(BaseSkill);
            _selectedBtn = tree.SkillButtonList.First(_ => _.Type == _selectedSkill.Type);
            _selectedBtn.OnSelected(_selectedSkill.RequiredCost);
            if (_playerSkills.TryUnlockSkill(_selectedSkill.Type)) AcceptSkill(_selectedSkill.Type);
            _selectedSkill.IsBase = true;

            UpdateButtons();

            foreach (var skillButton in tree.SkillButtonList)
            {
                skillButton.IsSelected += type =>
                {
                    var lastBtn = _selectedBtn;
                    _selectedBtn = skillButton;
                    lastBtn.OffSelected();

                    _selectedSkill = _playerSkills.GetModel(type);
                    _selectedBtn.OnSelected(_selectedSkill.RequiredCost);

                    UpdateButtons();
                };
            }

            _buttonsView.Learn += () =>
            {
                if (!_playerSkills.TryUnlockSkill(_selectedSkill.Type)) return;
                
                AcceptSkill(_selectedSkill.Type);
                UpdateButtons();
            };

            _buttonsView.Forget += () =>
            {
                if (_playerSkills.TryLockSkill(_selectedSkill.Type))
                {
                   CancelSkill(_selectedSkill.Type);
                }

                UpdateButtons();
            };

            _buttonsView.ForgetEverything += () =>
            {
                _playerSkills.LockAllSkill();

                UpdateButtons();
//todo
                foreach (var button in tree.SkillButtonList)
                {
                    button.SetColourLearn(_playerSkills.GetModel(button.Type).IsOpened);
                }
            };
            _buttonsView.AddScore += () =>
            {
                _playerSkills.AddScore();

                UpdateButtons();
            };
        }

        private void UpdateButtons()
        {
            _buttonsView.UpdateScore(_playerSkills.Score);
            IsLearning(_selectedSkill.Type);
            IsForgetting(_selectedSkill.Type);
            _selectedBtn.SetColourLearn(_playerSkills.IsSkillUnlocked(_selectedSkill.Type));
        }

        private void IsForgetting(PlayerSkills.SkillType skillType)
        {
            _buttonsView.Forgetting(_playerSkills.CanSkillLocked(skillType));
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

        private void AcceptSkill(PlayerSkills.SkillType skillType)
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

        private void CancelSkill(PlayerSkills.SkillType skillType)
        {
            switch (skillType)
            {
                case PlayerSkills.SkillType.None:
                    Debug.Log(skillType.ToString() + " closed");
                    break;
                case PlayerSkills.SkillType.Jump:
                    Debug.Log(skillType.ToString() + " closed");
                    break;
                case PlayerSkills.SkillType.Move:
                    Debug.Log(skillType.ToString() + " closed");
                    // ChangeMove();
                    break;
                case PlayerSkills.SkillType.Run:
                    Debug.Log(skillType.ToString() + " closed");
                    break;
                case PlayerSkills.SkillType.SitDown:
                    Debug.Log(skillType.ToString() + " closed");
                    break;
                case PlayerSkills.SkillType.Sleep:
                    Debug.Log(skillType.ToString() + " closed");
                    break;
                case PlayerSkills.SkillType.LieDown:
                    Debug.Log(skillType.ToString() + " closed");
                    break;
                case PlayerSkills.SkillType.Ready:
                    Debug.Log(skillType.ToString() + " closed");
                    break;
                case PlayerSkills.SkillType.Salsa:
                    Debug.Log(skillType.ToString() + " closed");
                    break;
                case PlayerSkills.SkillType.Wave:
                    Debug.Log(skillType.ToString() + " closed");
                    break;
                case PlayerSkills.SkillType.HipHop:
                    Debug.Log(skillType.ToString() + " closed");
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