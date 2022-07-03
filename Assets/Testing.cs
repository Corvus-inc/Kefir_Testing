using Skills;
using StarterAssets;
using UnityEngine;
using UnityEngine.Serialization;

public class Testing : MonoBehaviour
{
    private SkillTreeController skillController;
    
    [SerializeField] private SkillTree skillTree;
    [SerializeField] private SkillPlayerButtons skillPlayerButtons;
    [SerializeField] private Animator _animator;
    [FormerlySerializedAs("panelButtonsView")] [SerializeField] private SkillTreePanelButtons skillTreePanelButtons;
    [SerializeField] private ThirdPersonController personController;

    private void Awake()
    {
        var activator = new SkillActivator(_animator);

        skillPlayerButtons.Activator = activator;
        
        skillController = new SkillTreeController(skillTreePanelButtons, skillTree, activator,skillPlayerButtons, personController);
    }
}
