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
            UnityEngine.UI.Button btn;
            //StartCoroutine(Patch.StartCheck());
            //StartCoroutine(Version.StartCheck());
        }

        void Start()
        {
            Module.Start();
        }

        void Update()
        {
        }

    }

}