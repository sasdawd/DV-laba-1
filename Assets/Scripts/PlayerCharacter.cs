using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCharacter : MonoBehaviour
{
    [SerializeField]
    private int _hp;
    [SerializeField]
    private Slider _hpBar;
    [SerializeField]
    private Button _restartButton;

    private int _startHp;
    // Start is called before the first frame update
    void Start()
    {
        _startHp = _hp;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Hit()
    {
        _hp--;
        _hpBar.value = (float)_hp / (float)_startHp;
        if (_hp <= 0)
        {
            Destroy(this.gameObject);
            _restartButton.gameObject.SetActive(true);
            Camera.main.GetComponent<RayShooter>().enabled = false;
            Camera.main.GetComponent<MouseLook>().enabled = false;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
