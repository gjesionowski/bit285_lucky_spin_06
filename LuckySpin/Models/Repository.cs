using System.Collections.Generic;
using LuckySpin.ViewModels;

namespace LuckySpin.Models
{
    public class Repository
    {
        private List<SpinViewModel> spins = new List<SpinViewModel>();

        public Player PlayerOne { get; set; } // make this a property to be added to later

        //Property
        public IEnumerable<SpinViewModel> PlayerSpins 
        {
            get { return spins; }
        }

        //Access method
        public void AddSpin(SpinViewModel s)
        {
            spins.Add(s);
        }
    }
}
