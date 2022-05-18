namespace GameFramework.API
{
    using System;
    using UnityEngine;
    using UnityEngine.Networking;
    using GameFramework.Logging;
    using System.Collections.Generic;
    public delegate void APICallback(UnityEngine.Networking.UnityWebRequest req, string error, string result);

    public struct APIRequest
    {
        public readonly uint id;
        public readonly APICallback callback;
        public readonly UnityEngine.Networking.UnityWebRequest wwwRequest;
        public UnityWebRequestAsyncOperation operation;

        public APIRequest(uint initId, APICallback initCb, UnityEngine.Networking.UnityWebRequest initReq)
        {
            id = initId;
            callback = initCb;
            wwwRequest = initReq;
            operation = null;
        }
    }
    

    ///<summary>
    /// APIService for API request
    /// You should override this class to make your own
    ///</summary>
    public abstract class APIService : MonoBehaviour
    {
        protected int MAX_REQUEST_AT_THE_SAME_TIME = 10;
        private uint _requestId = uint.MinValue;
        private List<APIRequest> _requests = new List<APIRequest>();
        private Queue<APIRequest> _pendingRequests = new Queue<APIRequest>();

        ///<summary>
        /// Host to send API requests
        /// Override this one so that you can make any custom for yours
        ///</summary>
        public virtual string GetHost()
        {
            return @"localhost";
        }

        public APIRequest Request(UnityWebRequest req, APICallback callback, object args = null)
        {
            if (req == null)
            {
                return default;
            }

            // The request will be call in next frame if APIServices has not reached max concurrent request
            APIRequest apiRequest = new APIRequest(++_requestId, callback, req);
            _pendingRequests.Enqueue(apiRequest);

            return apiRequest;
        }

        public APIRequest Request(EndPoint endPoint, APICallback callback, object args = null)
        {
            string url = GetHost() + endPoint.ResourceAt;

            UnityWebRequest req = GetRequest(url, endPoint.Method);

            SetupHeader(endPoint, req);
            SerializeObject(endPoint, req, args);

            return Request(req, callback, args);
        }

        protected virtual void Update()
        {
            int reqCount = _requests.Count;
            int pendingCount = _pendingRequests.Count;

            if (reqCount == 0 && pendingCount == 0) { return; }

            UpdateAPIRequesting();
        }

        private void UpdateAPIRequesting()
        {
            for (int i = _requests.Count - 1; i >= 0; i--)
            {
                APIRequest apiReq = _requests[i];
                try
                {
                    if (apiReq.operation.isDone)
                    {
                        if (!string.IsNullOrEmpty(apiReq.wwwRequest.error))
                        {
                            UnityLog.LogE(string.Format("[APIService] Error {0}{1}\nURL:{2}", apiReq.wwwRequest.responseCode, apiReq.wwwRequest.error, apiReq.wwwRequest.url));
                            apiReq.callback.Invoke(apiReq.wwwRequest, apiReq.wwwRequest.error, default);
                        }
                        else
                        {
                            try
                            {
                                string json = apiReq.wwwRequest.downloadHandler.text;
                                if (!string.IsNullOrEmpty(json))
                                {
                                    apiReq.callback.Invoke(apiReq.wwwRequest, null, json);
                                }
                                else
                                {
                                    apiReq.callback.Invoke(apiReq.wwwRequest, null, null);
                                }
                            }
                            catch (Exception e)
                            {
                                UnityLog.LogE(string.Format("[APIService] Parse Error: {0}", e.ToString()));
                                apiReq.callback.Invoke(apiReq.wwwRequest, e.ToString(), default);
                            }
                        }
                        apiReq.wwwRequest.Dispose();
                        _requests.RemoveAt(i);
                    }
                }
                catch (Exception e)
                {
                    UnityLog.LogE(string.Format("[APIService] Internal Error: {0}", e.ToString()));
                    apiReq.wwwRequest.Dispose();
                    _requests.RemoveAt(i);
                }
            }
        }

        private void UpdatePendingAPI()
        {
            if (_requests.Count < MAX_REQUEST_AT_THE_SAME_TIME && _pendingRequests.Count > 0)
            {
                while (_requests.Count < MAX_REQUEST_AT_THE_SAME_TIME && _pendingRequests.Count > 0)
                {
                    APIRequest apiReq = _pendingRequests.Dequeue();
                    try
                    {
                        apiReq.operation = apiReq.wwwRequest.SendWebRequest();
                        _requests.Add(apiReq);
                    }
                    catch (System.Exception e)
                    {
                        UnityLog.LogE(string.Format("[APIService] Internal Error: {0}", e.ToString()));
                    }
                }
            }
        }

        private void SetupHeader(EndPoint endPoint, UnityWebRequest request)
        {
            OnSetupHeader(endPoint, request);
        }

        
        private void SerializeObject(EndPoint endPoint, UnityWebRequest request, object args)
        {
            OnSerializeObject(endPoint, request, args);
        }

        ///<summary>
        /// This function will set header properties such as Content-Type, API token ...
        /// Override this one so that you can make any custom for yours
        ///</summary>
        protected virtual void OnSetupHeader(EndPoint endPoint, UnityWebRequest request)
        {
            request.SetRequestHeader("Content-Type", "application/json; charset=UTF-8");
        }

        ///<summary>
        /// This function will serialize data which will be sent to back-end, default is raw
        /// Override this one so that you can make any custom for yours
        ///</summary>
        protected virtual void OnSerializeObject(EndPoint endPoint, UnityWebRequest request, object args)
        {
            request.downloadHandler = new DownloadHandlerBuffer();
            if (args != null)
            {
#if UNITY_EDITOR
                UnityLog.LogI(string.Format("[APIService] request params {0}", args.ToString()));
#endif
                request.uploadHandler = new UploadHandlerRaw(System.Text.Encoding.UTF8.GetBytes(args.ToString()));
                
            }
        }

        ///<summary>
        /// This function will create an UnityWebRequest corresponding to APIMethod
        /// Override this one so that you can make any custom for yours
        ///</summary>
        protected virtual UnityWebRequest GetRequest(string url, APIMethod method)
        {
            string kHttpVerb = null;
            UnityWebRequest request = null;
            switch (method)
            {
                case APIMethod.GET:
                    kHttpVerb = UnityWebRequest.kHttpVerbGET;
                    break;
                case APIMethod.POST:
                    kHttpVerb = UnityWebRequest.kHttpVerbPOST;
                    break;
                case APIMethod.PUT:
                    kHttpVerb = UnityWebRequest.kHttpVerbPUT;
                    break;
                case APIMethod.DELETE:
                    kHttpVerb = UnityWebRequest.kHttpVerbDELETE;
                    break;

            }
            if (!string.IsNullOrEmpty(kHttpVerb))
            {
                request = new UnityWebRequest()
                {
                    url = url,
                    method = kHttpVerb,
                };
            }
            return request;
        }
    }
}


