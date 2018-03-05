using System;

namespace Ellucian.Colleague.Dtos.Student.Custom
{
    /// <summary>
    /// The WebAdvisor Id and Date Created associated with a Colleague Id.
    /// </summary>
    public class WebAdvisorIdAndDateDto
    {
        /// <summary>
        /// The WebAdvisor Id associated with the given Colleague Id.
        /// </summary>
        public string WebAdvisorId { get; set; }

        /// <summary>
        /// The date for when the WebAdvisor Id was created.
        /// </summary>
        public DateTime? DateCreated { get; set; }

        /// <summary>
        /// The source Colleague Id
        /// </summary>
        public string ColleagueId { get; set; }
    }
}

