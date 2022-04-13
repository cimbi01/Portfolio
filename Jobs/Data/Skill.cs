using System.ComponentModel.DataAnnotations;

namespace Jobs.Data
{
    public class Skill
    {
        public const int MinimumRangeOfKnowledge = 0;
        public const int MaximumRangeOfKnowledge = 5;

        [Required]
        //TODO: Should be predefined
        public string Name { get; set; }

        /// <summary>
        /// Details about the skill. Can be null.
        /// </summary>
        public string? Details { get; set; }

        [Required]
        [Range(MinimumRangeOfKnowledge, MaximumRangeOfKnowledge)]
        public int RangeOfKnowledge { get; set; }
        public Skill()
        {

        }

        public Skill(string name, int rangeOfKnowledge, string? details = null)
        {
            this.Name = name;
            this.Details = details;
            this.RangeOfKnowledge = rangeOfKnowledge;
        }
    }
}