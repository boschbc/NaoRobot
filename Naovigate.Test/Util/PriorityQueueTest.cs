using System;
using System.Diagnostics;

using NUnit.Framework;

using Naovigate.Util;

namespace Naovigate.Test.Util
{

    [TestFixture, Timeout(2500)]
    public class PriorityQueueTest
    {
        private static PriorityQueue<int> q;

        // add all ints in data, with those priorities
        private void Add(params int[] data)
        {
            Add(data, data);
        }

        // add all the inputs with the priorities
        private void Add(int[] input, int[] priorities)
        {
            Assert.AreEqual(input.Length, priorities.Length);
            for (int i = 0; i < input.Length; i++)
            {
                Console.WriteLine("Added " + input[i] + " => " + priorities[i]);
                q.Enqueue(input[i], priorities[i]);
            }
        }

        // empty the queue, check if the queue matches the data array
        private void Check(params int[] data)
        {
            Assert.IsNotNull(q);
            foreach (int i in data)
            {
                Assert.AreEqual(i, q.Dequeue());
            }

        }

        [SetUp]
        public void NewQueue()
        {
            q = new PriorityQueue<int>(10);
        }

        [Test]
        public void EmptyAtStartTest()
        {
            Assert.IsTrue(q.IsEmpty());
        }

        [Test]
        public void NotEmptyAfterAddTest()
        {
            q.Enqueue(0, 0);
            Assert.IsFalse(q.IsEmpty());
        }

        [Test]
        public void EmptyAfterRemoveTest()
        {
            q.Enqueue(0, 0);
            q.Dequeue();
            Assert.IsTrue(q.IsEmpty());
        }

        [Test]
        public void InitialSizeTest()
        {
            Assert.AreEqual(0, q.Size());
        }

        [Test]
        public void SizeTest()
        {
            int expected = 0;
            Add(8, 4);
            expected += 2;
            Assert.AreEqual(expected, q.Size());
            q.Dequeue();
            expected--;
            Assert.AreEqual(expected, q.Size());
            Add(4, 4, 4, 4);
            expected += 4;
            Assert.AreEqual(expected, q.Size());
            for (int i = 0; i < 3; i++) q.Dequeue();
            expected -= 3;
            Assert.AreEqual(expected, q.Size());
        }

        [Test]
        public void EnqueueInOrderTest()
        {
            Add(new int[] { 1, 2, 3 }, new int[] { 0, 0, 0 });
            Check(1, 2, 3);
        }

        [Test]
        public void EnqueuePriorityTest()
        {
            Add(1, 2, 3);
            Check(3, 2, 1);
        }

        [Test]
        public void EnqueuePriorityRandomOrderTest()
        {
            Add(1, 4, 3, 7, 5, 6, 8, 2, 9);
            Check(9, 8, 7, 6, 5, 4, 3, 2, 1);
        }

        [Test]
        public void EnqueuePriorityAndOrderTest()
        {
            Add(new int[] { 1, 2, 3 },
                    new int[] { 0, 2, 0 });
            Check(2, 1, 3);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void EnqueueToHighPriorityTest()
        {
            q.Enqueue(0, 100);
        }
    }
}
