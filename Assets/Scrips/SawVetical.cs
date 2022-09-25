using System.Collections;
using UnityEngine;

namespace Assets.Scrips
{
    public class SawVetical : MonoBehaviour
    {
        public float rotatespeed; // toc do xoay
        public float speed;       //to do 
        public Transform pos1;    //vi tri xoay den
        public Transform pos2;    // vi tri xoay ve
        bool turnback;

        private void Update()
        {
            transform.Rotate(0, 0, rotatespeed); // rotetion 

            if (transform.position.y >= pos1.position.y)
            {
                turnback = true; 
            }
            if (transform.position.y <= pos2.position.y)
            {
                turnback = false;
            }
            if (turnback) // true 
            {
                transform.position = Vector2.MoveTowards(transform.position, pos2.position, speed * Time.deltaTime);
            }
            else
            {
                transform.position = Vector2.MoveTowards(transform.position, pos1.position, speed * Time.deltaTime);
            }
        }
    }
}