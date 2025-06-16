// using System;
// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
//
// public class LoseGameManager : MonoBehaviour
// {
//     public static LoseGameManager Instance;
//
//     [SerializeField] private GameObject YouFailed;
//     [SerializeField] private GameObject Rules;
//
//     private void Awake()
//     {
//         if (Instance is null)
//             Instance = this;
//         else
//             Destroy(gameObject);
//     }
//
//     private void OnDestroy()
//     {
//         if (Instance == this)
//             Instance = null;
//     }
//
//     public void Lose()
//     {
//         YouFailed.SetActive(true);
//         Rules.SetActive(false);
//     }
//
// }
