using Unity;
using Unity.VisualScripting;
using UnityEngine;


public class Bullet : MonoBehaviour
{
    private int _damage;
    private int _speed;
    public GameObject theObj;
    public Bullet(int damage, int speed)
    {
        _damage = damage;
        _speed = speed;
    }

    public void Set(int damage,int speed)
    {
        _damage = damage;
        _speed = speed;
    }
}