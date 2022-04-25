using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace emresisman
{
    public class CollisionDetect : MonoBehaviour
    {
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.tag == "Enemy")
            {
                Debug.Log(isEnemyUnderThePlayer(collision.GetContact(0).normal));
                Debug.Log(collision.GetContact(0).normal);
            }
        }

        private bool isEnemyUnderThePlayer(Vector2 point)
        {
            if (point.y >= 0.8f) return true;
            else return false;
        }

        private bool isXAxisCorrect(float XAxis)
        {
            if (XAxis <= 0.3f && XAxis >= -0.4f) return true;
            else return false;
        }

        private bool isYAxisCorrect(float YAxis)
        {
            if (YAxis <= 1f && YAxis >= 0.9f) return true;
            else return false;
        }
    }
}
