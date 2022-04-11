using Jobs.Common;
using Jobs.Common.Factories;
using Jobs.Data;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Jobs.Test
{
    [TestFixture]
    class SkillTest : TestBase
    {

        private Skill CreateAndAssertSkill(int rangeOfKnowledge)
        {
            string name = "TestSkill";
            string details = "TestDetails";
            Skill skill = this.factory.CreateSkill(name, rangeOfKnowledge, details);
            ICollection<ValidationResult> results = new List<ValidationResult>();
            bool valid = SkillTest.Validate(skill, out results);
            Assert.AreEqual(name, skill.Name);
            Assert.AreEqual(details, skill.Details);
            Assert.AreEqual(rangeOfKnowledge, skill.RangeOfKnowledge);
            if(rangeOfKnowledge > Skill.MaximumRangeOfKnowledge || rangeOfKnowledge < Skill.MinimumRangeOfKnowledge)
            {
                Assert.IsFalse(valid);
            }
            else
            {
                Assert.IsTrue(valid);
            }
            return skill;
        }

        [Test]
        public void InValidRangeTest()
        {
            int rangeOfKnowledge = 7;
            this.CreateAndAssertSkill(rangeOfKnowledge);
        }

        [Test]
        public void ValidRangeTest()
        {
            int rangeOfKnowledge = 3;
            this.CreateAndAssertSkill(rangeOfKnowledge);
        }

        static bool Validate<T>(T obj, out ICollection<ValidationResult> results)
        {
            results = new List<ValidationResult>();

            return Validator.TryValidateObject(obj, new ValidationContext(obj), results, true);
        }
    }
}
