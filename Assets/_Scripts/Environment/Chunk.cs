using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk : MonoBehaviour
{
    public int chunkIndex = 0;
    ChunkManager manager;

    public List<Chunk> neighborChunks;
    private void Awake()
    {
        manager = FindObjectOfType<ChunkManager>();

        DetectNeighborChunks();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ship"))
        {
            manager.UpdateChunks(chunkIndex);
        }
    }

    public void DetectNeighborChunks()
    {
        neighborChunks.Clear();

        Ray[] rays = new Ray[8];

        // Find all neighbor Chunks.
        rays[0] = new Ray(transform.position + new Vector3(0, 5.0f, 0), new Vector3(1, 0, 0));
        rays[1] = new Ray(transform.position + new Vector3(0, 5.0f, 0), new Vector3(-1, 0, 0));
        rays[2] = new Ray(transform.position + new Vector3(0, 5.0f, 0), new Vector3(0, 0, 1));
        rays[3] = new Ray(transform.position + new Vector3(0, 5.0f, 0), new Vector3(0, 0, -1));
        rays[4] = new Ray(transform.position + new Vector3(0, 5.0f, 0), new Vector3(-0.5f, 0, 0.5f));
        rays[5] = new Ray(transform.position + new Vector3(0, 5.0f, 0), new Vector3(-0.5f, 0, -0.5f));
        rays[6] = new Ray(transform.position + new Vector3(0, 5.0f, 0), new Vector3(0.5f, 0, -0.5f));
        rays[7] = new Ray(transform.position + new Vector3(0, 5.0f, 0), new Vector3(0.5f, 0, 0.5f));

        for (int i = 0; i < 8; ++i)
        {
            RaycastHit hit;
            if (Physics.Raycast(rays[i], out hit, Mathf.Infinity, manager.chunkLayerMask))
            {
                neighborChunks.Add(hit.collider.gameObject.GetComponentInParent<Chunk>());
            }
        }
    }

    public void MakeNeighborChunks(List<Chunk> chunks)
    {
        Ray[] rays = new Ray[8];

        // Find all neighbor Chunks.
        rays[0] = new Ray(transform.position + new Vector3(0, 5.0f, 0), new Vector3(1, 0, 0));
        rays[1] = new Ray(transform.position + new Vector3(0, 5.0f, 0), new Vector3(-1, 0, 0));
        rays[2] = new Ray(transform.position + new Vector3(0, 5.0f, 0), new Vector3(0, 0, 1));
        rays[3] = new Ray(transform.position + new Vector3(0, 5.0f, 0), new Vector3(0, 0, -1));
        rays[4] = new Ray(transform.position + new Vector3(0, 5.0f, 0), new Vector3(-0.5f, 0, 0.5f));
        rays[5] = new Ray(transform.position + new Vector3(0, 5.0f, 0), new Vector3(-0.5f, 0, -0.5f));
        rays[6] = new Ray(transform.position + new Vector3(0, 5.0f, 0), new Vector3(0.5f, 0, -0.5f));
        rays[7] = new Ray(transform.position + new Vector3(0, 5.0f, 0), new Vector3(0.5f, 0, 0.5f));

        float[] distances = new float[8];
        distances[0] = manager.chunkDistance;
        distances[1] = manager.chunkDistance;
        distances[2] = manager.chunkDistance;
        distances[3] = manager.chunkDistance;
        distances[4] = manager.diagonalChunkDistance;
        distances[5] = manager.diagonalChunkDistance;
        distances[6] = manager.diagonalChunkDistance;
        distances[7] = manager.diagonalChunkDistance;

        for (int i = 0; i < 8; ++i)
        {
            RaycastHit hit;
            if (!Physics.Raycast(rays[i], out hit, Mathf.Infinity, manager.chunkLayerMask))
            {
                // Set position for new chunk
                chunks[0].gameObject.transform.position = transform.position + (distances[i] * rays[i].direction);
                chunks.RemoveAt(0);
            }
        }
    }
}
