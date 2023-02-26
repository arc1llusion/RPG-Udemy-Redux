using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using Unity.VisualScripting;
using UnityEngine;


namespace RPG.Saving
{
    public class SavingSystem : MonoBehaviour
    {
        public void Save(string saveFile)
        {
            string path = GetPathFromSaveFile(saveFile);
            Debug.Log("Saving to " + path);

            using (var stream = File.Open(path, FileMode.Create))
            {
                Transform playerTransform = GetPlayerTransform();

                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, new SerializableVector3(playerTransform.position));
            }
        }

        public void Load(string saveFile)
        {
            string path = GetPathFromSaveFile(saveFile);
            Debug.Log("Loading from " + path);

            if (!File.Exists(path))
            {
                return;
            }

            using (var stream = File.Open(path, FileMode.Open))
            {
                var formatter = new BinaryFormatter();
                var graph = formatter.Deserialize(stream);

                Transform playerTransform = GetPlayerTransform();
                var sv3 = graph as SerializableVector3;
                playerTransform.position = sv3.ToVector();
            }
        }

        private Vector3 DeserializeVector(byte[] buffer)
        {
            Vector3 position = new Vector3();

            position.x = BitConverter.ToSingle(buffer, 0);
            position.y = BitConverter.ToSingle(buffer, 4);
            position.z = BitConverter.ToSingle(buffer, 8);

            return position;
        }

        private string GetPathFromSaveFile(string saveFile)
        {
            return Path.Combine(Application.persistentDataPath, Path.ChangeExtension(saveFile, ".sav"));
        }

        private Transform GetPlayerTransform()
        {
            return GameObject.FindGameObjectWithTag("Player").transform;
        }
    }
}