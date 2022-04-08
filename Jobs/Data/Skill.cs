using System.ComponentModel.DataAnnotations;

namespace Jobs.Data
{
    public class Skill
    {
        public const int MinimumRangeOfKnowledge = 0;
        public const int MaximumRangeOfKnowledge = 5;

        public string Name { get; }
        public string Details { get; }

        [Range(MinimumRangeOfKnowledge, MaximumRangeOfKnowledge)]
        public int RangeOfKnowledge { get; }
    }
}