using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bullet : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        // Obtener la velocidad del proyectil en el momento de la colisión
        Rigidbody rb = GetComponent<Rigidbody>();
        float impactForce = rb.mass * rb.velocity.magnitude; // Fórmula básica de fuerza de impacto

        // Mostrar la fuerza de impacto en la consola
        Debug.Log($"Impacto con {collision.gameObject.name} con una fuerza de {impactForce} N.");

        // Si quieres agregar esta información al ScrollView, puedes hacerlo aquí
        Shot shot = FindObjectOfType<Shot>();
        if (shot != null && shot.scrollViewContent != null && shot.textPrefab != null)
        {
            string impactInfo = $"Impacto con {collision.gameObject.name} con una fuerza de {impactForce} N.";
            GameObject newTextObject = Instantiate(shot.textPrefab, shot.scrollViewContent.transform);
            Text newTextComponent = newTextObject.GetComponent<Text>();
            newTextComponent.text = impactInfo;
        }
    }
}
