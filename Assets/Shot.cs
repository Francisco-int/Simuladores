using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Shot : MonoBehaviour
{
    [SerializeField] Transform shootPoint;
    [SerializeField] float shootForce;
    [SerializeField] GameObject bullet;
    [SerializeField] Slider moveRotX;
    [SerializeField] InputField updateShootForce;
    [SerializeField] InputField updateBreakForce;
    [SerializeField] FixedJoint[] fixedJointsBreakForce;
    
    // Start is called before the first frame update
    void Start()
    {
        if (shootForce >= fixedJointsBreakForce[0].breakForce)
        {
            Debug.Log("La fuerza del disparo es suficiente para romper la junta.");
        }
        else
        {
            Debug.Log("La fuerza del disparo no es suficiente para romper la junta.");
        }
        moveRotX.maxValue = 360;
        updateShootForce.onEndEdit.AddListener(UpdateShootForce);
        updateBreakForce.onEndEdit.AddListener(UpdateBreakForce);
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(0);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            Shoot();
        }
        transform.localRotation = Quaternion.Euler(-moveRotX.value,0,0);
    }

    void Shoot()
    {
        GameObject projectile = Instantiate(bullet, shootPoint.position, shootPoint.rotation);
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        rb.AddForce(shootPoint.up * shootForce);
        Destroy(projectile, 4f );
    }
    void UpdateShootForce(string input)
    {
        float newForce;
        if (float.TryParse(input, out newForce))
        {
            shootForce = newForce;
        }
        else
        {
            Debug.LogWarning("Valor de fuerza no válido. Asegúrate de ingresar un número.");
        }
        if (shootForce >= fixedJointsBreakForce[0].breakForce)
        {
            Debug.Log("La fuerza del disparo es suficiente para romper la junta.");
        }
        else
        {
            Debug.Log("La fuerza del disparo no es suficiente para romper la junta.");
        }
    }
    void UpdateBreakForce(string input)
    {
        float newForce;
        if (float.TryParse(input, out newForce))
        {
            fixedJointsBreakForce[0].breakForce = newForce;
            fixedJointsBreakForce[1].breakForce = newForce;
        }
        else
        {
            Debug.LogWarning("Valor de fuerza no válido. Asegúrate de ingresar un número.");
        }
        if (shootForce >= fixedJointsBreakForce[0].breakForce)
        {
            Debug.Log("La fuerza del disparo es suficiente para romper la junta.");
        }
        else
        {
            Debug.Log("La fuerza del disparo no es suficiente para romper la junta.");
        }
    }
}
