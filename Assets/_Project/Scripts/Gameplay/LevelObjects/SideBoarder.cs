using Assets._Project.Scripts.Gameplay.BallLogic;
using UnityEngine;

public class SideBoarder : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.TryGetComponent(out Ball ball))
        {
            Vector3 reflectedVelocity = Vector3.Reflect(collision.relativeVelocity, collision.GetContact(0).normal);

            ball.SetVelocity(reflectedVelocity);
        }
    }
}
