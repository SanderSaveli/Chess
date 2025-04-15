using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

namespace OFG.ChessPeak
{
    public static class APIServer
    {
        private const string AcceptHeader = "application/json";
        private const string ContentTypeHeader = "application/json";

        private const bool debug = true;

        public static IEnumerator GET(string url, Action<string> callback, Action<string> error)
        {
            var request = CreateRequest(UnityWebRequest.kHttpVerbGET, GenerateFullURL(url));
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
                    byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(data);
                    request.uploadHandler = new UploadHandlerRaw(bodyRaw);
                    request.SetRequestHeader("Content-Type", ContentTypeHeader);
                }
            }

            request.downloadHandler = new DownloadHandlerBuffer();
            SetDefaultHeaders(request);

            return request;
        }

        private static string GenerateFullURL(string url)
        {
            return Const.BACKEND_SERVER + url;
        }
        private static void SetDefaultHeaders(UnityWebRequest request)
        {
            request.SetRequestHeader("Accept", AcceptHeader);
        }

        private static IEnumerator SendRequest(UnityWebRequest request, Action<string> callback, Action<string> error)
        {
            if (debug)
                Debug.Log("SEND REQUESR\n" + request.uri);

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
                    Debug.Log("REQUESR ERROR\n" + request.error);

                error?.Invoke(request.error);
            }
        }
    }
}
