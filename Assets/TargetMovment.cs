using UnityEngine;
using UnityEngine.AI;

public class TargetMovment : MonoBehaviour
{
   public Transform finishLine;
   
   private NavMeshAgent agent;

   private void Start()
   {
      
      agent = GetComponent<NavMeshAgent>();
   }

   private void FixedUpdate()
   {
      agent.destination = finishLine.position;
   }
}
