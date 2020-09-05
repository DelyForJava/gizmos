namespace Whatever
{
    class Game
    {
        public int ID;
        public string Name;
        public string Icon;
        public int Level;
    }

    public class GameManager : UnityEngine.MonoBehaviour
    {
        static string gameListFileName = "GameList.json";
        static string gameListURL;
        static string gameListFullname;

        static string recentGameIconURL;
        static string recentGameIconFullname;

        System.Collections.Generic.List<Game> gameList = new System.Collections.Generic.List<Game>();
        Game recentGame;
        Game hotGame;
        Game newGame;

        DG.Tweening.Sequence sequence;
        public UnityEngine.UI.Image recentIcon;

        void OnDestroy()
        {
            Event.RemoveActionListener(EventDefine.GetGameListData, GetGameListData);
        }

        void Awake()
        {
            Event.AddActionListener(EventDefine.GetGameListData, GetGameListData);
        }

        // Start is called before the first frame update
        void Start()
        {
            gameList.Clear();

            recentGame = new Game();
            recentGame.ID = 1001;
            recentGame.Name = "Chess";
            recentGame.Icon = "r";
            recentGame.Level = 0;

            var first = DG.Tweening.DOTween.To(() => recentIcon.fillAmount, x => recentIcon.fillAmount = x, 0, 0.2f);
            var second = DG.Tweening.DOTween.To(() => recentIcon.fillAmount, x => recentIcon.fillAmount = x, 1, 0.5f);
            sequence = DG.Tweening.DOTween.Sequence();
            DG.Tweening.TweenSettingsExtensions.Append(sequence, first);
            DG.Tweening.TweenSettingsExtensions.Append(sequence, second);
            DG.Tweening.TweenSettingsExtensions.SetAutoKill(sequence, false);
        }

        void Update()
        {

        }

        void GetGameListData()
        {
            gameListURL = Define.prefixRemotePath + gameListFileName;
            //StartCoroutine(Network.Get(gameListURL, OnGetCompleteGameList));
        }

        void OnGetCompleteGameList(string json)
        {
            if (!System.IO.Directory.Exists(Define.configPath))
                System.IO.Directory.CreateDirectory(Define.configPath);

            gameListFullname = Define.configPath + gameListFileName;

            var stream = System.IO.File.CreateText(gameListFullname);
            stream.Write(json);
            stream.Close();
        }

        void LoadFileGameList()
        {
            //StartCoroutine(File.LoadFile(gameListFullname, OnLoadCompleteGameList));
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
            Debug.Log(Define.prefixLogCacheContent + json);
        }

        // ========================================  RecentGame ======================================== //
        public void GetRecentGameData()
        {
            DG.Tweening.TweenExtensions.Restart(sequence);

            recentGame.Level += 1;
            if (recentGame.Level > 7)
                recentGame.Level = 0;

            recentGameIconURL = Define.prefixRemotePath + recentGame.Icon + recentGame.Level + ".jpg";
            StartCoroutine(Network.Http.GetTexture(recentGameIconURL, OnGetCompleteRecentGameTexture));
        }

        void OnGetCompleteRecentGameTexture(UnityEngine.Texture2D texture)
        {
            if (!System.IO.Directory.Exists(Define.iconPath))
                System.IO.Directory.CreateDirectory(Define.iconPath);
            byte[] textureData = UnityEngine.ImageConversion.EncodeToJPG(texture);
            recentGameIconFullname = Define.iconPath + recentGame.Icon + recentGame.Level + ".jpg";
            System.IO.File.WriteAllBytes(recentGameIconFullname, textureData);
            LoadTextureRecentGameIcon();
        }

        void LoadTextureRecentGameIcon()
        {
            recentGameIconFullname = Define.iconPath + recentGame.Icon + recentGame.Level + ".jpg";
            StartCoroutine(File.LoadTexture(recentGameIconFullname, OnLoadTextureCompleteRecentGameIcon));
        }

        void OnLoadTextureCompleteRecentGameIcon(UnityEngine.Texture2D texture2d)
        {
            var sprite = UnityEngine.Sprite.Create(texture2d, new UnityEngine.Rect(0, 0, texture2d.width, texture2d.height), new UnityEngine.Vector2(0, 0));
            recentIcon.sprite = sprite;
        }

    }

}