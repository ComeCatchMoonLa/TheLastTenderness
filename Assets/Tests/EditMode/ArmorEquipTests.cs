using System.Collections.Generic;
using System.Reflection;
using NUnit.Framework;
using UnityEngine;

namespace CatchMoon.Tests
{
    public class ArmorEquipTests
    {
        [Test]
        public void EquippedPart_ActivatesNamedModelAndCopiesAbsorption()
        {
            var rig = new ArmorRig();
            var armor = ScriptableObject.CreateInstance<TorsoArmorItem>();
            armor.transformName = "plate";
            armor.physicalDA = 0.2f;
            armor.fireDA = 0.3f;
            armor.magicDA = 0.4f;
            armor.lightningDA = 0.5f;
            armor.darkDA = 0.6f;
            rig.armor.currentTorsoArmor = armor;

            rig.armor.EquipAllArmorModels();

            Assert.IsTrue(rig.torsoPlate.activeSelf);
            Assert.IsFalse(rig.torsoNaked.activeSelf);
            Assert.AreEqual(0.2f, rig.stats.torsoArmorPDA, 0.001f);
            Assert.AreEqual(0.3f, rig.stats.torsoArmorFDA, 0.001f);
            Assert.AreEqual(0.4f, rig.stats.torsoArmorMDA, 0.001f);
            Assert.AreEqual(0.5f, rig.stats.torsoArmorLDA, 0.001f);
            Assert.AreEqual(0.6f, rig.stats.torsoArmorDDA, 0.001f);
            rig.Destroy(armor);
        }

        [Test]
        public void MissingArmor_WithNakedName_ActivatesNakedAndZeroesAbsorption()
        {
            var rig = new ArmorRig();

            rig.armor.EquipAllArmorModels();

            Assert.IsFalse(rig.torsoPlate.activeSelf);
            Assert.IsTrue(rig.torsoNaked.activeSelf);
            Assert.AreEqual(0f, rig.stats.torsoArmorPDA, 0.001f);
            Assert.AreEqual(0f, rig.stats.torsoArmorFDA, 0.001f);
            Assert.AreEqual(0f, rig.stats.torsoArmorMDA, 0.001f);
            Assert.AreEqual(0f, rig.stats.torsoArmorLDA, 0.001f);
            Assert.AreEqual(0f, rig.stats.torsoArmorDDA, 0.001f);
            rig.Destroy();
        }

        [Test]
        public void HeadWithoutNakedName_LeavesModelsOffAndZeroesAbsorption()
        {
            var rig = new ArmorRig();

            rig.armor.EquipAllArmorModels();

            Assert.IsFalse(rig.headHelm.activeSelf);
            Assert.AreEqual(0f, rig.stats.headArmorPDA, 0.001f);
            Assert.AreEqual(0f, rig.stats.headArmorFDA, 0.001f);
            Assert.AreEqual(0f, rig.stats.headArmorMDA, 0.001f);
            Assert.AreEqual(0f, rig.stats.headArmorLDA, 0.001f);
            Assert.AreEqual(0f, rig.stats.headArmorDDA, 0.001f);
            rig.Destroy();
        }

        sealed class ArmorRig
        {
            public PlayerArmorManager armor;
            public CharacterStatsManager stats;
            public GameObject torsoPlate;
            public GameObject torsoNaked;
            public GameObject headHelm;
            GameObject root;

            public ArmorRig()
            {
                root = new GameObject("armor-rig");
                torsoPlate = new GameObject("plate");
                torsoNaked = new GameObject("naked-torso");
                headHelm = new GameObject("helm");
                var hipsNaked = new GameObject("naked-hips");
                var head = Changer(headHelm);
                var torso = Changer(torsoPlate, torsoNaked);
                var hips = Changer(hipsNaked);

                var player = root.AddComponent<PlayerManager>();
                var playerStats = root.AddComponent<PlayerStatsManager>();
                stats = playerStats;
                player.pStats = playerStats;
                armor = root.AddComponent<PlayerArmorManager>();

                Set(armor, "player", player);
                Set(armor, "headModelChanger", head);
                Set(armor, "torsoModelChanger", torso);
                Set(armor, "hipModelChanger", hips);
                Set(armor, "nakedTorsoModelName", "naked-torso");
                Set(armor, "nakedHipsModelName", "naked-hips");
            }

            public void Destroy(Object extra = null)
            {
                if (extra != null)
                    Object.DestroyImmediate(extra);
                Object.DestroyImmediate(root);
            }

            ModelChanger Changer(params GameObject[] models)
            {
                var go = new GameObject("changer");
                go.transform.SetParent(root.transform);
                foreach (GameObject model in models)
                    model.transform.SetParent(go.transform);
                var changer = go.AddComponent<ModelChanger>();
                changer.models = new List<GameObject>(models);
                return changer;
            }

            static void Set(Object target, string fieldName, object value)
            {
                var field = target.GetType().GetField(fieldName, BindingFlags.Instance | BindingFlags.NonPublic);
                field.SetValue(target, value);
            }
        }
    }
}
