using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TGS_GetMenu : MonoBehaviour
{
    public ScenesList SelectedScene;
    public Panels SelectedPanel;
    Button btn;
    private void Start()
    {
        if (this.GetComponent<Button>())
            btn = this.GetComponent<Button>();
        if (btn)
        {
            if (TGS_Constants.CurrentScene != SelectedScene)
            {
                btn.onClick.AddListener(() => UIManager.Instance.Goto(SelectedScene));
            }else
            btn.onClick.AddListener(() => UIManager.Instance.OpenPanel(SelectedPanel));
            
        }
    }
}
