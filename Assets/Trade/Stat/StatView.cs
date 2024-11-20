using UnityEngine;
using UnityEngine.UI;

public class StatView : MonoBehaviour
{
    [SerializeField]
    private Stat _stat;

    [SerializeField]
    private Text _text;

    [SerializeField]
    private string _mask;

    public void Refresh()
    {
        _text.text = _mask + _stat.Current.ToString();
    }
}