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
    [SerializeField] Slider moveRotZ;
    [SerializeField] Slider movePositionX;
    [SerializeField] InputField updateShootForce;
    [SerializeField] InputField updateBreakForce;
    [SerializeField] FixedJoint[] fixedJointsBreakForce;

    private float lastShootForce;
    private float lastAngleX;
    private float lastAngleZ;
    private float lastPositionX;

    [SerializeField] Text newEntry;
    public GameObject scrollViewContent;
    public GameObject textPrefab;

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
        moveRotZ.maxValue = 360;
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

        transform.position = new Vector3(movePositionX.value, transform.position.y, transform.position.z);

        transform.localRotation = Quaternion.Euler(-moveRotX.value,0, moveRotZ.value);
        newEntry.text = $"Fuerza del disparo: "+ shootForce +"\nÁngulo X: " + moveRotX.value +"\nÁngulo Z: "+ moveRotZ.value +"\nPosición X: " + transform.position.x;

    }

    void Shoot()
    {
        lastShootForce = shootForce;
        lastAngleX = moveRotX.value;
        lastAngleZ = moveRotZ.value;
        lastPositionX = transform.position.x;

        GameObject projectile = Instantiate(bullet, shootPoint.position, shootPoint.rotation);
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        rb.AddForce(shootPoint.up * shootForce);
        Destroy(projectile, 4f );


        Debug.Log("Fuerza del disparo: " + lastShootForce);
        Debug.Log("Ángulo X del disparo: " + lastAngleX);
        Debug.Log("Ángulo Z del disparo: " + lastAngleZ);
        Debug.Log("Posición X del cañón: " + lastPositionX);

        newEntry.text = $"Fuerza del disparo: {lastShootForce}\nÁngulo X: {lastAngleX}\nÁngulo Z: {lastAngleZ}\nPosición X: {lastPositionX}";

        GameObject newTextObject = Instantiate(textPrefab, scrollViewContent.transform);
        Text newTextComponent = newTextObject.GetComponent<Text>();
        newTextComponent.text = newEntry.text;
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
            fixedJointsBreakForce[0].breakTorque = newForce;
            fixedJointsBreakForce[1].breakTorque = newForce;
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
