using System;

namespace FightingArena.Tests
{
    using NUnit.Framework;

    [TestFixture]
    public class WarriorTests
    {
        Warrior warrior;
        private const string name = "Krasimir";
        private const int damage = 50;
        private const int hp = 100;
        private const int MIN_ATTACK_HP = 30;
        
        [Test]
        public void Construcor_ShouldInitializeCorrectly()
        {
            warrior = new Warrior(name, damage, hp);

            Assert.AreEqual(name, warrior.Name);
            Assert.AreEqual(damage, warrior.Damage);
            Assert.AreEqual(hp, warrior.HP);
        }
        
        [Test]
        public void Name_ShouldThrowException_WhenSetToNullOrWhitespace()
        {
            Assert.Throws<ArgumentException>(() => new Warrior(null, damage, hp), "Name should not be empty or whitespace!");
            Assert.Throws<ArgumentException>(() => new Warrior(string.Empty, damage, hp), "Name should not be empty or whitespace!");
            Assert.Throws<ArgumentException>(() => new Warrior(" ", damage, hp), "Name should not be empty or whitespace!");
        }
        
        [Test]
        public void Damage_ShouldThrowException_WhenSetToZeroOrNegative()
        {
            Assert.Throws<ArgumentException>(() => new Warrior(name, 0, hp), "Damage value should be positive!");
            Assert.Throws<ArgumentException>(() => new Warrior(name, -1, hp), "Damage value should be positive!");
        }

        [Test]
        public void HP_ShouldThrowException_WhenSetToNegative()
        {
            Assert.Throws<ArgumentException>(() => new Warrior(name, damage, -1), "HP should not be negative!");
        }
        
        [Test]
        public void Attack_ShouldAttackSuccessfully_WhenWarriorIsValid_WarriorHpIsReduced()
        {
            Warrior defender = new Warrior("Defender", 30, 80);
            warrior = new Warrior(name, damage, hp);

            int expectedDefenderHP = defender.HP - warrior.Damage;
            warrior.Attack(defender);

            Assert.AreEqual(expectedDefenderHP, defender.HP);
        }
        
        [Test]
        public void Attack_ShouldAttackSuccessfully_WhenWarriorIsValid_WarriorHpIsSetToZero()
        {
            Warrior defender = new Warrior("Defender", 30, 40);
            warrior = new Warrior(name, damage, hp);

            int expectedDefenderHP = 0;
            warrior.Attack(defender);

            Assert.AreEqual(expectedDefenderHP, defender.HP);
        }

        [Test]
        public void Attack_ShouldThrowException_WhenAttackerHpIsLessThenMinimumAttackHp()
        {
            Warrior defender = new Warrior("Defender", 30, 80);
            warrior = new Warrior(name, damage, 29);
            
            Assert.Throws<InvalidOperationException>(() => warrior.Attack(defender), "Your HP is too low to attack other warriors!");
        }

        [Test]
        public void Attack_ShouldThrowException_WhenDefenderHpIsLessThanMinimumAttackHp()
        {
            Warrior defender = new Warrior("Defender", 30, 29);
            warrior = new Warrior(name, damage, hp);
            
            Assert.Throws<InvalidOperationException>(() => warrior.Attack(defender), $"Enemy HP must be greater than {MIN_ATTACK_HP} in order to attack him!");
        }
        
        [Test]
        public void Attack_ShouldThrowException_WhenDefenderDamageIsGreaterThanAttackerHp()
        {
            Warrior defender = new Warrior("Defender", 101, 80);
            warrior = new Warrior(name, damage, hp);
            
            Assert.Throws<InvalidOperationException>(() => warrior.Attack(defender), "You are trying to attack too strong enemy");
        }
    }
}