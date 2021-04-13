using System.IO;
using System.Text.RegularExpressions;
using UnityEngine;
using MelonLoader;
using SG.Claymore.Interaction;//Where the Name and Description hide

namespace SimpleRewards
{
    public class MyMod : MelonMod
    {
        public override void OnLateUpdate()
        {
            string pattern = "(<script(\\s|\\S)*?<\\/script>)|(<style(\\s|\\S)*?<\\/style>)|(<!--(\\s|\\S)*?-->)|(<\\/?(\\s|\\S)*?>)";
            GameObject left = GameObject.Find("RewardSpawner/SpawnPoints/Left");
            if (left != null)
            {
                RewardInteractable rewardInteractable = left.GetComponentInChildren<RewardInteractable>();
                if (rewardInteractable != null)
                {
                    if (rewardInteractable.DisplayName.ToString() != File.ReadAllText(MelonLoaderBase.UserDataPath + "\\DisplayNameLeft.txt"))
                    {
                        File.WriteAllText(MelonLoaderBase.UserDataPath + "\\DisplayNameLeft.txt", rewardInteractable.DisplayName.ToString());
                        File.WriteAllText(MelonLoaderBase.UserDataPath + "\\DisplayDescriptionLeft.txt", Regex.Replace(rewardInteractable.DisplayDescription.ToString(), pattern, ""));
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
            GameObject center = GameObject.Find("RewardSpawner/SpawnPoints/Center");
            if (center != null)
            {
                RewardInteractable rewardInteractable = center.GetComponentInChildren<RewardInteractable>();
                if (rewardInteractable != null)
                {
                    if (rewardInteractable.DisplayName.ToString() != File.ReadAllText(MelonLoaderBase.UserDataPath + "\\DisplayNameCenter.txt"))
                    {
                        File.WriteAllText(MelonLoaderBase.UserDataPath + "\\DisplayNameCenter.txt", rewardInteractable.DisplayName.ToString());
                        File.WriteAllText(MelonLoaderBase.UserDataPath + "\\DisplayDescriptionCenter.txt", Regex.Replace(rewardInteractable.DisplayDescription.ToString(), pattern, ""));
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
            GameObject right = GameObject.Find("RewardSpawner/SpawnPoints/Right");
            if (right != null)//prevents the code from crashing the game while referencing a null object
            {
                RewardInteractable rewardInteractable = right.GetComponentInChildren<RewardInteractable>();
                if (rewardInteractable != null)
                {
                    if (rewardInteractable.DisplayName.ToString() != File.ReadAllText(MelonLoaderBase.UserDataPath + "\\DisplayNameRight.txt"))
                    {
                        File.WriteAllText(MelonLoaderBase.UserDataPath + "\\DisplayNameRight.txt", rewardInteractable.DisplayName.ToString());
                        File.WriteAllText(MelonLoaderBase.UserDataPath + "\\DisplayDescriptionRight.txt", Regex.Replace(rewardInteractable.DisplayDescription.ToString(), pattern, ""));
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