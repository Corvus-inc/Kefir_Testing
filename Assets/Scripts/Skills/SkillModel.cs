using Unity.VisualScripting;

namespace Skills
{
    public class SkillModel
    {
        public string Name;
        public bool IsOpened;
        public int RequiredCost;
        public PlayerSkills.SkillType Type;
        public PlayerSkills.SkillType[] RequiredTypes;
    }
}