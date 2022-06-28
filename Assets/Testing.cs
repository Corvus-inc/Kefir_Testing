using System;
using System.Collections;
using System.Collections.Generic;
using Skills;
using StarterAssets;
using UnityEngine;
using UnityEngine.Serialization;

public class Testing : MonoBehaviour
{
    private SkillTreeController skillController;
    
    [SerializeField] private SkillTreeView skillTreeView;
    [SerializeField] private PanelButtonsView panelButtonsView;
    [SerializeField] private ThirdPersonController personController;

    private void Awake()
    {
        skillController = new SkillTreeController(panelButtonsView, skillTreeView);
    }

    private void Start()
    {
        // skillTreeView.SetPlayerSkills(skillController.GetPlayerSkills());
    }

}
