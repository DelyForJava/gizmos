namespace whatever
{
    public class File
    {
        public static System.Collections.IEnumerator GetString2(string name, System.Action<string> onComplete, bool fromRemote = false)
        {
            var fullName = Define.configPath + name;

                var url = Define.prefixURL + name;
                yield return Network.Http.GetString(url, (text) =>
                {
                    onComplete?.Invoke(text);
                    if (!System.IO.Directory.Exists(Define.configPath))
                        System.IO.Directory.CreateDirectory(Define.configPath);

                    System.IO.File.WriteAllText(fullName, text);
                    Debug.Log(Define.prefixLogLocalFileSaved + fullName);

                });

        }

        public static System.Collections.IEnumerator GetString(string name, System.Action<string> onComplete)
        {
            var fullName = Define.configPath + name;
            var request = UnityEngine.Networking.UnityWebRequest.Get(fullName);

            yield return request.SendWebRequest();
            if (request.isNetworkError || request.isHttpError)
            {
                Debug.LogError(request.error);
                //todo something
            }
            else
            {
                var text = request.downloadHandler.text;
                onComplete?.Invoke(text);
            }

        }

        public static System.Collections.IEnumerator LoadTexture(string fullName, System.Action<UnityEngine.Texture2D> onComplete)
        {
            var requestTexture = UnityEngine.Networking.UnityWebRequestTexture.GetTexture(fullName);
            yield return requestTexture.SendWebRequest();
            if (requestTexture.isNetworkError || requestTexture.isHttpError)
            {
                Debug.LogError(requestTexture.error);
            }
            else
            {
                var texture = ((UnityEngine.Networking.DownloadHandlerTexture)requestTexture.downloadHandler).texture;
                onComplete?.Invoke(texture);
            }

        }

    }

}