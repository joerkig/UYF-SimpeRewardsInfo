using System.IO;
using System.Text.RegularExpressions;
using UnityEngine;
using MelonLoader;
using SG.Claymore.Interaction;//Where the Name and Description hide

namespace SimpleRewards
{
    public class MyMod : MelonMod
    {
        static void Blank(string position)
        {
            if (File.ReadAllText(MelonUtils.UserDataDirectory + "\\DisplayName" + position + ".txt") != null)
            {
                File.WriteAllText(MelonUtils.UserDataDirectory + "\\DisplayName" + position + ".txt", null);
                File.WriteAllText(MelonUtils.UserDataDirectory + "\\DisplayDescription" + position + ".txt", null);
            }
        }
        static void WriteInfo(string position)
        {
            string pattern = "(<script(\\s|\\S)*?<\\/script>)|(<style(\\s|\\S)*?<\\/style>)|(<!--(\\s|\\S)*?-->)|(<\\/?(\\s|\\S)*?>)";
            GameObject gameObject = GameObject.Find("RewardSpawner/SpawnPoints/" + position);
            if (gameObject != null)
            {
                RewardInteractable rewardInteractable = gameObject.GetComponentInChildren<RewardInteractable>();
                if (rewardInteractable != null)
                {
                    if (rewardInteractable.DisplayName.ToString() != File.ReadAllText(MelonUtils.UserDataDirectory + "\\DisplayName" + position + ".txt"))
                    {
                        File.WriteAllText(MelonUtils.UserDataDirectory + "\\DisplayName" + position + ".txt", rewardInteractable.DisplayName.ToString());
                        File.WriteAllText(MelonUtils.UserDataDirectory + "\\DisplayDescription" + position + ".txt", Regex.Replace(rewardInteractable.DisplayDescription.ToString(), pattern, ""));
                    }
                }
            }
        }
        public override void OnLateUpdate()
        {
            WriteInfo("Left");
            WriteInfo("Center");
            WriteInfo("Right");
        }
        public override void OnSceneWasLoaded(int buildIndex, string sceneName)
        {
            Blank("Left");
            Blank("Center");
            Blank("Right");
        }
        public override void OnApplicationQuit()
        {
            Blank("Left");
            Blank("Center");
            Blank("Right");
        }
    }
}