namespace Whatever.Network
{
    public class Http
    {
        public static System.Collections.IEnumerator GetTexture(string url, System.Action<UnityEngine.Texture2D> onComplete)
        {
            UnityEngine.Networking.UnityWebRequest webRequest = UnityEngine.Networking.UnityWebRequestTexture.GetTexture(url);
            Debug.Log(Define.prefixLogWebRequest + "url = " + url);
            yield return webRequest.SendWebRequest();

            if (webRequest.isHttpError || webRequest.isNetworkError)
            {
                Debug.Log(webRequest.error);
            }
            else
            {
                onComplete?.Invoke(((UnityEngine.Networking.DownloadHandlerTexture)webRequest.downloadHandler).texture);
            }

        }

        public static System.Collections.IEnumerator Get(string url, System.Action<string> onFail = null, System.Action<string> onSuccess = null, System.Action onComplete = null)
        {
            using (UnityEngine.Networking.UnityWebRequest webRequest = UnityEngine.Networking.UnityWebRequest.Get(url))
            {
                webRequest.disposeCertificateHandlerOnDispose = false;
                yield return webRequest.SendWebRequest();

                if (webRequest.isHttpError || webRequest.isNetworkError)
                {
                    onFail?.Invoke(webRequest.error);
                }
                else
                {
                    onSuccess?.Invoke(webRequest.downloadHandler.text);
                }

            }
            onComplete?.Invoke();
        }

        public static System.Collections.IEnumerator Post(string url)
        {
            UnityEngine.WWWForm form = new UnityEngine.WWWForm();
            form.AddField("key", "value");
            form.AddField("name", "dely");

            UnityEngine.Networking.UnityWebRequest webRequest = UnityEngine.Networking.UnityWebRequest.Post(url, form);

            yield return webRequest.SendWebRequest();
            if (webRequest.isHttpError || webRequest.isNetworkError)
                Debug.Log(webRequest.error);
            else
                Debug.Log(webRequest.downloadHandler.text);
        }

    }

}