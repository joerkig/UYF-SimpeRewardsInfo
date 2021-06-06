using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.IO;
using System.Text.RegularExpressions;
using UnityEngine;
using TMPro;
using MelonLoader;
using SG.Claymore.Interaction;//Where the Name and Description hide
using SG.Claymore.Entities;//Health hides here
using SG.GlobalEvents.Variables;
using SG.Claymore.Combat.Combatants;
using SG.Claymore.UI;

namespace SimpleInfo
{
    public class MyMod : MelonMod
    {
        float currentHealthPrev;
        float maxHealthPrev;
        float currentDashPrev;
        float maxDashPrev;
        string comboLimitPrev;
        string dashDamagePrev;
        readonly string healthPip = "<img src=\"HealthPip.png\">";
        readonly string healthPipCracked = "<img src=\"HealthPipCracked.png\">";
        static readonly string modFolder = MelonUtils.UserDataDirectory + "\\SimpleInfo\\";
        string reloadScript = "<script>function timedRefresh(timeoutPeriod) {setTimeout(\"location.reload(true); \",timeoutPeriod);} window.onload = timedRefresh(1500);</script>";
        static void Blank(string file)
        {
            if (File.ReadAllText(modFolder + file) != null)//If not empty, empty it
            {
                File.WriteAllText(modFolder + file, null);
            }
        }
        static void CreateIfMissing(string file)
        {
            if (File.Exists(modFolder + file) == false)//If missing create text based file
            {
                File.CreateText(modFolder + file);
            }
        }
    static void WriteReward(string position)//Write Reward info to their txt files 
        {
            string pattern = "(<script(\\s|\\S)*?<\\/script>)|(<style(\\s|\\S)*?<\\/style>)|(<!--(\\s|\\S)*?-->)|(<\\/?(\\s|\\S)*?>)";
            GameObject gameObject = GameObject.Find("RewardSpawner/SpawnPoints/" + position);
            if (gameObject != null)
            {
                RewardInteractable rewardInteractable = gameObject.GetComponentInChildren<RewardInteractable>();
                if (rewardInteractable != null)
                {
                    if (rewardInteractable.DisplayName != File.ReadAllText(modFolder + "DisplayName" + position + ".txt"))
                    {
                        File.WriteAllText(modFolder + "DisplayName" + position + ".txt", rewardInteractable.DisplayName);
                        MelonLogger.Msg(rewardInteractable.DisplayName);
                        File.WriteAllText(modFolder + "DisplayDescription" + position + ".txt", Regex.Replace(rewardInteractable.DisplayDescription, pattern, ""));
                        MelonLogger.Msg(Regex.Replace(rewardInteractable.DisplayDescription, pattern, ""));
                    }
                }
            }
        }
        public override void OnLateUpdate()
        {
            GameObject playerInteractor = GameObject.Find("Player/PlayerInteractor");
            if (playerInteractor != null)
            {
                PlayerHealth playerHealth = playerInteractor.GetComponent<PlayerHealth>();
                if (playerHealth != null)
                {
                    if (playerHealth.CurrentHealth != currentHealthPrev)
                    {
                        int currentHealth = (int)Math.Ceiling(playerHealth.CurrentHealth);
                        int maxHealth = (int)Math.Ceiling(playerHealth.MaxHealth);
                        int emptyHealth = maxHealth - currentHealth;
                        File.WriteAllText(modFolder + "CurrentHealth.txt", playerHealth.CurrentHealth.ToString());
                        try
                        {
                            File.WriteAllText(modFolder + "Health.html", "<center>" + String.Concat(Enumerable.Repeat(healthPip, currentHealth)) + String.Concat(Enumerable.Repeat(healthPipCracked, emptyHealth)) + "</center>" + reloadScript);
                        }
                        catch (ArgumentOutOfRangeException) { }
                        MelonLogger.Msg(playerHealth.CurrentHealth.ToString() + "/" + playerHealth.MaxHealth.ToString());
                        currentHealthPrev = playerHealth.CurrentHealth;
                    }
                    if (playerHealth.MaxHealth != maxHealthPrev)
                    {
                        int currentHealth = (int)Math.Ceiling(playerHealth.CurrentHealth);
                        int maxHealth = (int)Math.Ceiling(playerHealth.MaxHealth);
                        int emptyHealth = maxHealth - currentHealth;
                        File.WriteAllText(modFolder + "MaxHealth.txt", playerHealth.MaxHealth.ToString());
                        try
                        {
                            File.WriteAllText(modFolder + "Health.html", "<center>" + String.Concat(Enumerable.Repeat(healthPip, currentHealth)) + String.Concat(Enumerable.Repeat(healthPipCracked, emptyHealth)) + "</center>" + reloadScript);
                        }
                        catch (ArgumentOutOfRangeException) { }
                        MelonLogger.Msg(playerHealth.CurrentHealth.ToString() + "/" + playerHealth.MaxHealth.ToString());
                        maxHealthPrev = playerHealth.MaxHealth;
                    }
                }
            }

            WriteReward("Left");
            WriteReward("Center");
            WriteReward("Right");

            GameObject dashUI_Instanced = GameObject.Find("DashUI_Instanced");
            if (dashUI_Instanced != null)
            {
                DashUIInstanced dashUIInstanced = dashUI_Instanced.GetComponent<DashUIInstanced>();
                if (dashUIInstanced != null)
                {
                    string dashPip = "<img src=\"DashPip.png\">";
                    string dashPipEmpty = "<img src=\"DashPipEmpty.png\">";
                    if (dashUIInstanced._cachedCurrentValue != currentDashPrev)
                    {
                        int emptyDash = dashUIInstanced._cachedMaxValue - dashUIInstanced._cachedCurrentValue;
                        File.WriteAllText(modFolder + "CurrentDash.txt", dashUIInstanced._cachedCurrentValue.ToString());
                        try
                        {
                            File.WriteAllText(modFolder + "Dash.html", "<center>" + String.Concat(Enumerable.Repeat(dashPip, dashUIInstanced._cachedCurrentValue)) + String.Concat(Enumerable.Repeat(dashPipEmpty, emptyDash)) + "</center>" + reloadScript);
                        }
                        catch (ArgumentOutOfRangeException) { }
                        MelonLogger.Msg(dashUIInstanced._cachedCurrentValue.ToString() + "/" + dashUIInstanced._cachedMaxValue.ToString());
                        currentDashPrev = dashUIInstanced._cachedCurrentValue;
                    }
                    if (dashUIInstanced._cachedMaxValue != maxDashPrev)
                    {
                        int emptyDash = dashUIInstanced._cachedMaxValue - dashUIInstanced._cachedCurrentValue;
                        File.WriteAllText(modFolder + "MaxDash.txt", dashUIInstanced._cachedMaxValue.ToString());
                        try
                        {
                            File.WriteAllText(modFolder + "Dash.html", "<center>" + String.Concat(Enumerable.Repeat(dashPip, dashUIInstanced._cachedCurrentValue)) + String.Concat(Enumerable.Repeat(dashPipEmpty, emptyDash)) + "</center>" + reloadScript);
                        }
                        catch (ArgumentOutOfRangeException) { }
                        MelonLogger.Msg(dashUIInstanced._cachedCurrentValue.ToString() + "/" + dashUIInstanced._cachedMaxValue.ToString());
                        maxDashPrev = dashUIInstanced._cachedMaxValue;
                    }
                }
            }
            GameObject comboLimitLabel = GameObject.Find("ComboLimitLabel");
            if (comboLimitLabel != null)
            {
                TMP_Text combo_Text = comboLimitLabel.GetComponent<TextMeshPro>();
                if (combo_Text != null)
                {
                    if (combo_Text.m_text != comboLimitPrev)
                    {
                        File.WriteAllText(modFolder + "ComboLimit.txt", combo_Text.m_text);
                        comboLimitPrev = combo_Text.m_text;
                    }
                }
            }
            GameObject damageLabel = GameObject.Find("DamageLabel ");
            if (damageLabel != null)
            {
                TMP_Text damage_Text = damageLabel.GetComponent<TextMeshPro>();
                if (damage_Text != null)
                    if (damage_Text.m_text != dashDamagePrev)
                    {
                        File.WriteAllText(modFolder + "DashDamage.txt", damage_Text.m_text);
                        dashDamagePrev = damage_Text.m_text;
                    }
            }
        }
        public override void OnSceneWasLoaded(int buildIndex, string sceneName)
        {
            Blank("DisplayNameLeft.txt");//Remove the reward info when changing room
            Blank("DisplayNameCenter.txt");
            Blank("DisplayNameRight.txt");
            Blank("DisplayDescriptionLeft.txt");
            Blank("DisplayDescriptionCenter.txt");
            Blank("DisplayDescriptionRight.txt");
        }
        public override void OnApplicationStart()
        {
            Directory.CreateDirectory(modFolder);//Create a folder just for me <3
            CreateIfMissing("MaxHealth.txt");
            CreateIfMissing("CurrentHealth.txt");
            CreateIfMissing("DisplayNameLeft.txt");
            CreateIfMissing("DisplayDescriptionLeft.txt");
            CreateIfMissing("DisplayNameCenter.txt");
            CreateIfMissing("DisplayDescriptionCenter.txt");
            CreateIfMissing("DisplayNameRight.txt");
            CreateIfMissing("DisplayDescriptionRight.txt");
            CreateIfMissing("MaxDash.txt");
            CreateIfMissing("CurrentDash.txt");
            CreateIfMissing("ComboLimit.txt");
            CreateIfMissing("DashDamage.txt");
            if (File.Exists(modFolder + "Health.html") == false)
            {
                File.WriteAllText(modFolder + "Health.html", reloadScript);
            }
            if (File.Exists(modFolder + "HealthPip.png") == false)
            {
                Properties.Resources.HealthPip.Save(modFolder + "HealthPip.png", ImageFormat.Png);
            }
            if (File.Exists(modFolder + "HealthPipCracked.png") == false)
            {
                Properties.Resources.HealthPipCracked.Save(modFolder + "HealthPipCracked.png", ImageFormat.Png);
            }
            if (File.Exists(modFolder + "Dash.html") == false)
            {
                File.WriteAllText(modFolder + "Dash.html", reloadScript);
            }
            if (File.Exists(modFolder + "DashPip.png") == false)
            {
                Properties.Resources.DashPip.Save(modFolder + "DashPip.png", ImageFormat.Png);
            }
            if (File.Exists(modFolder + "DashPipEmpty.png") == false)
            {
                Properties.Resources.DashPipEmpty.Save(modFolder + "DashPipEmpty.png", ImageFormat.Png);
            }
        }
        public override void OnApplicationQuit()
        {
            Blank("DisplayNameLeft.txt");
            Blank("DisplayNameCenter.txt");
            Blank("DisplayNameRight.txt");
            Blank("DisplayDescriptionLeft.txt");
            Blank("DisplayDescriptionCenter.txt");
            Blank("DisplayDescriptionRight.txt");
            Blank("MaxHealth.txt");
            Blank("CurrentHealth.txt");
            Blank("MaxDash.txt");
            Blank("CurrentDash.txt");
            Blank("DashDamage.txt");
            Blank("ComboLimit.txt");
            File.WriteAllText(modFolder + "Health.html", reloadScript);
            File.WriteAllText(modFolder + "Dash.html", reloadScript);
        }
    }
}