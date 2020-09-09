namespace Whatever
{
    public class Version
    {
        static void OnGetVersionIDFail(string info)
        {
            Debug.Log("OnGetVersionIDFail " + info);
        }

        static void OnGetVersionIDSuccess(string content)
        {
            var remoteVersion = content.Trim();
            Debug.Log("OnGetVersionIDSuccess remoteVersion:" + remoteVersion + ",localVersion:" + UnityEngine.Application.version);

            if (remoteVersion != UnityEngine.Application.version)
            {
                //todo
            }
            else
            {

            }

        }

        public static System.Collections.IEnumerator StartCheck(System.Action onComplete = null)
        {
            Event.Brocast("EventOnPatchStateChanged", "Checking Version");

            var url = Define.versionURL;
            Debug.Log("GetVersionID:" + url);
            //yield return Network.Http.Get(url, OnGetRemotePatchIDFail, OnGetRemotePatchIDSuccess, OnGetRemotePatchIDComplete);
            yield return Network.Http.Get(url, OnGetVersionIDFail, OnGetVersionIDSuccess);

            onComplete?.Invoke();
        }

    }

}