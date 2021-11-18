using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class DissolveChilds : MonoBehaviour
    {
        // Start is called before the first frame update
        List<Material> materials = new List<Material>();
        bool PingPong = false;
        void Start()
        {
            var renders = GetComponentsInChildren<Renderer>();
            for (int i = 0; i < renders.Length; i++)
            {
                materials.AddRange(renders[i].materials);
            }
        }

        public void DissolverReset()
        {
            Start();
            SetValue(0);
        }

        // Update is called once per frame
        void Update()
        {
            var value = Mathf.PingPong(Time.time * 0.2f, 1f);
            SetValue(value);
        }



        public void SetValue(float value)
        {
            for (int i = 0; i < materials.Count; i++)
            {
                materials[i].SetFloat("_Dissolve", value);
            }
        }
    }
