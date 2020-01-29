using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LuckySpin.Models;
using LuckySpin.ViewModels; 

namespace LuckySpin.Controllers
{
    public class SpinnerController : Controller
    {
        Random random;
        Repository repository;

        /***
         * Controller Constructor
         */
        public SpinnerController(Repository repository)
        {
            random = new Random();
            //TODO: Inject the Repository singleton
            repository = new Repository();
        }

        /***
         * Entry Page Action
         **/
        [HttpGet]
        public IActionResult Index()
        {
                return View();
        }
        [HttpPost]
        public IActionResult Index(Player player)
        {
            if (!ModelState.IsValid) { return View(); }

            // TODO: Add the Player to the Repository - CHECK
            repository.player = player; // pulls from the parameter

            // TODO: Build a new SpinItViewModel object with data from the Player and pass it to the View - CHECK
            SpinViewModel spinViewModel = new SpinViewModel();
            spinViewModel.FirstName = player.FirstName;
            spinViewModel.Balance = player.Balance;
            spinViewModel.Luck = player.Luck;

            return RedirectToAction("SpinIt", spinViewModel); 
        }

        /***
         * Spin Action - Game Play
         **/  
         public IActionResult SpinIt(SpinViewModel spinViewModel) //TODO: replace the parameter with a ViewModel - CHECK
        {
            Spin spin = new Spin
            {
                Luck = spinViewModel.Luck,
                A = random.Next(1, 10),
                B = random.Next(1, 10),
                C = random.Next(1, 10)
            };

            spin.IsWinning = (spin.A == spin.Luck || spin.B == spin.Luck || spin.C == spin.Luck);

            //Add to Spin Repository
            repository.AddSpin(spin);

            //TODO: Clean up ViewBag using a SpinIt ViewModel instead
            ViewBag.ImgDisplay = (spin.IsWinning) ? "block" : "none";
            ViewBag.FirstName = spinViewModel.FirstName;
            ViewBag.Balance = spinViewModel.Balance;

            return View("SpinIt", spin);
        }

        /***
         * LuckList Action - End of Game
         **/
         public IActionResult LuckList()
        {
                return View(repository.PlayerSpins);
        }
    }
}

