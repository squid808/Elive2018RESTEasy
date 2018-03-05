using System.Threading.Tasks;
using Ellucian.Colleague.Domain.Student.Entities.Custom;

namespace Ellucian.Colleague.Domain.Student.Repositories.Custom
{
    /// <summary>Interface to WebAdvisorIdAndDateRepository</summary>
    public interface IWebAdvisorIdAndDateRespository
    {        
        /// <summary>Asynchronously get a WebAdvisor ID and Date Createed by Colleague Id</summary>
        Task<WebAdvisorIdAndDateEntity> GetWebAdvisorIdAndDateAsync(string ColleagueId);

        /// <summary>Asynchronously get a WebAdvisor ID and Date Createed by Colleague Id</summary>
        Task<WebAdvisorIdAndDateEntity> GetWebAdvisorIdAndDateAsync(WebAdvisorIdAndDateEntity WaiddInput);
    }
}
