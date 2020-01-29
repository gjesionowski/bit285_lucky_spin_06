using System.Collections.Generic;

namespace LuckySpin.Models
{
    public class Repository
    {
        private List<Spin> spins = new List<Spin>();

        public Player player { get; set; } // make this a property to be added to later

        //Property
        public IEnumerable<Spin> PlayerSpins {

            get { return spins; }
        }

        //Access method
        public void AddSpin(Spin s)
        {
            spins.Add(s);
        }
    }
}
