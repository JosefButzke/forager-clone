using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderInLayer : MonoBehaviour
{
    public float offSet;
    private SpriteRenderer m_SpriteRender;
    private float playerY;

    // Start is called before the first frame update
    void Start()
    {
        m_SpriteRender = GetComponent<SpriteRenderer>();    
    }

    private void LateUpdate()
    {
        if(m_SpriteRender == null) { return; }

        playerY = CoreGame._instance.playerController.positionY;
        if(transform.position.y < playerY - offSet)
        {
            m_SpriteRender.sortingLayerName = "PrimeiroPlano";
        } else
        {
            m_SpriteRender.sortingLayerName = "SegundoPlano";
        }
    }
}
