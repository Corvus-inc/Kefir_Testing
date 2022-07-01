using Unity.VisualScripting;

namespace Skills
{
    public class SkillModel
    {
        public string Name { get; set; }
        public bool IsBase { get; set; }
        public bool IsOpened { get; set; }
        public int RequiredCost { get; set; }
        public PlayerSkills.SkillType Type { get; set; }
        public PlayerSkills.SkillType[] RequiredTypes { get; set; }
        public PlayerSkills.SkillType[] LockRequiredTypes { get; set; }
    }
}