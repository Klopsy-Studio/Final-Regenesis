using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnStatusUI : MonoBehaviour
{

    [Header("Turn Status")]
    [SerializeField] Image turnStatus;
    [SerializeField] Text turnUser;

    [SerializeField] Animator turnStatusAnim;


    public void ActivateTurn(string turn)
    {
        turnStatusAnim.SetBool("inScreen", true);
        turnUser.text = turn;
    }

    public void DeactivateTurn()
    {
        turnStatusAnim.SetBool("inScreen", false);

    }

}
