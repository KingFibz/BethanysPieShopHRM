﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BethanysPieShopHRM.HR
{
    internal class JuniorResearcher : Researcher
    {
        public JuniorResearcher(string firstName, string lastName, string email, DateTime birthDay, double? hourlyRate) : base(firstName, lastName, email, birthDay, hourlyRate)
        {
        }
    }
}
