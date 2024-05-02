using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class ThrowingKnifeProjectile : MonoBehaviour
{
    Vector3 direction;
    [SerializeField] float speed;
    [SerializeField] float damage = 5;

    public void SetDirection(float dir_x, float dir_y){
        direction = new Vector3(dir_x, dir_y, 0f);

        if(dir_x < 0){
            Quaternion scale = transform.rotation;
            scale.z *= -1;
            transform.rotation = scale;
        }
    }

    bool hitDetected = false;
    private void Update(){
        transform.position += direction * speed * Time.deltaTime;

        if(Time.frameCount %6 != 0){
            return;
        }

        Collider2D[] hit = Physics2D.OverlapCircleAll(transform.position, 0.5f);
        foreach(Collider2D c in hit){
            Enemy e = c.GetComponent<Enemy>();
            if(e != null){
                MessageSystem.instance.PostMessage(damage.ToString(), c.transform.position);
                e.TakeDamage(damage);
                hitDetected = true;
                break;
            }
        }
        if(hitDetected){
            Destroy(gameObject);
        }
    }
}
