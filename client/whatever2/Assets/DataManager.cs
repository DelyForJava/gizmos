namespace whatever
{
    public class DataManager : UnityEngine.MonoBehaviour
    {
        internal static DataManager instance;

        void OnDestroy()
        {
            instance = null;
        }

        void Awake()
        {
            instance = this;
            //StartCoroutine(File.GetString(versionFileName, (value) => { map.Add(versionFileName, value); }));
            StartCoroutine(File.ReadString(subjectListFileName, OnCompleteRead));
        }

        internal static System.Collections.Generic.Dictionary<string, string> map = new System.Collections.Generic.Dictionary<string, string>();

        internal static string subjectListFileName = "SubjectList";
        internal static System.Collections.Generic.List<string> subjectStringList = new System.Collections.Generic.List<string>();
        internal static System.Collections.Generic.List<Subject> subjectList = new System.Collections.Generic.List<Subject>();

        internal static void OnCompleteRead(string fileContent)
        {
            map.Add(subjectListFileName, fileContent);
            var fullName = Define.configPath + subjectListFileName;
            System.IO.StreamReader streamReader = new System.IO.StreamReader(fullName);
            string line;
            while ((line = streamReader.ReadLine()) != null)
            {
                subjectStringList.Add(line);
            }
            streamReader.Close();

            ParseStringToSubjectList();

        }

        internal static void ParseStringToSubjectList()
        {
            foreach (string subjectString in subjectStringList)
            {
                //var subject = new Subject();
                var subject = UnityEngine.JsonUtility.FromJson<Subject>(subjectString);
                //subject.id = json["id"];
                //subject.type = json["type"];
                //subject.icon = json["icon"];
                //subject.title = json["title"];
                subjectList.Add(subject);
            }

        }

        internal string GetString(string key)
        {
            string ret = string.Empty;
            if (map.ContainsKey(key))
                ret = map[key];
            else
                Todo();

            return ret;
        }

        internal void Todo()
        {
            //the user may modify the cache data.
        }

    }

}