namespace whatever
{
    public class Main : UnityEngine.MonoBehaviour
    {
        void OnDestroy()
        {
        }

        void Awake()
        {

        }

        void Start()
        {
            var str = Cache.instance.GetString("version");
            Debug.Log(Define.prefixLogCacheContent + str+ UnityEngine.Application.version); 
        }

    }

}