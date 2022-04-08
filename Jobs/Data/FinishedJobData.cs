using System;
using System.Collections.Generic;
using System.Text;

namespace Jobs.Data
{
    class FinishedJobData : JobData
    {

        /// <summary>
        /// End time of the Job. Can be null if the job is not finished yet.
        /// </summary>
        public DateTime? EndTime { get; protected set; }

        /// <summary>
        /// The average of the rates that the employees are giving to the done job. Can be null if the job is not finished yet.
        /// </summary>
        public int? AverageEmployeeRate { get; protected set; }

        /// <summary>
        /// The rate that the employer is giving to the done job. Can be null if the job is not finished yet.
        /// </summary>
        public int? EmployerRate { get; protected set; }


        /// <summary>
        /// The comments that the employees are giving to the done job. Can be null if the job is not finished yet.
        /// </summary>
        public List<string>? EmployeesComments { get; protected set; }

        /// <summary>
        /// The comments that the employer is giving to the done job. Can be null if the job is not finished yet.
        /// </summary>
        public string? EmployerComments { get; protected set; }
    }
}
