namespace whatever
{
    public class File
    {
        public static void WriteString(string name, string text)
        {
            var fullName = Define.configPath + name;

            if (!System.IO.Directory.Exists(Define.configPath))
                System.IO.Directory.CreateDirectory(Define.configPath);

            System.IO.StreamWriter streamWriter = new System.IO.StreamWriter(fullName, true);
            streamWriter.WriteLine(text);
            streamWriter.Close();
            Debug.Log(Define.prefixLogLocalFileSaved + fullName);
        }

        public static System.Collections.IEnumerator ReadString(string name, System.Action<string> onComplete)
        {
            var fullName = Define.configPath + name;
            if (System.IO.Directory.Exists(Define.configPath) && System.IO.File.Exists(fullName))
            {
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
            else
            {
                //todo something
                yield return null;
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