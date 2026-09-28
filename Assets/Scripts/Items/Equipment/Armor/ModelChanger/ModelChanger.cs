using System.Collections.Generic;
using UnityEngine;

namespace CatchMoon
{
    public class ModelChanger : MonoBehaviour
    {
        public List<GameObject> models = new List<GameObject>();

        private void Awake()
        {
            GetAllModels();
        }

        private void GetAllModels()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                models.Add(transform.GetChild(i).gameObject);
            }
        }

        public void UnEquipAllModels()
        {
            foreach (GameObject model in models)
            {
                model.SetActive(false);
            }
        }

        public void EquipModelByName(string name)
        {
            for (int i = 0; i < models.Count; i++)
            {
                if (models[i].name == name)
                {
                    models[i].SetActive(true);
                }
            }
        }
    }
}