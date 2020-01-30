﻿using System;
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
            sVM.Balance = player.StartingBalance;
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

            
            spinVM.IsWinning = (spinVM.A == spinVM.Luck || spinVM.B == spinVM.Luck || spinVM.C == spinVM.Luck);

            //Add to Spin Repository
            _repository.AddSpin(spinVM);

            //TODO: Clean up ViewBag using a SpinIt ViewModel instead
            ViewBag.ImgDisplay = (spinViewModel.IsWinning) ? "block" : "none";
            ViewBag.FirstName = spinViewModel.FirstName;
            ViewBag.Balance = spinViewModel.Balance;

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

