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
        private readonly SkillTreePanelButtons _skillTreePanelButtons;
        private readonly SkillActivator _activator;
        private readonly SkillPlayerButtons _skillPlayerButtons;

        private SkillTreeButton _selectedBtn;
        private SkillModel _selectedSkill;
        private ThirdPersonController _personController;

        public SkillTreeController(SkillTreePanelButtons skillTreePanelButtons, 
            SkillTree tree, 
            SkillActivator activator, 
            SkillPlayerButtons skillPlayerButtons,
            ThirdPersonController personController)
        {
            _personController = personController;
            _skillTreePanelButtons = skillTreePanelButtons;
            _activator = activator;
            _skillPlayerButtons = skillPlayerButtons;
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

            _skillTreePanelButtons.Learn += () =>
            {
                if (!_playerSkills.TryUnlockSkill(_selectedSkill.Type)) return;
                
                AcceptSkill(_selectedSkill.Type);
                UpdateButtons();
            };

            _skillTreePanelButtons.Forget += () =>
            {
                if (_playerSkills.TryLockSkill(_selectedSkill.Type))
                {
                   CancelSkill(_selectedSkill.Type);
                }

                UpdateButtons();
            };

            _skillTreePanelButtons.ForgetEverything += () =>
            {
                _playerSkills.LockAllSkill();

                UpdateButtons();
//todo
                foreach (var button in tree.SkillButtonList)
                {
                    button.SetColourLearn(_playerSkills.GetModel(button.Type).IsOpened);
                }

                CancelAllSkills();
            };
            _skillTreePanelButtons.AddScore += () =>
            {
                _playerSkills.AddScore();

                UpdateButtons();
            };
        }

        private void UpdateButtons()
        {
            _skillTreePanelButtons.UpdateScore(_playerSkills.Score);
            IsLearning(_selectedSkill.Type);
            IsForgetting(_selectedSkill.Type);
            _selectedBtn.SetColourLearn(_playerSkills.IsSkillUnlocked(_selectedSkill.Type));
        }

        private void IsForgetting(PlayerSkills.SkillType skillType)
        {
            _skillTreePanelButtons.Forgetting(_playerSkills.CanSkillLocked(skillType));
        }

        private void IsLearning(PlayerSkills.SkillType skillType)
        {
            if (_playerSkills.CanSkillUnlock(skillType))
            {
                _skillTreePanelButtons.Learning(!_playerSkills.IsSkillUnlocked(skillType));
            }
            else
            {
                _skillTreePanelButtons.Learning(false);
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
                    _personController.CanJump = true;
                    Debug.Log(skillType.ToString() + " opened");
                    break;
                case PlayerSkills.SkillType.Move:
                    Debug.Log(skillType.ToString() + " opened");
                    // ChangeMove();
                    break;
                case PlayerSkills.SkillType.Run:
                    _personController.CanRun = true;
                    Debug.Log(skillType.ToString() + " opened");
                    break;
                case PlayerSkills.SkillType.SitDown:
                    _activator.CanSitting = true;
                    _skillPlayerButtons.GetPlayerButtons()[1].enabled = true;
                    _skillPlayerButtons.GetPlayerButtons()[1].image.color = Color.yellow;
                    Debug.Log(skillType.ToString() + " opened");
                    break;
                case PlayerSkills.SkillType.Sleep:
                    _activator.CanSleeping = true;
                    _skillPlayerButtons.GetPlayerButtons()[2].enabled = true;
                    _skillPlayerButtons.GetPlayerButtons()[2].image.color = Color.yellow;
                    Debug.Log(skillType.ToString() + " opened");
                    break;
                case PlayerSkills.SkillType.LieDown:
                    _activator.CanLying = true;
                    _skillPlayerButtons.GetPlayerButtons()[0].enabled = true;
                    _skillPlayerButtons.GetPlayerButtons()[0].image.color = Color.yellow;
                    Debug.Log(skillType.ToString() + " opened");
                    break;
                case PlayerSkills.SkillType.Ready:
                    _activator.CanReady = true;
                    _skillPlayerButtons.GetPlayerButtons()[3].enabled = true;
                    _skillPlayerButtons.GetPlayerButtons()[3].image.color = Color.yellow;
                    Debug.Log(skillType.ToString() + " opened");
                    break;
                case PlayerSkills.SkillType.Salsa:
                    _activator.CanSalsa = true;
                    _skillPlayerButtons.GetPlayerButtons()[5].enabled = true;
                    _skillPlayerButtons.GetPlayerButtons()[5].image.color = Color.yellow;
                    Debug.Log(skillType.ToString() + " opened");
                    break;
                case PlayerSkills.SkillType.Wave:
                    _activator.CanWave = true;
                    _skillPlayerButtons.GetPlayerButtons()[4].enabled = true;
                    _skillPlayerButtons.GetPlayerButtons()[4].image.color = Color.yellow;
                    Debug.Log(skillType.ToString() + " opened");
                    break;
                case PlayerSkills.SkillType.HipHop:
                    _activator.CanHipHop = true;
                    _skillPlayerButtons.GetPlayerButtons()[6].enabled = true;
                    _skillPlayerButtons.GetPlayerButtons()[6].image.color = Color.yellow;
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
                    _personController.CanJump = false;
                    Debug.Log(skillType.ToString() + " closed");
                    break;
                case PlayerSkills.SkillType.Move:
                    Debug.Log(skillType.ToString() + " closed");
                    // ChangeMove();
                    break;
                case PlayerSkills.SkillType.Run:
                    _personController.CanRun = false;
                    Debug.Log(skillType.ToString() + " closed");
                    break;
                case PlayerSkills.SkillType.SitDown:
                    _activator.Sitting = false;
                    _skillPlayerButtons.GetPlayerButtons()[1].enabled = false;
                    _skillPlayerButtons.GetPlayerButtons()[1].image.color = Color.gray;
                    Debug.Log(skillType.ToString() + " closed");
                    break;
                case PlayerSkills.SkillType.Sleep:
                    _activator.CanSleeping = false;
                    _skillPlayerButtons.GetPlayerButtons()[2].enabled = false;
                    _skillPlayerButtons.GetPlayerButtons()[2].image.color = Color.gray;
                    Debug.Log(skillType.ToString() + " closed");
                    break;
                case PlayerSkills.SkillType.LieDown:
                    _activator.CanLying = false;
                    _skillPlayerButtons.GetPlayerButtons()[0].enabled = false;
                    _skillPlayerButtons.GetPlayerButtons()[0].image.color = Color.gray;
                    Debug.Log(skillType.ToString() + " closed");
                    break;
                case PlayerSkills.SkillType.Ready:
                    _activator.CanReady = false;
                    _skillPlayerButtons.GetPlayerButtons()[3].enabled = false;
                    _skillPlayerButtons.GetPlayerButtons()[3].image.color = Color.gray;
                    Debug.Log(skillType.ToString() + " closed");
                    break;
                case PlayerSkills.SkillType.Salsa:
                    _activator.CanSalsa = false;
                    _skillPlayerButtons.GetPlayerButtons()[5].enabled = false;
                    _skillPlayerButtons.GetPlayerButtons()[5].image.color = Color.gray;
                    Debug.Log(skillType.ToString() + " closed");
                    break;
                case PlayerSkills.SkillType.Wave:
                    _activator.CanWave = false;
                    _skillPlayerButtons.GetPlayerButtons()[4].enabled = false;
                    _skillPlayerButtons.GetPlayerButtons()[4].image.color = Color.gray;
                    Debug.Log(skillType.ToString() + " closed");
                    break;
                case PlayerSkills.SkillType.HipHop:
                    _activator.CanHipHop = false;
                    _skillPlayerButtons.GetPlayerButtons()[6].enabled = false;
                    _skillPlayerButtons.GetPlayerButtons()[6].image.color = Color.gray;
                    Debug.Log(skillType.ToString() + " closed");
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(skillType), skillType, null);
            }
        }

        private void CancelAllSkills()
        {
            foreach (var model in _playerSkills._listModels)
            {
                CancelSkill(model.Type);
            }
        }
    }
}