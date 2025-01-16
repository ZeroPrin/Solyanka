using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMirror : NetworkBehaviour // даём системе понять, что это сетевой объект
{
    void Update()
    {
        if (isOwned) // проверяем, есть ли у нас права изменять этот объект
        {
            float h = Input.GetAxis("Horizontal"); // движение по оси X
            float v = Input.GetAxis("Vertical");   // движение по оси Z
            float y = 0f; // движение по оси Y, можно настроить при необходимости (например, прыжки)

            float speed = 5f * Time.deltaTime;

            Vector3 movement = new Vector3(h * speed, y, v * speed); // создаём вектор для перемещения
            transform.Translate(movement, Space.World); // перемещаем объект в мировых координатах
        }
    }
}
