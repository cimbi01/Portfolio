using System.ComponentModel.DataAnnotations;

namespace Jobs.Data
{
    public class Skill
    {
        public const int MinimumRangeOfKnowledge = 0;
        public const int MaximumRangeOfKnowledge = 5;

        //TODO: Should be predefined
        public string Name { get; }

        /// <summary>
        /// Details about the skill. Can be null.
        /// </summary>
        public string? Details { get; }

        [Range(MinimumRangeOfKnowledge, MaximumRangeOfKnowledge)]
        public int RangeOfKnowledge { get; }

        public Skill(string name, string details, int rangeOfKnowledge)
        {
            this.Name = name;
            this.Details = details;
            this.RangeOfKnowledge = rangeOfKnowledge;
        }
    }
}