using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlePoolingHandler : MonoBehaviour
{
    private void OnDisable()
    {
        ObjectPooler.poolerInstance.ReturnToPool(this.gameObject);
    }
}
