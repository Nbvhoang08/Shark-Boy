using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayCanvas : UICanvas
{
    // Start is called before the first frame update
    public void PauseBtn()
    {
        Time.timeScale = 0;
        UIManager.Instance.OpenUI<PauseCanvas>();
        SoundManager.Instance.PlayVFXSound(2);
    }
}
