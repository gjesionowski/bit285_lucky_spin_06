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
        Repository _repository;

        /***
         * Controller Constructor
         */
        public SpinnerController(Repository repository)
        {
            random = new Random();
            //TODO: Inject the Repository singleton
            _repository = repository; 
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

            //Repository repository = new Repository();
            // TODO: Add the Player to the Repository - CHECK
            _repository.PlayerOne = player; // pulls player data from the parameter

            // TODO: Build a new SpinItViewModel object with data from the Player and pass it to the View - CHECK
            SpinViewModel sVM = new SpinViewModel();
            sVM.FirstName = player.FirstName;
            player.Balance = player.StartingBalance;
            sVM.Balance = _repository.PlayerOne.Balance;
            sVM.Luck = player.Luck;

            return RedirectToAction("SpinIt", sVM); 
        }

        /***
         * Spin Action - Game Play
         **/  
         public IActionResult SpinIt(SpinViewModel spinViewModel) //TODO: replace the parameter with a ViewModel - CHECK
        {
            SpinViewModel spinVM = new SpinViewModel
            {
                FirstName = spinViewModel.FirstName,
                Balance = spinViewModel.Balance,
                Luck = spinViewModel.Luck,
                A = random.Next(1, 10),
                B = random.Next(1, 10),
                C = random.Next(1, 10)
            };

            if (!(_repository.PlayerOne.ChargeSpin()))      // charge fee even before you know the results
                return View("LuckList");          // boot the player if they're out of balance. Show previous plays.

            spinVM.IsWinning = (spinVM.A == spinVM.Luck || spinVM.B == spinVM.Luck || spinVM.C == spinVM.Luck);

            if (spinVM.IsWinning != false)
                _repository.PlayerOne.CollectWinnings(); // Did they lose? If not, reward player

            //Add to Spin Repository
            _repository.AddSpin(spinVM);

            //TODO: Clean up ViewBag using a SpinIt ViewModel instead
            ViewBag.ImgDisplay = (spinVM.IsWinning) ? "block" : "none";
            ViewBag.FirstName = spinVM.FirstName;
            ViewBag.Balance = _repository.PlayerOne.Balance;

            return View("SpinIt", spinVM);
        }

        /***
         * LuckList Action - End of Game
         **/
         public IActionResult LuckList()
        {
                return View(_repository.PlayerSpins);
        }
    }
}

