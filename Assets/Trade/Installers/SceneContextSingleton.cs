using UnityEngine;
using Zenject;

public class SceneContextSingleton : MonoBehaviour
{
    public static SceneContext Instance;

    private void Awake() => Instance = GetComponent<SceneContext>();
}