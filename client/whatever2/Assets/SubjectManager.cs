namespace whatever
{
    public class SubjectManager : UnityEngine.MonoBehaviour
    {
        internal static SubjectManager instance;

        void OnDestroy()
        {
            instance = null;
        }

        void Awake()
        {
            instance = this;
        }
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        int index;
        internal void Insert()
        {
            index++;
            var subject = new Subject();
            subject.id = index;
            subject.type = "type";
            subject.icon = "icon";
            subject.title = "title";
            DataManager.subjectList.Add(subject);
            var jsonStr = UnityEngine.JsonUtility.ToJson(subject);
            File.WriteString(DataManager.subjectListFileName, jsonStr);
        }

    }

    internal class Subject
    {
        public int id;
        public string type;
        public string icon;
        public string title;
    }

}