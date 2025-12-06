using UnityEngine;

public class CenarioInfinito : MonoBehaviour
{
    public float VelocidadeDoCenario;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MovimentarCenario();
    }

    private void MovimentarCenario()
    { 
        Vector2 deslocamento = new Vector2(Time.deltaTime * VelocidadeDoCenario, 0);
        GetComponent<Renderer>().material.mainTextureOffset = deslocamento;
    }
}
