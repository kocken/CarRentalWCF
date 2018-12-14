using Storage;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace CarRentalService
{
    [ServiceContract]
    public interface ICarRentalServiceRest
    {
        [OperationContract]
        [WebInvoke(Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped, UriTemplate = "GetCars")]
        List<Car> GetCarsRest();
    }
}
