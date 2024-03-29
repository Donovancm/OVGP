﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OVGPFinalv1.Models;

namespace OVGPFinalv1.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<OVGPFinalv1.Models.User> User { get; set; }
        public DbSet<OVGPFinalv1.Models.Content> Content { get; set; }

        public DbSet<OVGPFinalv1.Models.Chat> Chat { get; set; }

        public DbSet<OVGPFinalv1.Models.Agenda> Agenda { get; set; }
        public DbSet<OVGPFinalv1.Models.Nieuwsbrief> NieuwsBrief { get; set; }

        public DbSet<OVGPFinalv1.Models.Comment> Comment { get; set; }
    }
}
