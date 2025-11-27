using UnityEngine;

public class DifficultMode : MonoBehaviour
{
    public void Easy()
    {
        GameController.isEasyMode = true;
        transform.gameObject.SetActive(false);
    }
    public void Hard()
    {
        GameController.isEasyMode = false;
        transform.gameObject.SetActive(false);
    }
}
