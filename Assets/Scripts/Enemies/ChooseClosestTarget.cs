using UnityEngine;
using UnityEngine.AI;

namespace Scripts.Enemies
{
    public class ChooseClosestTarget : MonoBehaviour
    {
        public Transform[] targets;
        public NavMeshAgent agent;

        public void ChooseTarget()
        {
            //Transform closestTarget = null;
            float closestDistance = float.MaxValue;
            NavMeshPath path = null;
            NavMeshPath shortestPath = null;

            for (int i = 0; i < targets.Length; i++)
            {
                if (targets[i] == null)
                {
                    continue;
                }
                path = new NavMeshPath();

                if (NavMesh.CalculatePath(transform.position, targets[i].position, agent.areaMask, path))
                {
                    float distance = Vector3.Distance(transform.position, path.corners[0]);

                    for (int j = 0; j < path.corners.Length; j++)
                    {
                        distance += Vector2.Distance(path.corners[j-1], path.corners[j]);

                        if (distance < closestDistance)
                        {
                            closestDistance = distance;
                            shortestPath = path;
                        }
                    }
                }

                if (shortestPath != null)
                {
                    agent.SetPath(shortestPath);
                }
            }
        }

        private void OnGui()
        {
            if (GUI.Button(new Rect(10, 10, 100, 30), "Move To Target"))
            {
                ChooseTarget();
            }
        }
    }
}