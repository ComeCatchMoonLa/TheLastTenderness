using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace CatchMoon.Tests
{
    public class CovenantHandInTests
    {
        [Test]
        public void TryOffer_GrantsEachTierOnce_AndLeavesABadOffer()
        {
            Item proof = ScriptableObject.CreateInstance<Item>();
            Item wrong = ScriptableObject.CreateInstance<Item>();
            List<Item> bag = new List<Item> { proof, proof, wrong };
            HandInState state = new HandInState { firstAt = 1, secondAt = 2 };

            Assert.IsTrue(CovenantHandIn.TryOffer(bag, proof, proof, state, "祭坛"));
            Assert.AreEqual(1, state.handed);
            Assert.IsTrue(state.giveFirst);
            Assert.IsFalse(state.giveSecond);

            Assert.IsTrue(CovenantHandIn.TryOffer(bag, proof, proof, state, "祭坛"));
            Assert.AreEqual(2, state.handed);
            Assert.IsFalse(state.giveFirst);
            Assert.IsTrue(state.giveSecond);

            int before = bag.Count;
            Assert.IsFalse(CovenantHandIn.TryOffer(bag, proof, wrong, state, "祭坛"));
            Assert.AreEqual(2, state.handed);
            Assert.AreEqual(before, bag.Count);

            HandInState empty = new HandInState { firstAt = 0, secondAt = 2 };
            LogAssert.Expect(LogType.Error, "祭坛: firstAt 未填");
            Assert.IsFalse(CovenantHandIn.TryOffer(bag, proof, wrong, empty, "祭坛"));
            Assert.AreEqual(0, empty.handed);
        }
    }
}
