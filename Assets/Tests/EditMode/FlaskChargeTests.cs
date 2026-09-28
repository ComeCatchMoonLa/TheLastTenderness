using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace CatchMoon.Tests
{
    public class FlaskChargeTests
    {
        [Test]
        public void Start_IsThreeEstusAndNoAsh()
        {
            PlayerInventoryManager inventory = NewInventory();

            Assert.AreEqual(3, inventory.flaskTotal);
            Assert.AreEqual(3, inventory.estusShare);
            Assert.AreEqual(0, inventory.ashShare);
        }

        [Test]
        public void ObtainAshFlask_Once_BecomesFourWithOneAsh()
        {
            PlayerInventoryManager inventory = NewInventory();

            inventory.ObtainAshFlask();
            Assert.AreEqual(4, inventory.flaskTotal);
            Assert.AreEqual(3, inventory.estusShare);
            Assert.AreEqual(1, inventory.ashShare);

            inventory.ObtainAshFlask();
            Assert.AreEqual(4, inventory.flaskTotal);
            Assert.AreEqual(3, inventory.estusShare);
            Assert.AreEqual(1, inventory.ashShare);
        }

        [Test]
        public void AddFlaskShard_StopsAtFifteen_WithoutChangingShares()
        {
            PlayerInventoryManager inventory = NewInventory();

            for (int i = 0; i < 12; i++)
                inventory.AddFlaskShard();

            Assert.AreEqual(15, inventory.flaskTotal);
            Assert.AreEqual(3, inventory.estusShare);
            Assert.AreEqual(0, inventory.ashShare);

            inventory.AddFlaskShard();
            Assert.AreEqual(15, inventory.flaskTotal);
            Assert.AreEqual(3, inventory.estusShare);
            Assert.AreEqual(0, inventory.ashShare);
        }

        [Test]
        public void AddBoneShard_StopsAtTen()
        {
            PlayerInventoryManager inventory = NewInventory();

            for (int i = 0; i < 10; i++)
                inventory.AddBoneShard();

            Assert.AreEqual(10, inventory.boneTier);
            inventory.AddBoneShard();
            Assert.AreEqual(10, inventory.boneTier);
        }

        [Test]
        public void TryGetFlaskSip_ReadsTable_NotItemAmounts()
        {
            PlayerInventoryManager inventory = NewInventory();
            FlaskRecoveryTable table = FilledTable();
            inventory.recoveryTable = table;
            var flask = ScriptableObject.CreateInstance<FlaskItem>();
            flask.healthRecoverAmount = 1;
            flask.focusPointsRecoverAmount = 1;

            inventory.boneTier = 0;
            flask.flaskType = FlaskType.estus;
            Assert.IsTrue(inventory.TryGetFlaskSip(flask.flaskType, out int health));
            Assert.AreEqual(250, health);
            flask.flaskType = FlaskType.ashen;
            Assert.IsTrue(inventory.TryGetFlaskSip(flask.flaskType, out int focus));
            Assert.AreEqual(80, focus);

            inventory.boneTier = 10;
            flask.flaskType = FlaskType.estus;
            Assert.IsTrue(inventory.TryGetFlaskSip(flask.flaskType, out health));
            Assert.AreEqual(600, health);
            flask.flaskType = FlaskType.ashen;
            Assert.IsTrue(inventory.TryGetFlaskSip(flask.flaskType, out focus));
            Assert.AreEqual(200, focus);

            Object.DestroyImmediate(flask);
            Object.DestroyImmediate(table);
        }

        [Test]
        public void TryGetFlaskSip_MissingTable_ReturnsFalseAndLogsField()
        {
            PlayerInventoryManager inventory = NewInventory();
            inventory.recoveryTable = null;

            LogAssert.Expect(LogType.Error, "PlayerInventoryManager.recoveryTable 未填");
            Assert.IsFalse(inventory.TryGetFlaskSip(FlaskType.estus, out int amount));
            Assert.AreEqual(0, amount);
        }

        static PlayerInventoryManager NewInventory()
        {
            var root = new GameObject("inventory");
            return root.AddComponent<PlayerInventoryManager>();
        }

        static FlaskRecoveryTable FilledTable()
        {
            var table = ScriptableObject.CreateInstance<FlaskRecoveryTable>();
            table.rows = new FlaskSipRow[11];
            for (int i = 0; i < table.rows.Length; i++)
                table.rows[i] = new FlaskSipRow { health = 1, focus = 1 };
            table.rows[0] = new FlaskSipRow { health = 250, focus = 80 };
            table.rows[10] = new FlaskSipRow { health = 600, focus = 200 };
            return table;
        }
    }
}
