using Newtonsoft.Json;
using System;
using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

namespace OFG.ChessPeak
{
    public static class APIServer
    {
        private const string AcceptHeader = "application/json";
        private const string ContentTypeHeader = "application/json";

        private const bool debug = true;

        public static IEnumerator GET_BY_URL(string url, Action<string> callback, Action<string> error)
        {
            var request = CreateRequest(UnityWebRequest.kHttpVerbGET, url);
            request.redirectLimit = 5;
            yield return SendRequest(request, callback, error);
        }
        public static IEnumerator GET(string url, Action<string> callback, Action<string> error)
        {
            var request = CreateRequest(UnityWebRequest.kHttpVerbGET, GenerateFullURL(url));
            request.redirectLimit = 5;
            yield return SendRequest(request, callback, error);
        }

        public static IEnumerator POST(string data, string url, Action<string> callback, Action<string> error)
        {
            var request = CreateRequest(UnityWebRequest.kHttpVerbPOST, GenerateFullURL(url), data);
            yield return SendRequest(request, callback, error);
        }

        public static IEnumerator PATCH(string data, string url, Action<string> callback, Action<string> error)
        {
            var request = CreateRequest("PATCH", GenerateFullURL(url), data);
            yield return SendRequest(request, callback, error);
        }

        public static IEnumerator DELETE(string data, string url, Action<string> callback, Action<string> error)
        {
            var request = CreateRequest(UnityWebRequest.kHttpVerbDELETE, GenerateFullURL(url), data);
            yield return SendRequest(request, callback, error);
        }

        private static UnityWebRequest CreateRequest(string method, string url, string data = null)
        {
            UnityWebRequest request;

            if (method == UnityWebRequest.kHttpVerbGET)
            {
                request = UnityWebRequest.Get(url);
            }
            else if (method == UnityWebRequest.kHttpVerbDELETE && string.IsNullOrEmpty(data))
            {
                request = UnityWebRequest.Delete(url);
            }
            else
            {
                request = new UnityWebRequest(url, method);
                if (!string.IsNullOrEmpty(data))
                {
                    byte[] bodyRaw = Encoding.UTF8.GetBytes(data);
                    request.uploadHandler = new UploadHandlerRaw(bodyRaw);
                }
            }

            request.downloadHandler = new DownloadHandlerBuffer();
            //SetDefaultHeaders(request);
            request.SetRequestHeader("Content-Type", "application/json");
            request.SetRequestHeader("Accept", "application/json");

            Debug.Log("Request created, url: " + request.url + "\nData: " + data);
            return request;
        }

        private static string GenerateFullURL(string url)
        {
            return Const.BACKEND_SERVER + url;
        }
        private static void SetDefaultHeaders(UnityWebRequest request)
        {
            request.SetRequestHeader("Accept", AcceptHeader);
            request.SetRequestHeader("Content-Type", ContentTypeHeader);
        }

        private static IEnumerator SendRequest(UnityWebRequest request, Action<string> callback, Action<string> error)
        {
            if (debug)
                Debug.Log("SEND REQUEST\n" + request.uri);

            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                if (debug)
                    Debug.Log("REQUESR SUCCSESS\n" + request.downloadHandler.text);

                callback?.Invoke(request.downloadHandler.text);
            }
            else
            {
                if (debug)
                {
                    Debug.Log("REQUESR ERROR\n" +request.url + "\n" + request.error);
                }

                error?.Invoke(request.error);
            }
        }
    }
}
