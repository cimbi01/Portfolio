﻿using Jobs.Data;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Jobs.Test
{
    [TestFixture]
    class SkillTest
    {
        [Test]
        public void InValidRangeTest()
        {
            string name = "TestSkill";
            string details = "TestDetails";
            int rangeOfKnowledge = 7;
            Skill skill = new Skill(name, details, rangeOfKnowledge);
            ICollection<ValidationResult> results = new List<ValidationResult>();
            bool valid = SkillTest.Validate(skill, out results);
            Assert.AreEqual(name, skill.Name);
            Assert.AreEqual(details, skill.Details);
            Assert.AreEqual(rangeOfKnowledge, skill.RangeOfKnowledge);
            Assert.IsFalse(valid);
        }

        [Test]
        public void ValidRangeTest()
        {
            string name = "TestSkill";
            string details = "TestDetails";
            int rangeOfKnowledge = 3;
            Skill skill = new Skill(name, details, rangeOfKnowledge);
            ICollection<ValidationResult> results = new List<ValidationResult>();
            bool valid = SkillTest.Validate(skill, out results);
            Assert.AreEqual(name, skill.Name);
            Assert.AreEqual(details, skill.Details);
            Assert.AreEqual(rangeOfKnowledge, skill.RangeOfKnowledge);
            Assert.IsTrue(valid);
        }

        static bool Validate<T>(T obj, out ICollection<ValidationResult> results)
        {
            results = new List<ValidationResult>();

            return Validator.TryValidateObject(obj, new ValidationContext(obj), results, true);
        }
    }
}