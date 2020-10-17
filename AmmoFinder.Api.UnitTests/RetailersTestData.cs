﻿using AmmoFinder.Common.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Xunit;

namespace AmmoFinder.Api.UnitTests
{
    public class RetailersTestData : TheoryData<IEnumerable<RetailerModel>, Type>
    {
        public RetailersTestData()
        {
            Add(new List<RetailerModel>
            {
                new RetailerModel
                {
                    CreatedOn = DateTime.Now,
                    Id = 1,
                    Name = "AmmoSurplus"
                }
            }, typeof(OkObjectResult));

            Add(new List<RetailerModel>(), typeof(NotFoundResult));
        }
    }
}
