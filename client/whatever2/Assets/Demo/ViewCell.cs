namespace Whatever
{
    public class ViewCell : UnityEngine.MonoBehaviour
    {
        public UnityEngine.UI.Text text;
        // Start is called before the first frame update
        void ScrollCellIndex(int idx)
        {
            text.text = "cell at:" + idx;
        }
 
    }

}