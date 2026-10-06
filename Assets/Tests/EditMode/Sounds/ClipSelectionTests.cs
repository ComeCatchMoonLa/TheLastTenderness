using NUnit.Framework;
using UnityEngine;

namespace CatchMoon.Tests
{
    public class ClipSelectionTests
    {
        [Test]
        public void SeveralClips_SkipsThePreviousOne()
        {
            var first = AudioClip.Create("a", 1, 1, 1000, false);
            var previous = AudioClip.Create("b", 1, 1, 1000, false);
            var third = AudioClip.Create("c", 1, 1, 1000, false);
            AudioClip[] clips = { first, previous, third };

            Assert.AreSame(first, CharacterSoundFXManager.PickClipAvoidingPrevious(clips, previous, 0));
            Assert.AreSame(third, CharacterSoundFXManager.PickClipAvoidingPrevious(clips, previous, 1));
        }

        [Test]
        public void SingleClip_ReturnsThatClip()
        {
            var only = AudioClip.Create("only", 1, 1, 1000, false);

            Assert.AreSame(only, CharacterSoundFXManager.PickClipAvoidingPrevious(new[] { only }, only, 0));
        }

        [Test]
        public void EmptyArray_ReturnsNull()
        {
            Assert.IsNull(CharacterSoundFXManager.PickClipAvoidingPrevious(new AudioClip[0], null, 0));
        }
    }
}
