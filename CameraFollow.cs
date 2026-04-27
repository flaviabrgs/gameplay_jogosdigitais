using UnityEngine;

public class CameraFollow : MonoBehaviour 
{
    public Transform alvo;
    public float suavidade = 0.125f;
    public Vector3 offset = new Vector3(0, 0, -10);

    void LateUpdate() 
    {
        if (alvo != null) 
        {
            
            Vector3 posicaoDesejada = alvo.position + offset;
            
            
            Vector3 posicaoSuavizada = Vector3.Lerp(transform.position, posicaoDesejada, suavidade);
            transform.position = posicaoSuavizada;
        }
    }
}
