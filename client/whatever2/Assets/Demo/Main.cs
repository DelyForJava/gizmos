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
            UnityEngine.UI.Image image;
            UnityEngine.Camera camera;
            UnityEngine.TextMesh mesh;
            UnityEngine.BoxCollider box;

            //StartCoroutine(Patch.StartCheck());
            //StartCoroutine(Version.StartCheck());
        }

        void Start()
        {
            Module.Start();
        }

        void Update()
        {
            // Module.Update();
            if (UnityEngine.Input.GetMouseButtonDown(0))
            {
                UnityEngine.RaycastHit hit;
                var ray = UnityEngine.Camera.main.ScreenPointToRay(UnityEngine.Input.mousePosition);
                if (UnityEngine.Physics.Raycast(ray, out hit))
                {
                    Module.OnClick(hit.collider.gameObject);
                }

            }

        }

    }

}