namespace whatever
{
    class Game
    {
        public int ID;
        public string Name;
        public string Icon;
    }

    public class Main : UnityEngine.MonoBehaviour
    {
        public static string fileName = "GameList.json";
        public string ConfigPath;
        public string fullName;

        System.Collections.Generic.List<Game> gameList = new System.Collections.Generic.List<Game>();

        void OnDestroy()
        {
            EventManager.RemoveActionListener(EventDefine.RegetGameList, RegetGameList);
        }

        void Awake()
        {
            ConfigPath = UnityEngine.Application.persistentDataPath + "/config/";
            fullName = ConfigPath + fileName;

            EventManager.AddActionListener(EventDefine.RegetGameList, RegetGameList);
        }

        void Start()
        {
            gameList.Clear();

            if (System.IO.Directory.Exists(ConfigPath) && System.IO.File.Exists(fullName))
                LoadFileGameList();
            else
                RegetGameList();
        }

        void OnLoadCompleteGameList(string json)
        {
            var jsonArray = SimpleJSON.JSON.Parse(json) as SimpleJSON.JSONArray;
            foreach (SimpleJSON.JSONObject jsonObject in jsonArray)
            {
                var game = new Game();
                game.ID = jsonObject["ID"];
                game.Name = jsonObject["Name"];
                game.Icon = jsonObject["Icon"];
                gameList.Add(game);
            }
            Debug.Log(Define.LogPrefixLocalFileLoaded + "text:" + json);
        }

        void LoadFileGameList()
        {
            StartCoroutine(LoadFile(fullName, OnLoadCompleteGameList));
        }

        System.Collections.IEnumerator LoadFile(string fullName, System.Action<string> onComplete = null)
        {
            var request = UnityEngine.Networking.UnityWebRequest.Get(fullName);
            yield return request.SendWebRequest();
            if (request.isNetworkError || request.isHttpError)
            {
                Debug.LogError(request.error);
            }
            else
            {
                var json = request.downloadHandler.text;
                onComplete?.Invoke(json);
            }

        }

        public void RegetGameList()
        {
            var remoteFullName = Define.URL + fileName;
            StartCoroutine(Get(remoteFullName));
        }

        System.Collections.IEnumerator Get(string url)
        {
            UnityEngine.Networking.UnityWebRequest webRequest = UnityEngine.Networking.UnityWebRequest.Get(url);
            Debug.Log(Define.LogPrefixWebRequest + "get at url:" + url);
            yield return webRequest.SendWebRequest();

            if (webRequest.isHttpError || webRequest.isNetworkError)
            {
                Debug.Log(webRequest.error);
            }
            else
            {
                if (!System.IO.Directory.Exists(ConfigPath))
                    System.IO.Directory.CreateDirectory(ConfigPath);
                var json = webRequest.downloadHandler.text;
                var stream = System.IO.File.CreateText(fullName);
                stream.Write(json);
                stream.Close();
                Debug.Log(Define.LogPrefixWebRequest + "get and write file success");
            }

        }

        System.Collections.IEnumerator Post()
        {
            UnityEngine.WWWForm form = new UnityEngine.WWWForm();
            form.AddField("key", "value");
            form.AddField("name", "dely");

            UnityEngine.Networking.UnityWebRequest webRequest = UnityEngine.Networking.UnityWebRequest.Post(Define.URL, form);

            yield return webRequest.SendWebRequest();
            if (webRequest.isHttpError || webRequest.isNetworkError)
                Debug.Log(webRequest.error);
            else
                Debug.Log(webRequest.downloadHandler.text);
        }

    }

}