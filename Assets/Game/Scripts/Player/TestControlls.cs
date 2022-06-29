using System;
using UnityEngine;

namespace Characters
{
    public class TestControlls : MonoBehaviour
    {
        [SerializeField] private FlyingCharacter character;
        
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                character.StopMotion();
            }
        }
    }
}