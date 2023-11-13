using UnityEngine;

namespace Spawning
{
    public class SpawnArea : MonoBehaviour
    {
         [SerializeField] private Vector2 _spawnAreaSize = new Vector2(10f, 10f);
         
         private void OnDrawGizmos()
         {
             Gizmos.color = Color.yellow;
             Gizmos.DrawWireCube(transform.position, new Vector3(_spawnAreaSize.x, 0f, _spawnAreaSize.y));
         }
         
         public Vector3 GetRandomPointInRectangle()
         {
             float randomX = Random.Range(-_spawnAreaSize.x / 2f, _spawnAreaSize.x / 2f);
             float randomZ = Random.Range(-_spawnAreaSize.y / 2f, _spawnAreaSize.y / 2f);

             Vector3 randomPoint = new Vector3(randomX, 0f, randomZ);
             return transform.position + randomPoint;
         }
    }
}