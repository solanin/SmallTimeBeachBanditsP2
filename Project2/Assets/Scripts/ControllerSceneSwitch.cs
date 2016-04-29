using UnityEngine;
using XboxCtrlrInput;
using System.Collections;

public class ControllerSceneSwitch : MonoBehaviour
{
    float moveCountDown = 0.5f;
    int position;

    float[] xPos = { -1.7f, 0.0f, 1.7f };

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (XCI.GetAxis(XboxAxis.LeftStickX) > 0.5f || XCI.GetAxis(XboxAxis.LeftStickX) < -0.5f)
        {
            if (moveCountDown == 0.5f)
            {
                if (XCI.GetAxis(XboxAxis.LeftStickX) > 0.0f)
                {
                    position++;
                    if (position > 2)
                        position = 0;
                }
                else
                {
                    position--;
                    if (position < 0)
                        position = 2;
                }
            }

            moveCountDown -= Time.deltaTime;
            if (moveCountDown <= 0.0f)
            {
                moveCountDown = 0.5f;
            }

        }
        else
        {
            moveCountDown = 0.5f;
        }

        if (XCI.GetButtonDown(XboxButton.A))
        {
            switch (position)
            {
                case 0:
                    Application.LoadLevel("game");
                    break;
                case 1:
                    Application.LoadLevel("highscore");
                    break;
                case 2:
                    Application.LoadLevel("credits");
                    break;
                default:
                    break;
            }
        }

        transform.position = new Vector3(xPos[position], transform.position.y);
    }
}
