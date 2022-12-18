using System.Threading.Tasks;

namespace BuildingManagement.Web.Business.Services.Abstracts
{
    public interface IHttpService
    {
        Task<T> Get<T>(string uri,bool withToken = true);
        Task<T> Post<T>(string uri, object value, bool withToken = true);
        Task<T> Put<T>(string uri, object value);
        Task<T> Delete<T>(string uri);
    }
}
