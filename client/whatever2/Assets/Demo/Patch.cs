namespace Whatever
{
    public class Patch
    {
        static XLua.LuaEnv patchLuaEnv;
        static byte[] PatchLuaLoader(ref string fileName)
        {
            string fullPath = Define.patchLocalDirectory + @"/" + fileName + ".lua.txt";
            Debug.Log("patch lua script path:" + fullPath);
            if (!System.IO.File.Exists(fullPath))
                return null;
            string txtString = System.IO.File.ReadAllText(fullPath);
            return System.Text.Encoding.UTF8.GetBytes(txtString);
        }

        static Patch()
        {
            patchLuaEnv = new XLua.LuaEnv();
            patchLuaEnv.AddLoader(PatchLuaLoader);
        }

        public static System.Collections.IEnumerator StartCheck()
        {
            yield return GetRemotePatchID();
        }

        static int localPatchID;
        static int remotePatchID;
        static bool needGetPatch;
        public static int GetLocalPatchID()
        {
            int ret = 0;
            if (!UnityEngine.PlayerPrefs.HasKey("PatchID"))
            {
                UnityEngine.PlayerPrefs.SetInt("PatchID", 0);
            }
            ret = UnityEngine.PlayerPrefs.GetInt("PatchID");
            return ret;
        }

        static void OnGetRemotePatchIDFail(string info)
        {
            Debug.Log(info);
        }

        static void OnGetRemotePatchIDSuccess(string content)
        {
            remotePatchID = System.Convert.ToInt32(content);
            localPatchID = GetLocalPatchID();
            Debug.Log("remotePatchID:" + remotePatchID + ",localPatchID:" + localPatchID);
        }

        static void OnGetRemotePatchIDComplete()
        {

            if (remotePatchID == 0)
            {
                if (System.IO.File.Exists(Define.patchFullname))
                {
                    System.IO.File.Delete(Define.patchFullname);
                }

            }

            if (remotePatchID == localPatchID)
            {
                DoPatch();
                needGetPatch = false;
            }
            else
            {
                //StartCoroutine(GetPatch(DoPatch));
                needGetPatch = true;
            }

        }

        public static System.Collections.IEnumerator GetRemotePatchID()
        {
            Event.Brocast("EventOnPatchStateChanged", "Checking Patch");

            var url = Define.patchIDURL;
            Debug.Log("GetRemotePatchID:" + url);
            //yield return Network.Http.Get(url, OnGetRemotePatchIDFail, OnGetRemotePatchIDSuccess, OnGetRemotePatchIDComplete);
            yield return Network.Http.Get(url, OnGetRemotePatchIDFail, OnGetRemotePatchIDSuccess);

            OnGetRemotePatchIDComplete();

            if (needGetPatch)
                yield return GetRemotePatch();
        }


        static void OnGetRemotePatchFail(string info)
        {
            Debug.Log(info);
        }

        static void OnGetRemotePatchSuccess(string content)
        {
            var patchFullname = Define.patchFullname;
            if (!System.IO.Directory.Exists(Define.patchLocalDirectory))
                System.IO.Directory.CreateDirectory(Define.patchLocalDirectory);
            File.Create(patchFullname, System.Text.Encoding.Default.GetBytes(content));
        }

        static void OnGetRemotePatchComplete()
        {
            DoPatch();
        }

        public static System.Collections.IEnumerator GetRemotePatch()
        {
            Event.Brocast("EventOnPatchStateChanged", "Getting Patch");

            var url = Define.patchURL;
            Debug.Log("GetRemotePatch at url:" + url);
            yield return Network.Http.Get(url, OnGetRemotePatchFail, OnGetRemotePatchSuccess, OnGetRemotePatchComplete);
        }

        static void DoPatch()
        {
            var localCode = remotePatchID;
            UnityEngine.PlayerPrefs.SetInt("PatchID", localCode);

            if (System.IO.File.Exists(Define.patchFullname))
                patchLuaEnv.DoString("require 'hotfix'");

            Event.Brocast("EventOnPatchStateChanged", "Excute Patch");
        }

    }

}