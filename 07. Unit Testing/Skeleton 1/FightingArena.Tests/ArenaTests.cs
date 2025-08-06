using System;

namespace FightingArena.Tests
{
    using NUnit.Framework;

    [TestFixture]
    public class ArenaTests
    {
        private Arena arena;
        private Warrior warrior1;
        private Warrior warrior2;

        [SetUp]
        public void SetUp()
        {
            arena = new Arena();
            warrior1 = new Warrior("Warrior1", 50, 100);
            warrior2 = new Warrior("Warrior2", 40, 80);
        }

        [Test]
        public void Enroll_ShouldAddWarriorToArenaCorrectly()
        {
            arena.Enroll(warrior1);
            int expectedCount = 1;
            Assert.AreEqual(expectedCount, arena.Count);
            Assert.Contains(warrior1, (System.Collections.ICollection)arena.Warriors);
        }

        [Test]
        public void Enroll_ShouldThrowException_WhenWarriorAlreadyEnrolled()
        {
            arena.Enroll(warrior1);
            Assert.Throws<InvalidOperationException>(() => arena.Enroll(warrior1), "Warrior is already enrolled for the fights!");
        }

        [Test]
        public void Fight_ShouldReduceDefenderHP_WhenFightIsValid()
        {
            arena.Enroll(warrior1);
            arena.Enroll(warrior2);

            int initialDefenderHP = warrior2.HP;
            arena.Fight(warrior1.Name, warrior2.Name);

            Assert.AreEqual(initialDefenderHP - warrior1.Damage, warrior2.HP);
        }
        
        [Test]
        public void Fight_ShouldThrowException_WhenAttackerOrDefenderNotFound()
        {
            arena.Enroll(warrior1);
            Assert.Throws<InvalidOperationException>(() => arena.Fight("NonExistent", warrior1.Name), "There is no fighter with name NonExistent enrolled for the fights!");
            Assert.Throws<InvalidOperationException>(() => arena.Fight(warrior1.Name, "NonExistent"), "There is no fighter with name NonExistent enrolled for the fights!");
        }
    }
}
