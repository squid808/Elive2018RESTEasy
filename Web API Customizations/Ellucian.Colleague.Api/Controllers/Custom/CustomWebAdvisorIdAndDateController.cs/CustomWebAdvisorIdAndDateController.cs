using System.ComponentModel;
using System.Threading.Tasks;
using System.Web.Http;
using Ellucian.Colleague.Api.Licensing;
using Ellucian.Colleague.Configuration.Licensing;
using Ellucian.Colleague.Domain.Student.Entities.Custom;
using Ellucian.Colleague.Domain.Student.Repositories.Custom;
using Ellucian.Colleague.Dtos.Student.Custom;
using Ellucian.Web.Adapters;
using Ellucian.Web.Http.Controllers;
using Ellucian.Web.License;
using slf4net;
using System.Net;

namespace Ellucian.Colleague.Api.Controllers.Custom
{
    /// <summary>Controller for retrieving WebAdvisor Id and DateCreated</summary>
    [AllowAnonymous]
    [LicenseProvider(typeof(EllucianLicenseProvider))]
    [EllucianLicenseModule(ModuleConstants.Base)]
    public class CustomWebAdvisorIdAndDateController : BaseCompressedApiController
    {
        private readonly IWebAdvisorIdAndDateRespository webAdvisorRepository;
        private readonly IAdapterRegistry adapterRegistry;
        private readonly ILogger logger;

        /// <summary>The default constructor</summary>
        /// <param name="webAdvisorRepository">IWebAdvisorIdAndDateRespository instance</param>
        /// <param name="adapterRegistry">IAdapterRegistry instance</param>
        /// <param name="logger">ILogger instance</param>
        public CustomWebAdvisorIdAndDateController(IWebAdvisorIdAndDateRespository webAdvisorRepository, 
            IAdapterRegistry adapterRegistry, ILogger logger)
        {
            this.webAdvisorRepository = webAdvisorRepository;
            this.adapterRegistry = adapterRegistry;
            this.logger = logger;
        }

        /// <summary>Retrieve a WebAdvisor Id and DateCreated from a provided Colleague Id</summary>
        /// <param name="ColleagueId"></param>
        /// <returns></returns>
        public async Task<WebAdvisorIdAndDateDto> GetWebAdvisorIdAndDateFromColleagueIdAsync(string ColleagueId)
        {
            if (string.IsNullOrWhiteSpace(ColleagueId)) { throw new HttpResponseException(HttpStatusCode.BadRequest); }

            WebAdvisorIdAndDateEntity result = await webAdvisorRepository.GetWebAdvisorIdAndDateAsync(ColleagueId);

            var entityToDtoAdapter = adapterRegistry.GetAdapter<WebAdvisorIdAndDateEntity, WebAdvisorIdAndDateDto>();
            WebAdvisorIdAndDateDto responseDto = entityToDtoAdapter.MapToType(result);

            return responseDto;
        }

        /// <summary>
        /// This is not a real POST call, it's not updating anything, but shows you how to handle it.
        /// </summary>
        /// <param name="WaiddObj"></param>
        public async Task<WebAdvisorIdAndDateDto> PostWebAdvisorIdAndDateWaiddObjectAsync(WebAdvisorIdAndDateDto WaiddObj)
        {
            if (string.IsNullOrWhiteSpace(WaiddObj.ColleagueId)) { throw new HttpResponseException(HttpStatusCode.BadRequest); }

            //In a normal example we'd not just be going about using the adapter for evertyhing
            //here we're using it to demonstrate how to map types both for objects in and out
            var dtoToEntityAdapter = adapterRegistry.GetAdapter<WebAdvisorIdAndDateDto, WebAdvisorIdAndDateEntity>();
            WebAdvisorIdAndDateEntity requestEntity = dtoToEntityAdapter.MapToType(WaiddObj);
            WebAdvisorIdAndDateEntity requestResult = await webAdvisorRepository.GetWebAdvisorIdAndDateAsync(requestEntity);

            var entityToDtoAdapter = adapterRegistry.GetAdapter<WebAdvisorIdAndDateEntity, WebAdvisorIdAndDateDto>();
            WebAdvisorIdAndDateDto responseDto = entityToDtoAdapter.MapToType(requestResult);
            return responseDto;
        }
    }
}