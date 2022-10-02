using Xunit;
using Xunit.Abstractions;

namespace TPCards.Tests
{
    public class Tests
    {
        private readonly ITestOutputHelper _output;

        public Tests(ITestOutputHelper output)
        {
            _output = output;
        }

        /// <summary>
        ///     test 1: assert the same card is not dealt twice
        /// </summary>
        [Fact]
        public void DealingIsValidTest()
        {
            Assert.True(DealingIsValid());
        }

        private bool DealingIsValid()
        {
            Deck deck = new Deck();
            // insure the same card is not dealt twice
            HashSet<int> cards = new HashSet<int>();
            for (int i = 0; i < deck.Count; i++)
            {
                int nextCardValue = deck.Deal();
                if (cards.Contains(nextCardValue))
                {
                    return false;
                }
                cards.Add(nextCardValue);
            }
            // if all cards are dealt without encountering a duplicate, return true
            return true;
        }

        /// <summary>
        ///     test 2: assert dealing over the deck capacity yields -1
        /// </summary>
        [Fact]
        public void DeckCapacityIsValidTest()
        {
            Assert.True(DeckCapacityIsValid());
        }

        private bool DeckCapacityIsValid()
        {
            Deck deck = new Deck();
            // perform all valid deals
            for (int i = 0; i < deck.Count; i++)
            {
                if (deck.Deal() == -1)
                {
                    return false;
                }
            }
            // deal next card over capacity
            int nextCardValue = deck.Deal();
            return nextCardValue == -1;
        }

        /// <summary>
        ///     test 3: assert shuffling the deck is effective (assuming a valid shuffle results in
        ///             a >= 50% difference with the original deck order)
        /// </summary>
        [Fact]
        public void DeckShuffleIsValidTest()
        {
            Assert.True(DeckShuffleIsValid());
        }

        private bool DeckShuffleIsValid()
        {
            Deck deck = new Deck();
            // create copy of initial card list
            List<Card> initialOrder = new List<Card>(deck.Cards);
            // perform shuffle
            deck.Shuffle();
            // create copy of shuffled card list
            List<Card> shuffledOrder = new List<Card>(deck.Cards);

            int diffCount = 0;
            // compare cards at each position in the deck
            for (int i = 0; i < deck.Count; i++)
            {
                if (initialOrder[i].Value != shuffledOrder[i].Value)
                {
                    diffCount++;
                }
            }

            double percentDiff = (double)diffCount / deck.Count;
            _output.WriteLine($"diff count: {diffCount} :: percent diff: {percentDiff}");
            return percentDiff >= .50;
        }
    }
}