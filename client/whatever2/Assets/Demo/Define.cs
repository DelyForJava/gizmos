namespace Whatever
{
    public class Define
    {
        //public readonly static string ConfigPath = UnityEngine.Application.persistentDataPath + "/config/";
        public readonly static string prefixLogWebRequest = ">======= web request: ";
        public readonly static string prefixLogLocalFileSaved = ">------- local file saved: ";
        public readonly static string prefixLogCacheContent = ">------- cache content: ";
        public readonly static string prefixRemotePath = "http://120.78.209.232:9531/";
        public readonly static string prefixLocalPath = UnityEngine.Application.persistentDataPath+"/";


        public readonly static string iconPath = UnityEngine.Application.persistentDataPath + "/icon/";
        public readonly static string configPath = UnityEngine.Application.persistentDataPath + "/config/";
        public readonly static string assetBundlePath;


        public readonly static string versionName = "version";
        public readonly static string versionURL = prefixRemotePath + "version";


        public readonly static string patchDirectory = "patch/";
        public readonly static string patchName = "hotfix.lua.txt";

        public readonly static string patchIDURL = prefixRemotePath + patchDirectory + "patchID.txt";
        public readonly static string patchURL = prefixRemotePath + patchDirectory + patchName;
        public readonly static string patchLocalDirectory = prefixLocalPath + patchDirectory;
        public readonly static string patchFullname = prefixLocalPath + patchDirectory + patchName;

        public readonly static string moduleDirectory = "module/";
        public readonly static string moduleLocalDirectory = prefixLocalPath + moduleDirectory;

        public readonly static UnityEngine.Color myGreen = new UnityEngine.Color(70,200,70,135);
        public readonly static UnityEngine.Color myBlue = new UnityEngine.Color(0, 120, 255, 135);
        public readonly static UnityEngine.Color myPurple = new UnityEngine.Color(170, 0, 160, 135);
        public readonly static UnityEngine.Color myOrange = new UnityEngine.Color(250,150,0, 135);
        public readonly static UnityEngine.Color myYellow = new UnityEngine.Color(255,255,0, 135);
    }

}