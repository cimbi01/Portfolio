using Jobs.Common;
using Jobs.Common.Factories;
using Jobs.Data;
using Jobs.Exceptions;
using Jobs.Data.WorkingPerson.Employee;
using Jobs.Data.WorkingPerson.Employer;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Jobs.Test.Data
{
    [TestFixture]
    class JobDataTest : TestBase
    {
        private Employee employee;
        private JobData jobData;

        [SetUp]
        public override void Init()
        {
            this.offerHandler = new OfferHandler();
            this.factory = new OfferHandleredFactory(this.offerHandler);
        }

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

        private List<Skill> CreateSkills(Dictionary<string, int> knowledgeRanges)
        {
            List<Skill> skills = new List<Skill>();
            for (int i = 0; i < knowledgeRanges.Keys.Count; i++)
            {
                string skillName = knowledgeRanges.Keys.ElementAt(i);
                int range = knowledgeRanges[skillName];
                string details = "details" + Convert.ToString(range);
                skills.Add(this.factory.CreateSkill(skillName, range, details));
            }
            return skills;
        }

        private void Init(Dictionary<string, int> neededKnowledgeRanges, Dictionary<string, int> employeeKnowledgeRanges)
        {
            this.InitEmployee(employeeKnowledgeRanges);
            this.InitJobData(neededKnowledgeRanges);
        }
        
        private void InitEmployee(Dictionary<string, int> employeeKnowledgeRanges)
        {
            string testName = "Emloyee1";
            employee = this.WorkingPersonFactory.CreateEmployee(testName);
            List<Skill> employeeSkills = this.CreateSkills(employeeKnowledgeRanges);
            employee.ProfessionData.Skills.AddRange(employeeSkills);
        }

        private void InitJobData(Dictionary<string, int> neededKnowledgeRanges)
        {
            string testName = "Employer1";
            Employer employer = this.WorkingPersonFactory.CreateEmployer(testName);
            jobData = this.factory.CreateJobData("job1", employer);
            List<Skill> neededSkills = this.CreateSkills(neededKnowledgeRanges);
            jobData.NeededSkills.AddRange(neededSkills);
        }

        [Test]
        [TestCaseSource(nameof(JobDataTest.testCasesForAllMatch))]
        public void CalculateSuitabilityAllMatchTest(Dictionary<string, int> neededKnowledgeRanges, Dictionary<string, int> employeeKnowledgeRanges)
        {
            this.Init(neededKnowledgeRanges, employeeKnowledgeRanges);
            double suitability = this.jobData.CalculateSuitability(employee);
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

        [Test]
        public void StartTest()
        {
            this.Init(new Dictionary<string, int>(), new Dictionary<string, int>());
            this.jobData.Start();
            Assert.IsTrue(this.jobData.Started());
        }

        [Test]
        public void EndNotStartedTest()
        {
            this.Init(new Dictionary<string, int>(), new Dictionary<string, int>());
            Assert.Throws<JobNotStartedException>(() => this.jobData.End());
        }

        [Test]
        public void EndStartedTest()
        {
            this.Init(new Dictionary<string, int>(), new Dictionary<string, int>());
            this.jobData.Start();
            this.jobData.End();
            Assert.IsTrue(this.jobData.Ended());
        }
    }
}
