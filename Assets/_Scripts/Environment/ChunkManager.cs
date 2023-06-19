using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkManager : MonoBehaviour
{
    List<Chunk> chunks = new List<Chunk>();
    public LayerMask chunkLayerMask;
    Chunk currentCenterChunk;

    public float chunkDistance = 1.0f;
    public float diagonalChunkDistance = 1.0f;
    private void Awake()
    {
        var gameObjects = GameObject.FindGameObjectsWithTag("Chunk");
     
        foreach(var chunk in gameObjects)
        {
            chunks.Add(chunk.GetComponent<Chunk>());
        }
        
        for (int i = 0; i < 9; ++i)
        {
            chunks[i].chunkIndex = i;
        }
    }

    public void UpdateChunks(int newCenterChunk)
    {
        if (currentCenterChunk == chunks[newCenterChunk])
            return;

        currentCenterChunk = chunks[newCenterChunk];
        List<Chunk> temporalChunks = new List<Chunk>(chunks);

        temporalChunks.Remove(currentCenterChunk);

        foreach(var chunk in currentCenterChunk.neighborChunks)
        {
            temporalChunks.Remove(chunk); // Remove every chunk that is a neighbor
                                                    // remaining chunks are not neighbors
        }

        if (temporalChunks.Count == 0)
            return;

        currentCenterChunk.MakeNeighborChunks(temporalChunks);

        for (int i = 0; i < 9; ++i)
        {
            chunks[i].DetectNeighborChunks();
        }
    }
}
