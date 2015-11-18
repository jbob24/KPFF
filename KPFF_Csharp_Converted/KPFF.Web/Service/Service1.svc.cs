using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;
using KPFF.PMP.Service.Contracts;
using KPFF.PMP.Web.Entities;

namespace KPFF.PMP.Service
{
    [ServiceContract(Namespace = "KPFF.PMP.Services")]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class Service1
    {
        // To use HTTP GET, add [WebGet] attribute. (Default ResponseFormat is WebMessageFormat.Json)
        // To create an operation that returns XML,
        //     add [WebGet(ResponseFormat=WebMessageFormat.Xml)],
        //     and include the following line in the operation body:
        //         WebOperationContext.Current.OutgoingResponse.ContentType = "text/xml";
        [OperationContract]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.Wrapped, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public EngineerResponse DoWork(EngineerRequest request)
        {
            var engineer = KPFF.PMP.Entities.Engineer.GetByEmployeeId(request.Id);
            //var response = new EngineerResponse() { EmployeeId = engineer.EmployeeID, Name = engineer.EmployeeName, HoursPerWeek = engineer.HoursPerWeek };

            var response = new EngineerResponse() { Engineer = engineer };

            // Add your operation implementation here
            return response;
        }

        [OperationContract]
        [WebGet(UriTemplate = "GetData?id={id}", ResponseFormat = WebMessageFormat.Json)]
        public EngineerResponse GetData(int id)
        {
            var engineer = KPFF.PMP.Entities.Engineer.GetByEmployeeId(id);
            //var response = new EngineerResponse() { EmployeeId = engineer.EmployeeID, Name = engineer.EmployeeName, HoursPerWeek = engineer.HoursPerWeek };

            var response = new EngineerResponse() { Engineer = engineer };

            // Add your operation implementation here
            return response;
        }

        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        public string GetThisWeekDate()
        {
            return DateTime.Now.GetMondayDate().ToShortDateString();
        }

        [OperationContract]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.Wrapped, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public DateResponse GetNextWeekDate(DateRequest request)
        {
            var response = new DateResponse();

            var date = new DateTime();

            if (DateTime.TryParse(request.Date, out date))
            {
                   response.Date = date.AddDays(7).GetMondayDate().ToShortDateString();
            }

            return response;
        }

        [OperationContract]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.Wrapped, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        public DateResponse GetPreviousWeekDate(DateRequest request)
        {
            var response = new DateResponse();

            var date = new DateTime();

            if (DateTime.TryParse(request.Date, out date))
            {
                response.Date = date.AddDays(-7).GetMondayDate().ToShortDateString();
            }

            return response;
        }

        // Add more operations here and mark them with [OperationContract]
    }
}
