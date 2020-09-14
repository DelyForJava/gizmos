namespace Whatever
{

    public class MyEvenTrigger : UnityEngine.MonoBehaviour, UnityEngine.EventSystems.IPointerClickHandler
    {
        [XLua.CSharpCallLua]
        public static System.Action<UnityEngine.Vector3> luaAction;
        public UnityEngine.Camera mainCamera;
        public void OnPointerClick(UnityEngine.EventSystems.PointerEventData eventData)
        {
            luaAction(eventData.pointerCurrentRaycast.worldPosition);
        }

    }

}