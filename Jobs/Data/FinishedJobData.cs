using Jobs.Data.WorkingPerson.Employer;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jobs.Data
{
    class FinishedJobData : JobData
    {
        public FinishedJobData(string name, Employer employer) : base(name, employer)
        {
        }

        /// <summary>
        /// End time of the job.
        /// </summary>
        public DateTime EndTime { get; protected set; }

        /// <summary>
        /// The rates that the employees are giving to the done job. Can be empty if no Employee is giving rate.
        /// </summary>
        public List<int> EmployeeRates { get; protected set; } = new List<int>();

        /// <summary>
        /// The rate that the employer is giving to the done job. Can be null if Employer is not giving rate.
        /// </summary>
        public int? EmployerRate { get; protected set; }

        /// <summary>
        /// The comments that the employees are giving to the done job. Can be empty if no Employee is giving comments.
        /// </summary>
        public List<string> EmployeeComments { get; protected set; } = new List<string>();

        /// <summary>
        /// The comments that the employer is giving to the done job. Can be null if the job is not finished yet.
        /// </summary>
        public string? EmployerComments { get; protected set; }
    }
}
