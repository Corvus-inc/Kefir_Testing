using Unity.VisualScripting;

namespace Skills
{
    public class SkillModel
    {
        public PlayerSkills.SkillType Type;
        public PlayerSkills.SkillType[] RequiredTypes;
        public string Name;
        public int RequiredCost;
        public bool IsOpened;
    }
}