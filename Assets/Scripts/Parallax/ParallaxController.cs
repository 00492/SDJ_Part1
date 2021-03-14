using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Permet d'utiliser la struct comme variable multiple. (Plusieurs variables)
[System.Serializable]
public struct BackGround
{
    public float m_LayerDistance;
    public GameObject m_Visual;
}

public class ParallaxController : MonoBehaviour
{
    [SerializeField]
    private Transform m_Camera;
    [SerializeField]
    private float m_Smoothing = 1f;
    [SerializeField]
    private List<BackGround> m_BackGrounds = new List<BackGround>();

    private Vector3 m_PreviousCamPos = new Vector3();
    private Vector3 m_BackgroundTargetPos = new Vector3();

    private void Start()
    {
        m_PreviousCamPos = m_Camera.position;
    }


    private void Update()
    {
        /*
        for (int i = 0; i < m_Backgrounds.Count; i++)
        {
            // On déplace nos Backgrounds

            m_Backgrounds[i].transform.Translate(-m_Speed_Background * Time.deltaTime, 0f, 0f);

            // On vérifie si nos backgrounds ont dépassé la limite de l'écran

            if (m_Backgrounds[i].transform.position.x < m_ScreenWidth)
            {
                // On assigne une nouvelle position en x au background ayant dépassé la limite

                m_MovePos.x = m_PreviousBackground.transform.position.x - m_ScreenWidth;
                m_Backgrounds[i].transform.position = m_MovePos;
                m_Backgrounds[i].transform.Translate(-m_Speed_Background * Time.deltaTime, 0f, 0f);
            }
            m_PreviousBackground = m_Backgrounds[i];
        }
        */
    }

    private void LateUpdate()
    {
        float parallaxevalue;
        BackGround backGround;

        // On passe par tous les backgrounds de la liste
        for (int i = 0; i < m_BackGrounds.Count; i++)
        {
            // On stock notre background à la variable i
            backGround = m_BackGrounds[i];
            parallaxevalue = (m_PreviousCamPos.x - m_Camera.position.x) * -backGround.m_LayerDistance;

            // On initialise notre vecteur de posibion tu background avec la position courante du Visuel
            m_BackgroundTargetPos = backGround.m_Visual.transform.position;

            // On veut modifier la valeure du x du background
            m_BackgroundTargetPos.x += parallaxevalue;

            // Interpollation linéaire de la position actuelle avec la position voulue pour créer l'effet de parallaxe
            backGround.m_Visual.transform.position = Vector3.Lerp(backGround.m_Visual.transform.position, m_BackgroundTargetPos, m_Smoothing * Time.deltaTime);
        }

        m_PreviousCamPos = m_Camera.position;
    }
}
