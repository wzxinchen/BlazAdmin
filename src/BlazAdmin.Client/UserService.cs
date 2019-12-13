using Blazui.Component;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BlazAdmin.Client
{
    public class UserService
    {
        private readonly HttpClient httpClient;

        public ServerOptions Options { get; }

        public UserService(IOptions<ServerOptions> options, IHttpClientFactory httpClientFactory)
        {
            this.Options = options.Value;
            this.httpClient = httpClientFactory.CreateClient();
            httpClient.BaseAddress = new Uri(Options.ServerUrl);
        }

        internal Task<List<object>> GetUsersAsync()
        {
            return Task.FromResult(new List<object>());
        }

        internal async Task<bool> IsRequireInitilizeAsync()
        {
            var response = await httpClient.GetAsync(Options.RequireInitilizeUrl);
            return response.StatusCode == System.Net.HttpStatusCode.NotFound;
        }

        internal async Task<string> CreateSuperUserAsync(string username, string password)
        {
            var response = await httpClient.PostAsync(Options.CreateSuperUserUrl, new
            {
                Username = username,
                Password = password
            });
            return response;
        }

        internal async Task<string> DeleteUsersAsync(params object[] users)
        {
            var ids = new List<string>();
            var type = users.GetType().GetElementType();
            var properties = type.GetProperties();
            var idProperty = properties.FirstOrDefault(x => x.Name.Equals("id", StringComparison.CurrentCultureIgnoreCase));
            if (idProperty == null)
            {
                idProperty = properties.FirstOrDefault(x => x.Name.Equals("userid", StringComparison.CurrentCultureIgnoreCase));
            }
            if (idProperty == null)
            {
                idProperty = properties.FirstOrDefault(x => x.Name.EndsWith("id", StringComparison.CurrentCultureIgnoreCase));
            }
            if (idProperty == null)
            {
                throw new BlazuiException($"类型 {type.Name} 没有找到 id 属性，已按照如下规则查找，不区分大小写：1、id 属性，2、userid 属性，3、以 id 结尾的属性");
            }
            foreach (var user in users)
            {
                ids.Add(idProperty.GetValue(user)?.ToString());
            }
            var response = await httpClient.PostAsync(Options.DeleteUserUrl, ids);
            return response;
        }

        internal async Task<string> ChangePasswordAsync(string username, string oldPassword, string newPassword)
        {
            var response = await httpClient.PostAsync(Options.ChangePasswordUrl, new
            {
                Username = username,
                OldPassword = oldPassword,
                NewPasword = newPassword
            });
            return response;
        }

        internal async Task<string> CheckPasswordAsync(string username, string password)
        {
            var response = await httpClient.PostAsync(Options.CheckPasswordUrl, new
            {
                Username = username,
                Password = password
            });
            return response;
        }
    }
}
