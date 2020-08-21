namespace whatever
{
    public class Cache : UnityEngine.MonoBehaviour
    {
        static System.Collections.Generic.Dictionary<string, string> map = new System.Collections.Generic.Dictionary<string, string>();

        private string versionFileName = "Version" ;

        public string GetString(string key)
        {
            string ret = string.Empty;
            if (map.ContainsKey(key))
                ret = map[key];
            else
                Todo();

            return ret;
        }

        public void Todo()
        {
            //the user may modify the cache data.
        }

        public static Cache instance;
        void Awake()
        {
            instance = this;
            StartCoroutine(File.GetString(versionFileName, (value) => { map.Add(versionFileName, value); }));
        }

    }

}