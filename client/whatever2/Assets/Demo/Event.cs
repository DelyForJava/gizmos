namespace Whatever
{
    public delegate void Callback(params object[] objs);
    //public class EventManager : Singleton<EventManager>
    class EventDefine
    {
        public readonly static string GetGameListData = "GetGameListData";
    }

    public class Event
    {
        private static System.Collections.Generic.Dictionary<string, System.Collections.Generic.List<System.Action>> actionListMap = new System.Collections.Generic.Dictionary<string, System.Collections.Generic.List<System.Action>>();
        public static void AddActionListener(string name, System.Action action)
        {
            if (string.IsNullOrEmpty(name) || action == null)
            {
                Debug.Log("EventManager AddListener failed,the name IsNullOrEmpty or the listener to add is null");
                return;
            }

            System.Collections.Generic.List<System.Action> cbs = null;
            if (map.ContainsKey(name))
            {
                cbs = actionListMap[name];
            }
            else
            {
                cbs = new System.Collections.Generic.List<System.Action>();
                actionListMap.Add(name, cbs);
            }
            cbs.Add(action);
        }

        public static void RemoveActionListener(string name, System.Action action)
        {
            if (string.IsNullOrEmpty(name) || action == null)
            {
                Debug.Log("EventManager RemoveListener failed,the name IsNullOrEmpty or the listener to add is null");
                return;
            }

            if (!actionListMap.ContainsKey(name))
            {
                Debug.Log("EventManager RemoveListener failed,the name is already removed");
                return;
            }
            var cbs = actionListMap[name];
            if (!cbs.Contains(action))
            {
                Debug.Log("EventManager RemoveListener failed,the callback is already removed");
                return;
            }
            cbs.Remove(action);
        }

        public static void RemoveActionListener(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                Debug.Log("EventManager RemoveListener failed,the name IsNullOrEmpty");
                return;
            }

            if (!actionListMap.ContainsKey(name))
            {
                Debug.Log("EventManager RemoveListener failed,the name is already removed");
                return;
            }
            actionListMap.Remove(name);
        }

        public static void BrocastToAction(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                Debug.Log("EventManager Brocast failed,the name IsNullOrEmpty");
                return;
            }

            if (!map.ContainsKey(name))
            {
                Debug.Log("EventManager Brocast failed,the name to brocast is not exist");
                return;
            }
            var cbs = actionListMap[name];
            foreach (var cb in cbs)
            {
                cb();
            }

        }

        private static System.Collections.Generic.Dictionary<string, System.Collections.Generic.List<Callback>> map = new System.Collections.Generic.Dictionary<string, System.Collections.Generic.List<Callback>>();

        public static void AddListener(string name, Callback cb)
        {
            if (string.IsNullOrEmpty(name) || cb == null)
            {
                Debug.Log("EventManager AddListener failed,the name IsNullOrEmpty or the listener to add is null");
                return;
            }

            System.Collections.Generic.List<Callback> cbs = null;
            if (map.ContainsKey(name))
            {
                cbs = map[name];
            }
            else
            {
                cbs = new System.Collections.Generic.List<Callback>();
                map.Add(name, cbs);
            }
            cbs.Add(cb);
        }

        public static void RemoveListener(string name, Callback cb)
        {
            if (string.IsNullOrEmpty(name) || cb == null)
            {
                Debug.Log("EventManager RemoveListener failed,the name IsNullOrEmpty or the listener to add is null");
                return;
            }

            if (!map.ContainsKey(name))
            {
                Debug.Log("EventManager RemoveListener failed,the name is already removed");
                return;
            }
            var cbs = map[name];
            if (!cbs.Contains(cb))
            {
                Debug.Log("EventManager RemoveListener failed,the callback is already removed");
                return;
            }
            cbs.Remove(cb);
        }

        public static void RemoveListener(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                Debug.Log("EventManager RemoveListener failed,the name IsNullOrEmpty");
                return;
            }

            if (!map.ContainsKey(name))
            {
                Debug.Log("EventManager RemoveListener failed,the name is already removed");
                return;
            }
            map.Remove(name);
        }

        public static void Brocast(string name, params object[] objs)
        {
            if (string.IsNullOrEmpty(name))
            {
                Debug.Log("EventManager Brocast failed,the name IsNullOrEmpty");
                return;
            }

            if (!map.ContainsKey(name))
            {
                Debug.Log("EventManager Brocast failed,the name to brocast is not exist");
                return;
            }
            var cbs = map[name];
            foreach (var cb in cbs)
            {
                cb(objs);
            }

        }

    }

}