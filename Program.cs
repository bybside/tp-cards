// See https://aka.ms/new-console-template for more information
using static System.Console;

namespace TPCards
{
    public class Card
    {
        public int Value { get; set; }
    }

    public class Deck
    {
        // track all cards which have been previously dealt
        private List<Card> _dealt = new List<Card>();
        public int Count { get; private set; }
        public List<Card> Cards { get; private set; } = new List<Card>();

        /// <summary>
        ///     creates a new Deck with the specified card count (default: 52)
        /// </summary>
        public Deck(int count = 52)
        {
            Count = count;
            // generating cards--(1, count]
            for (int i = 1; i <= count; i++)
            {
                Cards.Add(new Card
                {
                    Value = i
                });
            }
            // once deck is loaded, perform initial shuffle
            Shuffle();
        }

        /// <summary>
        ///     adds any dealt cards back to deck and shuffles
        /// </summary>
        public void Shuffle()
        {
            _Reload();
            // perform individual shuffle operation for each card in deck
            for (int currIndex = 0; currIndex < Count; currIndex++)
            {
                int shuffleIndex = Random.Shared.Next(0, Count - 1);
                // swapping cards at curr index and shuffle index
                Card currCard = Cards[currIndex];
                Card shuffleCard = Cards[shuffleIndex];
                Cards[currIndex] = shuffleCard;
                Cards[shuffleIndex] = currCard;
            }
        }

        /// <summary>
        ///     deals and discards random card from deck
        /// </summary>
        public int Deal()
        {
            if (Cards.Count == 0)
            {
                return -1;
            }
            // get card on top of deck
            Card nextCard = Cards[0];
            // add next card to discard pile
            _dealt.Add(nextCard);
            // remove top card from deck
            Cards.RemoveAt(0);
            // return value of dealt card
            return nextCard.Value;
        }

        /// <summary>
        ///     reload dealt cards back into deck
        /// </summary>
        private void _Reload()
        {
            if (_dealt.Count > 0)
            {
                // add previously dealt cards back to deck
                Cards.AddRange(_dealt);
                // clear dealt cards deck
                _dealt.Clear();
            }
        }
    }

    public class Program
    {
        static void Main(string[] args)
        {
            // note: can optionally provide card count (default is 52)
            Deck deck = new Deck();
            WriteLine($"num of cards in deck: {deck.Count}");

            WriteLine("dealing a card...");
            WriteLine($"dealing: {deck.Deal()}");

            WriteLine("reloading and shuffling deck...");
            deck.Shuffle();

            WriteLine("dealing a new card...");
            WriteLine($"dealing: {deck.Deal()}");
        }
    }
}
