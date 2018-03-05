using System;
using System.Threading.Tasks;
using Ellucian.Colleague.Data.Student.Transactions.Custom;
using Ellucian.Colleague.Domain.Student.Entities.Custom;
using Ellucian.Colleague.Domain.Student.Repositories.Custom;
using Ellucian.Data.Colleague;
using Ellucian.Data.Colleague.Repositories;
using Ellucian.Web.Cache;
using Ellucian.Web.Dependency;
using slf4net;

namespace Ellucian.Colleague.Data.Student.Repositories.Custom
{
    [RegisterType]
    public class WebAdvisorAndDateRepository : BaseColleagueRepository, IWebAdvisorIdAndDateRespository
    {
        #region Constructor

        /// <summary>The constructor which must account for the constructor
        ///  of the inherited BaseColleagueRepository class</summary>
        public WebAdvisorAndDateRepository(ICacheProvider cacheProvider, 
            IColleagueTransactionFactory transactionFactory, ILogger logger)
            : base(cacheProvider, transactionFactory, logger) { }

        #endregion

        #region Asynchronous Methods

        /// <summary>Retrieve WebAdvisor ID and Date Created from a Colleague Id string</summary>
        public Task<WebAdvisorIdAndDateEntity> GetWebAdvisorIdAndDateAsync(string ColleagueId)
        {
            var inputEntity = new WebAdvisorIdAndDateEntity()
            {
                ColleagueId = ColleagueId
            };

            return GetWebAdvisorIdAndDateAsync(inputEntity);
        }



        /// <summary>Retrieve WebAdvisor ID and Date Created from a WaiddEntity object</summary>
        public async Task<WebAdvisorIdAndDateEntity> GetWebAdvisorIdAndDateAsync
            (WebAdvisorIdAndDateEntity WaiddEntity)
        {
            var request = new URTRequest();
            request.ColleagueId = WaiddEntity.ColleagueId;

            var response = new URTResponse();

            try
            {
                response = await transactionInvoker.ExecuteAsync
                    <URTRequest, URTResponse>(request);
            }
            catch (Exception)
            {
                var errorText = "Transaction Invoker Execute Error for GetWAIDDRequest";
                logger.Error(errorText);
                throw new InvalidOperationException(errorText);
            }

            var entityResponse = BuildWebAdvisorIdAndDate(response, WaiddEntity.ColleagueId);

            return entityResponse;
        }

        #endregion

        #region DataContract to Entity Conversion

        /// <summary>Create a WebAdvisor ID and Date Entity object from a DataContract response.</summary>
        private WebAdvisorIdAndDateEntity BuildWebAdvisorIdAndDate(URTResponse response, string ColleagueId)
        {
            var entity = new WebAdvisorIdAndDateEntity(){
                WebAdvisorId = response.WebAdvisorId,
                ColleagueId = ColleagueId
            };

            DateTime dateOut;

            if (DateTime.TryParse(response.DateCreated, out dateOut))
            {
                entity.DateCreated = dateOut;
            }
            else
            {
                entity.DateCreated = null;
            }

            return entity;
        }

        #endregion

        #region Synchronous Methods

        /// <summary>An example of how you could call a synchronous version of the async method accepting a string</summary>
        public WebAdvisorIdAndDateEntity GetWebAdvisorIdAndDate(string ColleagueId)
        {
            var inputEntity = new WebAdvisorIdAndDateEntity()
            {
                ColleagueId = ColleagueId
            };

            var x = Task.Run(async () =>
            {
                return await GetWebAdvisorIdAndDateAsync(inputEntity);
            }).GetAwaiter().GetResult();
            return x;
        }

        /// <summary>An example of how to call a synchronous version of the async method accepting an object</summary>
        public WebAdvisorIdAndDateEntity GetWebAdvisorIdAndDate(WebAdvisorIdAndDateEntity WaiddEntity)
        {
            var x = Task.Run(async () =>
            {
                return await GetWebAdvisorIdAndDateAsync(WaiddEntity);
            }).GetAwaiter().GetResult();
            return x;
        }

        #endregion

    }
}
