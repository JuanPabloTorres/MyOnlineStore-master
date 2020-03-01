using MyOnlineStore.Application.Common.Interfaces.DataStore;
using MyOnlineStore.Application.Common.Utilities;
using MyOnlineStore.Entities.Models.Models.DTOs;
using MyOnlineStore.Entities.Models.Users;
using MyOnlineStore.Models.Utils;
using MyOnlineStore.Shared.Models.DTOs.Users;
using MyOnlineStore.Shared.Models.Entries;
using MyOnlineStore.Shared.Models.Users;
using Newtonsoft.Json;
using Polly;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MyOnlineStore.Application.Data.DataStore
{
    public class UserDataStore : DataStoreService<User>, IUserDataStore
    {
        #region Constructors

        public UserDataStore(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
        {
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Get User By email and discriminator
        /// </summary>
        /// <param name="email"></param>
        /// <returns>A user as UserDataStore<User></returns>
        public User? GetUserByType(string email)
        {
            FullAPIUri = new Uri(BaseAPIUri, $"{nameof(GetUserByType)}/{email}");

            var response = HttpClient.GetStringAsync(FullAPIUri);
            var result = response.Result;
            var deserializeObject = JsonConvert.DeserializeObject<User>(response.Result, new JsonSerializerSettings
            {
                ContractResolver = new JsonPrivateResolver(),
                ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor
            });

            return deserializeObject;
        }

        /// <summary>
        /// Get user by email without Discrminator
        /// </summary>
        /// <param name="email"></param>
        /// <returns>User as its found</returns>
        public async Task<User?> GetUserByEmail(string email)
        {
            User? user = null;
            FullAPIUri = new Uri(BaseAPIUri, $"{nameof(GetUserByEmail)}/{email}");
            string stringResult = string.Empty;

            await Policy.Handle<Exception>()
                .WaitAndRetryAsync(
                    retryCount: 5,
                    sleepDurationProvider: retryAttemp => TimeSpan.FromSeconds(Math.Pow(1.5, retryAttemp)),
                    onRetry: (exception, timeSpan, retryCount, context) =>
                    {
                        // Add logic to be executed before each retry, such as logging
                        System.Diagnostics.Debug.WriteLine($"Retry #{retryCount} due to exception '{(exception.InnerException ?? exception).Message}'");
                        HttpClient.CancelPendingRequests();

                    }
                ).ExecuteAsync(async () =>
                {
                    var response = await HttpClient.GetStringAsync(FullAPIUri);

                    user = JsonConvert.DeserializeObject<User>(response,
                        new JsonSerializerSettings
                        {
                            ContractResolver = new JsonPrivateResolver(),
                            ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor
                        }); ;
                });

            return user;
        }

        public User GetByEmailWithAll(string email, Type type)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Call to api for validation of credentials and creation of a user
        /// </summary>
        /// <param name="userLoginDTO">Data for login validation</param>
        /// <returns>A token with validations of credentials and type</returns>
        public ValidUserCreateableToken? GetValidUserCreateableToken(UserLoginDTO userLoginDTO)
        {
            ValidUserCreateableToken? response = null;
            HttpResponseMessage responseMessage;
            string responseStringContent = string.Empty;

            FullAPIUri = new Uri(BaseAPIUri, $"{nameof(GetValidUserCreateableToken)}");

            string content = JsonConvert.SerializeObject(userLoginDTO);
            var byteContent = new StringContent(content, Encoding.UTF8, "application/json");

            var request = new HttpRequestMessage(HttpMethod.Get, FullAPIUri);
            request.Content = byteContent;
            

            Policy.Handle<Exception>()
                //.WaitAndRetryForever
                .Retry(
                    retryCount: 0,
                    //sleepDurationProvider: retryAttemp => TimeSpan.FromSeconds(Math.Pow(1, retryAttemp)),
                    onRetry: (exception, /*timeSpan,*/ retryCount, context) =>
                    {
                        // Add logic to be executed before each retry, such as logging
                        System.Diagnostics.Debug.WriteLine($"Retry #{retryCount} due to exception '{(exception.InnerException ?? exception).Message}'");
                        HttpClient.CancelPendingRequests();
                        
                    }
                ).ExecuteAndCapture(() =>
                {
                    request = new HttpRequestMessage
                    {
                        RequestUri = FullAPIUri,
                        Method = HttpMethod.Get,
                        Content = byteContent
                    };
                    responseMessage = HttpClient.SendAsync(request).Result;
                    if(responseMessage.IsSuccessStatusCode)
                    {
                        responseStringContent = responseMessage.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                        response = JsonConvert.DeserializeObject<ValidUserCreateableToken>(responseStringContent);
                    }
                    else
                    {
                        if(true)
                        {
                        }
                    }
                });

            return response;
        }

        //public ValidUserCreateableToken? GetValidUserCreateableTokenWithRetryPolicy(UserLoginDTO userLogin)
        //{
        //    return NetworkService.Retry()
        //}

        public string GetTypeOfUser(string email)
        {
            FullAPIUri = new Uri(BaseAPIUri, $"{nameof(GetTypeOfUser)}/{email}");
            var response = HttpClient.GetStringAsync(FullAPIUri);
            return response.Result;
        }

        public User AttatchAdminRole(Guid userId, Guid storeId)
        {
            FullAPIUri = new Uri(BaseAPIUri, $"{nameof(AttatchAdminRole)}/{userId}/{storeId}");
            string apiStringResponse;
            User user;
            apiStringResponse = HttpClient.GetStringAsync(FullAPIUri).GetAwaiter().GetResult();

            user = JsonConvert.DeserializeObject<User>(apiStringResponse);

            return user;
        }

        public async Task< IEnumerable<User>> GetUser(string userfilter)
        {
            FullAPIUri = new Uri(BaseAPIUri, $"{nameof(GetUser)}/{userfilter}");

            var response = await HttpClient.GetStringAsync(FullAPIUri);
            var result = response;
            var deserializeObject = JsonConvert.DeserializeObject<IEnumerable<User>>(result, new JsonSerializerSettings
            {
                ContractResolver = new JsonPrivateResolver(),
                ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor
            });

            return deserializeObject ??= new List<User>();
        }

        public async Task<bool> SendRequest(JobRequest jobrequest)
        {
            FullAPIUri = new Uri(BaseAPIUri, $"{nameof(SendRequest)}");
            List<string> serializedList = new List<string>(); ;

            var serializeObj = JsonConvert
                .SerializeObject(jobrequest, Formatting.Indented, new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Include,

                });

            // var stringSerializedList = serializedList.ToArray().ToString();
            var buffer = Encoding.UTF8.GetBytes(serializeObj);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            var request = new HttpRequestMessage(HttpMethod.Put, FullAPIUri);
            request.Content = byteContent;

            var response = HttpClient.SendAsync(request);

            if (response.GetAwaiter().GetResult().IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<bool>(response.GetAwaiter().GetResult().Content.ReadAsStringAsync().GetAwaiter().GetResult());
            }
            else
                return false;
        }

        public bool CreateUserConnection(UserConnection connection)
        {
            FullAPIUri = new Uri(BaseAPIUri, $"{nameof(CreateUserConnection)}");
            List<string> serializedList = new List<string>(); ;

            var serializeObj = JsonConvert
                .SerializeObject(connection , Formatting.Indented, new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Include,

                });

            // var stringSerializedList = serializedList.ToArray().ToString();
            var buffer = Encoding.UTF8.GetBytes(serializeObj);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            var request = new HttpRequestMessage(HttpMethod.Post, FullAPIUri);
            request.Content = byteContent;

            var response = HttpClient.SendAsync(request);

            if (response.GetAwaiter().GetResult().IsSuccessStatusCode)
            {
                return   JsonConvert.DeserializeObject<bool>(response.GetAwaiter().GetResult().Content.ReadAsStringAsync().GetAwaiter().GetResult());
            }
            else
                return false;


        }

        public bool UpdateUserConnection(UserConnection connection)
        {
            FullAPIUri = new Uri(BaseAPIUri, $"{nameof(UpdateUserConnection)}");
            List<string> serializedList = new List<string>(); ;

            var serializeObj = JsonConvert
                .SerializeObject(connection, Formatting.Indented, new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Include,

                });

            // var stringSerializedList = serializedList.ToArray().ToString();
            var buffer = Encoding.UTF8.GetBytes(serializeObj);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            var request = new HttpRequestMessage(HttpMethod.Put, FullAPIUri);
            request.Content = byteContent;

            var response = HttpClient.SendAsync(request);

            if (response.GetAwaiter().GetResult().IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<bool>(response.GetAwaiter().GetResult().Content.ReadAsStringAsync().GetAwaiter().GetResult());
            }
            else
                return false;
        }

        public bool RemoveUserConnection(UserConnection connection)
        {
            FullAPIUri = new Uri(BaseAPIUri, $"{nameof(RemoveUserConnection)}");
            List<string> serializedList = new List<string>(); ;

            var serializeObj = JsonConvert
                .SerializeObject(connection, Formatting.Indented, new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Include,

                });

            // var stringSerializedList = serializedList.ToArray().ToString();
            var buffer = Encoding.UTF8.GetBytes(serializeObj);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            var request = new HttpRequestMessage(HttpMethod.Post, FullAPIUri);
            request.Content = byteContent;

            var response = HttpClient.SendAsync(request);

            if (response.GetAwaiter().GetResult().IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<bool>(response.GetAwaiter().GetResult().Content.ReadAsStringAsync().GetAwaiter().GetResult());
            }
            else
                return false;
        }

        public bool DeclineRequest(JobRequest requestId)
        {
            FullAPIUri = new Uri(BaseAPIUri, $"{nameof(DeclineRequest)}");
            List<string> serializedList = new List<string>(); ;

            var serializeObj = JsonConvert
                .SerializeObject(requestId, Formatting.Indented, new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Include,

                });

            // var stringSerializedList = serializedList.ToArray().ToString();
            var buffer = Encoding.UTF8.GetBytes(serializeObj);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            var request = new HttpRequestMessage(HttpMethod.Post, FullAPIUri);
            request.Content = byteContent;

            var response = HttpClient.SendAsync(request);

            if (response.GetAwaiter().GetResult().IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<bool>(response.GetAwaiter().GetResult().Content.ReadAsStringAsync().GetAwaiter().GetResult());
            }
            else
                return false;
        }

        public bool UpdateRequest(JobRequest toUpdate)
        {
            FullAPIUri = new Uri(BaseAPIUri, $"{nameof(UpdateRequest)}");
            List<string> serializedList = new List<string>(); ;

            var serializeObj = JsonConvert
                .SerializeObject(toUpdate, Formatting.Indented, new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Include,

                });

            // var stringSerializedList = serializedList.ToArray().ToString();
            var buffer = Encoding.UTF8.GetBytes(serializeObj);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            var request = new HttpRequestMessage(HttpMethod.Put, FullAPIUri);
            request.Content = byteContent;

            var response = HttpClient.SendAsync(request);

            if (response.GetAwaiter().GetResult().IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<bool>(response.GetAwaiter().GetResult().Content.ReadAsStringAsync().GetAwaiter().GetResult());
            }
            else
                return false;
        }

        public async Task<User> GetById(Guid id)
        {
            FullAPIUri = new Uri(BaseAPIUri, $"{nameof(GetById)}/{id}");

            var response = await HttpClient.GetStringAsync(FullAPIUri);
            var result = response;
            var deserializeObject = JsonConvert.DeserializeObject<User>(result, new JsonSerializerSettings
            {
                ContractResolver = new JsonPrivateResolver(),
                ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor
            });

            return deserializeObject;
        }


        #endregion Methods
    }
}