using UnityEngine;

public interface IPopup
{
}

public static class PopupExtensions
{
    public static Popup GetPopup(this IPopup component)
    {
        Popup popup = (component as MonoBehaviour).GetComponent<Popup>();
        return popup;
    }

    public static void Open(this IPopup component)
    {
        component.GetPopup().Open();
    }

    public static void Close(this IPopup component)
    {
        component.GetPopup().Close();
    }
}
