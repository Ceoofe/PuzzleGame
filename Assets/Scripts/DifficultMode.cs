using UnityEngine;

public class DifficultMode : MonoBehaviour
{
    public void Easy()
    {
        GameController.isEasyMode = true;
        GameController.isHardMode = false;
        transform.gameObject.SetActive(false);
    }
    public void Hard()
    {
        GameController.isEasyMode = false;
        GameController.isHardMode = true;
        transform.gameObject.SetActive(false);
    }
}
