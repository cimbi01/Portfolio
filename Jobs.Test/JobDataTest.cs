using Jobs.Data;
using Jobs.Data.WorkingPerson.Employee;
using Jobs.Data.WorkingPerson.Employer;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Jobs.Test
{
    [TestFixture]
    class JobDataTest
    {
        private Employee employee;
        private JobData jobData;

        private static Dictionary<string, int>[][] testCasesForAllMatch =
        {
            /*
             * All employee rate matches needed rate
             * Needed and employee skills are equals
             */
            new Dictionary<string, int>[]
            { 
                new Dictionary<string, int>()
                {
                    { "a", 1 }, {"b", 2 }, {"c", 3 }
                },
                new Dictionary<string, int>()
                {
                    { "a", 1 }, {"b", 2 }, {"c", 3 }
                }
            },
            /*
             * All employee rate matches needed rate
             * More employee skills than needed
             */
            new Dictionary<string, int>[]
            {
                new Dictionary<string, int>()
                {
                    { "c", 1 }, {"a", 2 }, {"b", 3 }
                },
                new Dictionary<string, int>()
                {
                    { "b", 3 }, {"c", 1 }, {"a", 2}, {"d", 4 }
                }
            }
        };

        private static Dictionary<string, int>[][] testCasesForNoMatch =
        {
            /*
             * No needed and employee skills are equals
             */
            new Dictionary<string, int>[]
            {
                new Dictionary<string, int>()
                {
                    { "a", 1 }, {"b", 2 }
                },
                new Dictionary<string, int>()
                {
                   {"c", 3 }, { "d", 2}
                }
            },
        };

        private static Dictionary<string, int>[][] testCasesForAllMatchHigher =
        {
            /*
             * Needed and employee skills are equals
             * Some employee rate higher than needed
             */
            new Dictionary<string, int>[]
            {
                new Dictionary<string, int>()
                {
                    { "a", 1 }, {"b", 2 }, {"c", 3 }
                },
                new Dictionary<string, int>()
                {
                    { "a", 2 }, {"b", 2 }, {"c", 4 }
                }
            },

            /*
             * More employee skills than needed
             * Some employee rate higher than needed
             */
            new Dictionary<string, int>[]
            {
                new Dictionary<string, int>()
                {
                    { "a", 1 }, {"b", 2 }, {"c", 3 }
                },
                new Dictionary<string, int>()
                {
                    { "a", 2 }, {"b", 2 }, {"c", 4 }, { "d", 4}
                }
            },
        };

        private static Dictionary<string, int>[][] testCasesForAllMatchLower=
        {
            /*
             * More needed skills than employee
             * All employee rate matches needed rate
             */
            new Dictionary<string, int>[]
            {
                new Dictionary<string, int>()
                {
                    { "a", 1 }, {"b", 2 }, {"c", 3 }, { "d", 4}
                },
                new Dictionary<string, int>()
                {
                    { "a", 1 }, {"b", 2 }, {"c", 3 }
                }
            },
            /*
             * More needed skills than employee
             * Some employee rate lower than needed
             */
            new Dictionary<string, int>[]
            {
                new Dictionary<string, int>()
                {
                    { "a", 1 }, {"b", 2 }, {"c", 3 }, { "d", 4}
                },
                new Dictionary<string, int>()
                {
                    { "a", 1 }, {"b", 1 }, {"c", 3 }
                }
            },
            /*
             * Needed and employee skills are equals
             * Some employee rate lower than needed
             */
            new Dictionary<string, int>[]
            {
                new Dictionary<string, int>()
                {
                    { "a", 1 }, {"b", 2 }, {"c", 3 }
                },
                new Dictionary<string, int>()
                {
                    { "a", 1 }, {"b", 1 }, {"c", 3 }
                }
            },
            /*
             * Some employee skills not matching needed, some needed skills not matching employee
             * Some employee rate lower than needed
             */
            new Dictionary<string, int>[]
            {
                new Dictionary<string, int>()
                {
                    { "a", 1 }, {"b", 2 }, {"c", 3 }, { "e", 5}
                },
                new Dictionary<string, int>()
                {
                    { "a", 1 }, {"b", 1 }, {"c", 3 }, { "d", 4}
                }
            },
            /*
             * Some employee skills not matching needed, some needed skills not matching employee
             * All employee rate matches needed rate
             */
            new Dictionary<string, int>[]
            {
                new Dictionary<string, int>()
                {
                    { "a", 1 }, {"b", 2 }, {"c", 3 }, { "e", 5}
                },
                new Dictionary<string, int>()
                {
                    { "a", 1 }, {"b", 2 }, {"c", 3 }, { "d", 4}
                }
            },
            /*
             * Some employee skills not matching needed, some needed skills not matching employee
             * Some employee rate matches needed rate
             * Some employee rate lower than needed rate
             * Some employee rate higher than needed rate
            */
            new Dictionary<string, int>[]
            {
                new Dictionary<string, int>()
                {
                    { "a", 1 }, {"b", 2 }, {"c", 3 }, { "e", 5}
                },
                new Dictionary<string, int>()
                {
                    { "a", 1 }, {"b", 1 }, {"c", 4 }, { "d", 4}
                }
            },
            /*
             * Some employee skills not matching needed, some needed skills not matching employee
             * Some employee rate lower than needed rate
             * Some employee rate higher than needed rate
            */
            new Dictionary<string, int>[]
            {
                new Dictionary<string, int>()
                {
                    { "a", 1 }, {"b", 2 }, {"c", 3 }, { "e", 5}
                },
                new Dictionary<string, int>()
                {
                    { "a", 2 }, {"b", 1 }, {"c", 4 }, { "d", 4}
                }
            },
            /*
             * Some employee skills not matching needed, some needed skills not matching employee
             * Some employee rate matches needed rate
             * Some employee rate lower than needed rate
            */
            new Dictionary<string, int>[]
            {
                new Dictionary<string, int>()
                {
                    { "a", 1 }, {"b", 2 }, {"c", 3 }, { "e", 5}
                },
                new Dictionary<string, int>()
                {
                    { "a", 1 }, {"b", 1 }, {"c", 3 }, { "d", 4}
                }
            },
             /* 
             * More needed skills than employee
             * Some employee rate matches needed rate
             * Some employee rate lower than needed rate
             * Some employee rate higher than needed rate
             */
            new Dictionary<string, int>[]
            {
                new Dictionary<string, int>()
                {
                    { "a", 1 }, {"b", 2 }, {"c", 3 }, { "d", 4}
                },
                new Dictionary<string, int>()
                {
                    { "a", 1 }, {"b", 1 }, {"c", 4 }
                }
            },
             /*
             * More needed skills than employee
             * Some employee rate matches needed rate
             * Some employee rate lower than needed rate
             */
            new Dictionary<string, int>[]
            {
                new Dictionary<string, int>()
                {
                    { "a", 1 }, {"b", 2 }, {"c", 3 }, { "d", 4}
                },
                new Dictionary<string, int>()
                {
                    { "a", 1 }, {"b", 1 }, {"c", 1 }
                }
            }, 
             /*
              * More needed skills than employee
             * Some employee rate lower than needed rate
             * Some employee rate higher than needed rate
             */
            new Dictionary<string, int>[]
            {
                new Dictionary<string, int>()
                {
                    { "a", 1 }, {"b", 2 }, {"c", 3 }, { "d", 4}
                },
                new Dictionary<string, int>()
                {
                    { "a", 2 }, {"b", 1 }, {"c", 4 }
                }
            }, 
             /* 
             * Needed and employee skills are equals
             * Some employee rate matches needed rate
             * Some employee rate lower than needed rate
             * Some employee rate higher than needed rate
             */
            new Dictionary<string, int>[]
            {
                new Dictionary<string, int>()
                {
                    { "a", 1 }, {"b", 2 }, {"c", 3 }
                },
                new Dictionary<string, int>()
                {
                    { "a", 1 }, {"b", 1 }, {"c", 4 }
                }
            },
             
             /*
              * Needed and employee skills are equals
             * Some employee rate matches needed rate
             * Some employee rate lower than needed rate
             */
            new Dictionary<string, int>[]
            {
                new Dictionary<string, int>()
                {
                    { "a", 1 }, {"b", 2 }, {"c", 3 }
                },
                new Dictionary<string, int>()
                {
                    { "a", 1 }, {"b", 2 }, {"c", 1 }
                }
            },
             
             /*
              * Needed and employee skills are equals
             * Some employee rate lower than needed rate
             * Some employee rate higher than needed rate
             */
            new Dictionary<string, int>[]
            {
                new Dictionary<string, int>()
                {
                    { "a", 1 }, {"b", 2 }, {"c", 3 }
                },
                new Dictionary<string, int>()
                {
                    { "a", 3 }, {"b", 1 }, {"c", 4 }
                }
            },
        };


        private void Init(Dictionary<string, int> neededKnowledgeRanges, Dictionary<string, int> employeeKnowledgeRanges)
        {
            this.InitEmployee(employeeKnowledgeRanges);
            this.InitJobData(neededKnowledgeRanges);
        }
        
        //TODO: Refactor -> SkillGeneration
        private void InitEmployee(Dictionary<string, int> employeeKnowledgeRanges)
        {
            string testName = "TestName";
            employee = new Employee(testName);
            List<Skill> employeeSkills = new List<Skill>();
            for (int i = 0; i < employeeKnowledgeRanges.Keys.Count; i++)
            {
                string skillName = employeeKnowledgeRanges.Keys.ElementAt(i);
                int range = employeeKnowledgeRanges[skillName];
                string details = "details" + Convert.ToString(range);
                employeeSkills.Add(
                    new Skill(skillName, details, range));
            }
            employee.ProfessionData.Skills.AddRange(employeeSkills);
        }

        //TODO: Refactor -> SkillGeneration
        private void InitJobData(Dictionary<string, int> neededKnowledgeRanges)
        {
            Employer employer = new Employer("Employer1");
            jobData = new JobData("job1", employer);

            List<Skill> neededSkills = new List<Skill>();
            for (int i = 0; i < neededKnowledgeRanges.Keys.Count; i++)
            {
                string skillName = neededKnowledgeRanges.Keys.ElementAt(i);
                int range = neededKnowledgeRanges[skillName];
                string details = "details" + Convert.ToString(range);
                neededSkills.Add(
                    new Skill(skillName, details, range));
            }
            jobData.NeededSkills.AddRange(neededSkills);
        }

        [Test]
        [TestCaseSource(nameof(JobDataTest.testCasesForAllMatch))]
        public void CalculateSuitabilityAllMatchTest(Dictionary<string, int> neededKnowledgeRanges, Dictionary<string, int> employeeKnowledgeRanges)
        {
            this.Init(neededKnowledgeRanges, employeeKnowledgeRanges);
            double suitability = jobData.CalculateSuitability(employee);
            double expectedSuitability = 1;

            Assert.AreEqual(expectedSuitability, suitability);
            Assert.GreaterOrEqual(suitability, 0);
        }

        [Test]
        [TestCaseSource(nameof(JobDataTest.testCasesForNoMatch))]
        public void CalculateSuitabilityNoMatchTest(Dictionary<string, int> neededKnowledgeRanges, Dictionary<string, int> employeeKnowledgeRanges)
        {
            this.Init(neededKnowledgeRanges, employeeKnowledgeRanges);
            double suitability = jobData.CalculateSuitability(employee);
            double expectedSuitability = 0;

            Assert.AreEqual(expectedSuitability, suitability);
            Assert.GreaterOrEqual(suitability, 0);
        }

        [Test]
        [TestCaseSource(nameof(JobDataTest.testCasesForAllMatchHigher))]
        public void CalculateSuitabilityAllMatchOrHigherTest(Dictionary<string, int> neededKnowledgeRanges, Dictionary<string, int> employeeKnowledgeRanges)
        {
            this.Init(neededKnowledgeRanges, employeeKnowledgeRanges);
            double suitability = jobData.CalculateSuitability(employee);

            Assert.GreaterOrEqual(suitability, 1);
            Assert.GreaterOrEqual(suitability, 0);
        }

        [Test]
        [TestCaseSource(nameof(JobDataTest.testCasesForAllMatchLower))]
        public void CalculateSuitabilityAllMatchOrLowerTest(Dictionary<string, int> neededKnowledgeRanges, Dictionary<string, int> employeeKnowledgeRanges)
        {
            this.Init(neededKnowledgeRanges, employeeKnowledgeRanges);
            double suitability = jobData.CalculateSuitability(employee);

            Assert.LessOrEqual(suitability, 1);
            Assert.GreaterOrEqual(suitability, 0);
        }
    }
}
