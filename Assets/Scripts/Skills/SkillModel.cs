using Unity.VisualScripting;

namespace Skills
{
    public class SkillModel
    {
        public PlayerSkills.SkillType Type;
        public int Id;
        public string Name;
        public int RequiredCost;
        public bool IsOpened;
    }
}