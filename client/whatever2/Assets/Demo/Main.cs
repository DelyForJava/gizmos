namespace Whatever
{
    public class Main : UnityEngine.MonoBehaviour
    {
        XLua.LuaEnv luaenv = new XLua.LuaEnv();
        int tick;
        void OnDestroy()
        {
        }

        void Awake()
        {
            Debug.Log(Define.prefixLocalPath);
            //var fullName = Define.configPath + DataManager.subjectListFileName;
            //var url = "https://120.78.209.232:9531/version.txt";
            //System.Net.HttpWebRequest request = System.Net.HttpWebRequest.CreateHttp(url);
            //var request = System.Net.FtpWebRequest.Create(url);
            //var respone = (System.Net.FtpWebResponse)request.GetResponse();
            //var respone = (System.Net.HttpWebResponse)request.GetResponse();
            //var createTime = System.IO.File.GetCreationTime(url);
            //var lastWriteTime = System.IO.File.GetLastWriteTime(url);
            //var lastAccessTime = System.IO.File.GetLastAccessTime(url);
            //Debug.Log(createTime);
            //Debug.Log(lastWriteTime);
            //Debug.Log(lastAccessTime);
            //Debug.Log(respone.LastModified);
            
        }

        void Start()
        {
            StartCoroutine(Patch.GetRemotePatchID());    
        }

        void Update()
        {
        }
    
    }

}