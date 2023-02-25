using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
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
                stream.Write(SerializeVector(playerTransform.position));
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
                byte[] buffer = new byte[stream.Length];
                stream.Read(buffer, 0, buffer.Length);

                var output = DeserializeVector(buffer);
                GetPlayerTransform().position = output;
            }
        }

        private byte[] SerializeVector(Vector3 vector)
        {
            byte[] vectorBytes = new byte[3 * 4];

            BitConverter.GetBytes(vector.x).CopyTo(vectorBytes, 0);
            BitConverter.GetBytes(vector.y).CopyTo(vectorBytes, 4);
            BitConverter.GetBytes(vector.z).CopyTo(vectorBytes, 8);

            return vectorBytes;

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