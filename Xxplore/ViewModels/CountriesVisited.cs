﻿using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xxplore.Data;
using Xxplore.Models;

namespace Xxplore.ViewModels
{
    public class CountriesVisited
    {
        private readonly ApplicationDbContext _context;
        public UserProfile UserProfile { get; set; }
        public Country Country { get; set; }
        public SelectList WishLists { get; set; }

    }
}
