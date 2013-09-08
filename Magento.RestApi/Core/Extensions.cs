using System;
using System.Threading.Tasks;
using RestSharp;

namespace Magento.RestApi.Core
{
    public static class Extensions
    {
        private static Task<IRestResponse<T>> ExecuteAsync<T>(this RestClient client, IRestRequest request, Func<IRestResponse<T>, IRestResponse<T>> selector) where T : new()
        {
            var taskCompletionSource = new TaskCompletionSource<IRestResponse<T>>();
            var restRequestAsyncHandle = client.ExecuteAsync<T>(request, r =>
            {
                if (r.ErrorException == null)
                {
                    taskCompletionSource.SetResult(selector(r));
                }
                else
                {
                    taskCompletionSource.SetException(r.ErrorException);
                }
            });
            return taskCompletionSource.Task;
        }

        public static Task<IRestResponse<T>> GetResponseAsync<T>(this RestClient client, IRestRequest request) where T : new()
        {
            return client.ExecuteAsync<T>(request, r => r);
        }

        private static Task<IRestResponse> ExecuteAsync(this RestClient client, IRestRequest request, Func<IRestResponse, IRestResponse> selector)
        {
            var taskCompletionSource = new TaskCompletionSource<IRestResponse>();
            var restRequestAsyncHandle = client.ExecuteAsync(request, r =>
            {
                if (r.ErrorException == null)
                {
                    taskCompletionSource.SetResult(selector(r));
                }
                else
                {
                    taskCompletionSource.SetException(r.ErrorException);
                }
            });
            return taskCompletionSource.Task;
        }

        public static Task<IRestResponse> GetResponseAsync(this RestClient client, IRestRequest request)
        {
            return client.ExecuteAsync(request, r => r);
        }
    }
}
