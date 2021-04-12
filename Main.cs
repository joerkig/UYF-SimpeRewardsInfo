using System.IO;
using UnityEngine;
using MelonLoader;
using SG.Claymore.Interaction;//Where the Name and Description hide

namespace SimpleRewards
{
    public class MyMod : MelonMod
    {
        public override void OnLateUpdate()
        {
            GameObject left = GameObject.Find("RewardSpawner/SpawnPoints/Left");//finding the switch
            if (left != null)//prevents the code from crashing the game while referencing a null object
            {
                RewardInteractable rewardInteractable = left.GetComponentInChildren<RewardInteractable>();//still finding the switch
                if (rewardInteractable != null)
                {
                    if (rewardInteractable.DisplayName.ToString() != File.ReadAllText(MelonLoaderBase.UserDataPath + "\\DisplayNameLeft.txt"))
                    {
                        File.WriteAllText(MelonLoaderBase.UserDataPath + "\\DisplayNameLeft.txt", rewardInteractable.DisplayName.ToString());
                        File.WriteAllText(MelonLoaderBase.UserDataPath + "\\DisplayDescriptionLeft.txt", rewardInteractable.DisplayDescription.ToString());
                        //MelonLogger.Log(rewardInteractable.DisplayName.ToString());
                        //MelonLogger.Log(rewardInteractable.DisplayDescription.ToString());
                    }
                }
                else
                {
                    if (File.ReadAllText(MelonLoaderBase.UserDataPath + "\\DisplayNameLeft.txt") != null)
                    {
                        File.WriteAllText(MelonLoaderBase.UserDataPath + "\\DisplayNameLeft.txt", null);
                        File.WriteAllText(MelonLoaderBase.UserDataPath + "\\DisplayDescriptionLeft.txt", null);
                    }
                }
            }
            GameObject center = GameObject.Find("RewardSpawner/SpawnPoints/Center");//finding the switch
            if (center != null)//prevents the code from crashing the game while referencing a null object
            {
                RewardInteractable rewardInteractable = center.GetComponentInChildren<RewardInteractable>();//still finding the switch
                if (rewardInteractable != null)
                {
                    if (rewardInteractable.DisplayName.ToString() != File.ReadAllText(MelonLoaderBase.UserDataPath + "\\DisplayNameCenter.txt"))
                    {
                        File.WriteAllText(MelonLoaderBase.UserDataPath + "\\DisplayNameCenter.txt", rewardInteractable.DisplayName.ToString());
                        File.WriteAllText(MelonLoaderBase.UserDataPath + "\\DisplayDescriptionCenter.txt", rewardInteractable.DisplayDescription.ToString());
                        //MelonLogger.Log(rewardInteractable.DisplayName.ToString());
                        //MelonLogger.Log(rewardInteractable.DisplayDescription.ToString());
                    }
                }
                else
                {
                    if (File.ReadAllText(MelonLoaderBase.UserDataPath + "\\DisplayNameCenter.txt") != null)
                    {
                        File.WriteAllText(MelonLoaderBase.UserDataPath + "\\DisplayNameCenter.txt", null);
                        File.WriteAllText(MelonLoaderBase.UserDataPath + "\\DisplayDescriptionCenter.txt", null);
                    }
                }
            }
            GameObject right = GameObject.Find("RewardSpawner/SpawnPoints/Right");//finding the switch
            if (right != null)//prevents the code from crashing the game while referencing a null object
            {
                RewardInteractable rewardInteractable = right.GetComponentInChildren<RewardInteractable>();//still finding the switch
                if (rewardInteractable != null)
                {
                    if (rewardInteractable.DisplayName.ToString() != File.ReadAllText(MelonLoaderBase.UserDataPath + "\\DisplayNameRight.txt"))
                    {
                        File.WriteAllText(MelonLoaderBase.UserDataPath + "\\DisplayNameRight.txt", rewardInteractable.DisplayName.ToString());
                        File.WriteAllText(MelonLoaderBase.UserDataPath + "\\DisplayDescriptionRight.txt", rewardInteractable.DisplayDescription.ToString());
                        //MelonLogger.Log(rewardInteractable.DisplayName.ToString());
                        //MelonLogger.Log(rewardInteractable.DisplayDescription.ToString());
                    }
                }
                else
                {
                    if (File.ReadAllText(MelonLoaderBase.UserDataPath + "\\DisplayNameRight.txt") != null)
                    {
                        File.WriteAllText(MelonLoaderBase.UserDataPath + "\\DisplayNameRight.txt", null);
                        File.WriteAllText(MelonLoaderBase.UserDataPath + "\\DisplayDescriptionRight.txt", null);
                    }
                }
            }
        }
        public override void OnApplicationQuit()
        {
            if (File.ReadAllText(MelonLoaderBase.UserDataPath + "\\DisplayNameLeft.txt") != null)
            {
                File.WriteAllText(MelonLoaderBase.UserDataPath + "\\DisplayNameLeft.txt", null);
                File.WriteAllText(MelonLoaderBase.UserDataPath + "\\DisplayDescriptionLeft.txt", null);
            }
            if (File.ReadAllText(MelonLoaderBase.UserDataPath + "\\DisplayNameCenter.txt") != null)
            {
                File.WriteAllText(MelonLoaderBase.UserDataPath + "\\DisplayNameCenter.txt", null);
                File.WriteAllText(MelonLoaderBase.UserDataPath + "\\DisplayDescriptionCenter.txt", null);
            }
            if (File.ReadAllText(MelonLoaderBase.UserDataPath + "\\DisplayNameRight.txt") != null)
            {
                File.WriteAllText(MelonLoaderBase.UserDataPath + "\\DisplayNameRight.txt", null);
                File.WriteAllText(MelonLoaderBase.UserDataPath + "\\DisplayDescriptionRight.txt", null);
            }
        }
    }
}