namespace Whatever
{
    public class Module
    {
        static XLua.LuaEnv luaEnv;
        static byte[] LuaLoader(ref string fileName)
        {
            string fullPath = Define.moduleLocalDirectory + @"/" + fileName + ".lua.txt";
            Debug.Log("patch lua script path:" + fullPath);
            if (!System.IO.File.Exists(fullPath))
                return null;
            string txtString = System.IO.File.ReadAllText(fullPath);
            return System.Text.Encoding.UTF8.GetBytes(txtString);
        }
        static Module()
        {
            luaEnv = new XLua.LuaEnv();
            //luaEnv.AddLoader(LuaLoader);
        }

        public static void Start()
        {
            if(!System.IO.Directory.Exists(Define.moduleLocalDirectory))
            {
                System.IO.Directory.CreateDirectory(Define.moduleLocalDirectory);
            }
            luaEnv.DoString("require('main')");
        }

    }

}